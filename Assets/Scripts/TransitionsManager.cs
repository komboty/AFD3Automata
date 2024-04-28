using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;
using UnityEngine.UI;

/// <summary>
/// Script que controla las transiciones de un estado.
/// </summary>
public class TransitionsManager : MonoBehaviour
{
    // Para conocer el tiempo de creacion de los simbolos.
    // Y guardar la solucion del usuario.
    public ModManager modManager;
    // Interfza del usuario para los simbolos.
    public Transform uiString;
    public Color uiSymbolColorDelete = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    // Interfza del usuario que muestra mensajes.
    public UIMessagesManager uIMessagesManager;
    // Animacion de destruccion de un simbolo.
    public GameObject destructAnim;
    public float destructAnimTime = 3.2f;
    public float uiSymbolAnimTime = 0.2f;
    // Es estado incial?.
    public bool isStateInitial = false;
    private bool auxStateInitial = true;
    //Es estado final?.
    public bool isStateFinal = false;
    

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        // Si colisiono otra cosa con el estado no hacer nada.
        if (!other.name.Equals(Constants.instance.SYMBOL_UAX_NAME))
            return;

        // Si la cadena entro al estado.
        Transform symbols = other.transform.parent;
        string nameFirstSymbol;

        // Si es estado inicial y es la primera vez, no se destruye un simbolo. 
        if (auxStateInitial && isStateInitial)
        {
            //Y el primer simbolo es el 1.
            nameFirstSymbol = symbols.GetChild(1).name;
            auxStateInitial = false;
        }
        // Si es estado inicial y es la segunda vez que pasa O si es cualquier otro estado.
        else
        {
            // Si hay dos o mas simbolos, el primero es el 2 del arreglo, sino el primero es 1.
            // (El elemento 0 del arreglo es el auxiliar, no cuenta como simbolo)
            int numFirstSymbol = symbols.childCount >= 3 ? 2 : 1;
            nameFirstSymbol = symbols.GetChild(numFirstSymbol).name;
            //Transform cardSymbol = uiString.GetChild(uiString.childCount - (symbols.childCount - 1));
            Vector3 posFirstSymbol = symbols.GetChild(1).position;

            // Se destruye el primer simbolo de la cadena y se pone animacion.            
            Destroy(symbols.GetChild(1).gameObject);

            // Si solo quedan un simbolo en la cadena, se muetra un mensaje y se finaliza.
            if (numFirstSymbol == 1)
            {
                // Si es estado final.
                if (isStateFinal)
                {
                    //cardSymbol.GetComponent<Image>().color = uiSymbolColorDelete;
                    showAnimation(symbols, destructAnim, posFirstSymbol, destructAnimTime);
                    // Se almacena la solucion del usaurio y se muestra mensaje de ganador.
                    modManager.SaveSolution();                        
                }                        
                // Si no es estado final, se muestra mensaje de perdedor.
                else
                    uIMessagesManager.ShowLoser();

                // Se finaliza.
                return;

            }
            // Si queda mas de un simbolo en la cadena, se pone animacion.
            else
            {
                //cardSymbol.GetComponent<Image>().color = uiSymbolColorDelete;
                showAnimation(symbols, destructAnim, posFirstSymbol, destructAnimTime);
            }
        }

        // Se obtine la transicion siguiente, segun el primer simbolo de la cadena.
        SplineContainer splineContainer = null;
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

    private void OnTriggerExit(Collider other)
    {
        // Cada que sale el simbolo auxiliar de un esatdo,
        // Se actualiza su posicion al del primer simbolo de la cadena.
        if (other.name.Equals(Constants.instance.SYMBOL_UAX_NAME))
        {
            StartCoroutine(UpdatePositionAux(other.transform));
        }
    }


    /// <summary>
    /// Actualiza la posicion del simbolo auxiliar al del primer simbolo de la cadena.
    /// </summary>
    /// <param name="symbolE">Simbolo auxiliar</param>
    /// <returns></returns>
    public IEnumerator UpdatePositionAux(Transform symbolE)
    {
        TransitionMove transitionMove = symbolE.parent.GetChild(1).GetComponent<TransitionMove>();
        //Debug.Log(nivelManager.symbolTimeout - (1f / transitionMove.speed));
        yield return new WaitForSeconds(modManager.symbolTimeout);// - (1f / transitionMove.speed));
        symbolE.GetComponent<TransitionMove>().CopyValuesTo(transitionMove);        
    }

    public void showAnimation(Transform symbols, GameObject prefabAnimation, Vector3 position, 
        float destructionTime)
    {
        // Se cambia el color del simbolo.
        Transform cardSymbol = uiString.GetChild(uiString.childCount - (symbols.childCount - 1));
        cardSymbol.GetComponent<Image>().color = uiSymbolColorDelete;
        //uiString.DOShakeScale(uiSymbolAnimTime);
        uiString.DOPunchScale(new Vector3(0.3f, 0.3f, 0f), uiSymbolAnimTime, 0);
        //StartCoroutine(nameof(delayCamera));

        // Se ponen particulas.
        GameObject anim = Instantiate(prefabAnimation);
        anim.transform.position = position;
        Destroy(anim, destructionTime);
    }

    //public IEnumerator delayCamera()
    //{
    //    Transform follow = CameraController.instance.followTransform;
    //    CameraController.instance.followTransform = null;
    //    yield return new WaitForSeconds(camDestructAnimTime);
    //    CameraController.instance.followTransform = follow;
    //}
}
