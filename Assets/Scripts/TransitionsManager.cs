using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

/// <summary>
/// Script que controla las transiciones de un estado.
/// </summary>
public class TransitionsManager : MonoBehaviour
{
    // Constantes del juego.
    public Constants constants;
    // Para conocer el tiempo de creacion de los simbolos.
    public NivelManager nivelManager;
    // Interfza del usuario para los simbolos.
    public Transform uiString;
    public Color uiSymbolColorDelete = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    // Interfza del usuario que muestra mensajes.
    public UIMessagesManager uIMessagesManager;
    // Es estado incial?.
    public bool isStateInitial = false;
    private bool auxStateInitial = true;
    //Es estado final?.
    public bool isStateFinal = false;
    

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        //// Si la cadena entro al estado.
        if (other.name.Equals(constants.SYMBOL_UAX_NAME))
        {
            Transform symbols = other.transform.parent;
            string nameFirstSymbol;

            // Si es estado inicial y es la primera vez, no se destruye un simbolo. 
            if (auxStateInitial && isStateInitial)
            {
                //Y el primer simbolo es el 1.
                nameFirstSymbol = symbols.GetChild(1).name;
                auxStateInitial = false;
            }
            // Si es estado inicial y es la segunda vez que pasa o si es cualquier otro estado.
            else
            {
                // Si hay tres o mas simbolos, el primero es el 2 del arreglo, sino el primero es 1.
                int numFirstSymbol = symbols.childCount >= 3 ? 2 : 1;
                nameFirstSymbol = symbols.GetChild(numFirstSymbol).name;
                Transform cardSymbol = uiString.GetChild(uiString.childCount - (symbols.childCount - 1));
                if (numFirstSymbol == 2)
                    cardSymbol.GetComponent<Image>().color = uiSymbolColorDelete;

                // Se destruye el primer simbolo de la cadena.
                Destroy(symbols.GetChild(1).gameObject);

                // Si solo quedan dos simbolos en la cadena, se muetra un mensaje y se finaliza.
                if (numFirstSymbol == 1)
                {
                    // Si es estado final.
                    if (isStateFinal)
                    {
                        cardSymbol.GetComponent<Image>().color = uiSymbolColorDelete;
                        uIMessagesManager.ShowWinner();
                    }                        
                    // Si no es estado final
                    else
                        uIMessagesManager.ShowLoser();

                    // Se finaliza.
                    return;
                }
            }            

            //Debug.Log(nameFirstSymbol);
            SplineContainer splineContainer = null;

            // Se obtine la transicion siguiente, segun el primer simbolo de la cadena.
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform transition = transform.GetChild(i);
                //Debug.Log(transition.name + " " + firstSymbol.name);

                // Si el primer simbolo de la cadena es igual al de la transicion.
                if (transition.name.Equals(nameFirstSymbol))
                {
                    splineContainer = transition.GetChild(0).GetComponent<SplineContainer>();
                    break;
                }
            }

            // Se agrega la siguiente transicion a todos los simbolos de la cadena.
            for (int i = 0; i < symbols.childCount; i++)
                symbols.GetChild(i).GetComponent<TransitionMove>()
                    .AddSplineContainer(splineContainer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals(constants.SYMBOL_UAX_NAME))
        {
            StartCoroutine(UpdatePositionAux(other.transform));
        }
    }

    public IEnumerator UpdatePositionAux(Transform symbolE)
    {
        TransitionMove transitionMove = symbolE.parent.GetChild(1).GetComponent<TransitionMove>();
        //Debug.Log(nivelManager.symbolTimeout - (1f / transitionMove.speed));
        yield return new WaitForSeconds(nivelManager.symbolTimeout);// - (1f / transitionMove.speed));
        symbolE.GetComponent<TransitionMove>().CopyValuesTo(transitionMove);        
    }
}
