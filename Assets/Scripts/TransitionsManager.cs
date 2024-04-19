using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

/// <summary>
/// Script que controla las transiciones de un estado.
/// </summary>
public class TransitionsManager : MonoBehaviour
{
    // Constantes del juego.
    public Constants constants;
    // Es estado incial?.
    public bool isStateInitial = false;
    private bool auxStateInitial = true;
    //Es estado final?.
    public bool isStateFinal = false;
    //// Mensaje de ganador.
    //public GameObject uiWinner;
    //// Mensaje de perdedor.
    //public GameObject uiLoser;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        //// Si la cadena entro al estado.
        if (other.name.Equals(constants.SymbolEpsilonName))
        {
            // Se pone el numero del primer simbolo de la cadena
            // Numeros de hijos (0 Objeto auxilar) (1 El simbolo a destruir)
            //int numFirstChild = 2;
            Transform symbols = other.transform.parent;
            int numFirstSymbol = 0;
            string nameFirstSymbol;

            // Si es estado inicial y es la primera vez, no se destruye un simbolo. 
            if (auxStateInitial && isStateInitial)
            {
                //Y el primer simbolo es el 1.                
                //numFirstChild = 1;
                nameFirstSymbol = symbols.GetChild(1).name;
                auxStateInitial = false;
            }
            // Si es estado inicial y es la segunda vez que pasa o si es cualquier otro estado.
            else
            {
                // Si hay tres o mas simbolos, el primero es el 2 del arreglo, sino el primero es 1.
                numFirstSymbol = symbols.childCount >= 3 ? 2 : 1;
                nameFirstSymbol = symbols.GetChild(numFirstSymbol).name;
                // Se destruye el primer simbolo de la cadena.
                Destroy(symbols.GetChild(1).gameObject);

                if (numFirstSymbol == 1)
                {
                    if (isStateFinal)
                    {
                        Debug.Log(symbols.childCount);
                        //uiWinner.SetActive(true);
                        Debug.Log("Ganaste");
                    }
                    else
                    {
                        Debug.Log("Perdiste");
                        //uiLoser.SetActive(true);
                    }
                    return;
                }
            }            

            Debug.Log(nameFirstSymbol);
            //AddTransition(symbols, firstSymbol);
            // Se obtiene el primer simbolo de la acdena.
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

            //Si el estado es final.
            //if (isStateFinal && numFirstSymbol == 1)
            //{
            //    Debug.Log(symbols.childCount);
            //    //uiWinner.SetActive(true);
            //    Debug.Log("Ganaste");
                
            //}
            //else
            //{
            //    Debug.Log("Perdiste");
            //    //uiLoser.SetActive(true);
            //}

        }
    }

    /// <summary>
    /// Agrega la siguiente transicion a todos los simbolos.
    /// </summary>
    /// <param name="symbols">Objeto que contiene todos los simbolos</param>
    /// <param name="numFirstChild">Numero del primer simbolo</param>
    //public void AddTransition(Transform symbols, Transform firstSymbol)//int numFirstChild)
    //{
    //    // Se obtiene el primer simbolo de la acdena.
    //    //Transform firstSymbol = symbols.GetChild(numFirstChild);
    //    SplineContainer splineContainer = null;

    //    // Se obtine la transicion siguiente, segun el primer simbolo de la cadena.
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        Transform transition = transform.GetChild(i);
    //        //Debug.Log(transition.name + " " + firstSymbol.name);

    //        // Si el primer simbolo de la cadena es igual al de la transicion.
    //        if (transition.name.Equals(firstSymbol.name))
    //        {
    //            splineContainer = transition.GetChild(0).GetComponent<SplineContainer>();
    //            break;
    //        }
    //    }

    //    // Se agrega la siguiente transicion a todos los simbolos de la cadena.
    //    for (int i = 0; i < symbols.childCount; i++)
    //        symbols.GetChild(i).GetComponent<TransitionMove>()
    //            .AddSplineContainer(splineContainer);
    //}
}
