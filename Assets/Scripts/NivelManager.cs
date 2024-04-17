using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;

/// <summary>
/// Script que maneja el nivel.
/// </summary>
public class NivelManager : MonoBehaviour
{
    // Constantes del juego.
    public Constants constants;
    // Cadena a mover por las transiciones.
    public Transform stringMove;
    // Contenedor de simbolos (cadena).
    public Transform stringSymbols;
    // Transicion inicial.
    public SplineContainer transitionInitial;
    // Cadena hecha por el usuario.
    public Transform uiString;
    // Prefabs del simbolo E.
    public GameObject symbolE;
    // Prefabs de cada simbolo del alfabeto del automata.
    public List<GameObject> symbolsAlphabet;

    public void StartAutomata()
    {
        // Si ya se inicio el automata. No hacer nada.
        if (!uiString.parent.gameObject.activeSelf)
        {
            return;
        }

        // Auxiliar para la creacion de un simbolo.
        GameObject symbolPrefa = null;        
        //CreateSymbol(symbolE, constants.SymbolEpsilonName);

        for (int i = 0; i < uiString.childCount; i++)
        {
            Transform uiSymbol = uiString.GetChild(i);
            // Se busca el modelo 3d del symbolo.
            foreach (GameObject symbol in symbolsAlphabet)
            {
                if (symbol.name.Equals(uiSymbol.name))
                {
                    symbolPrefa = symbol;
                }
            }
            // Se crea el symbolo.            
            CreateSymbol(symbolPrefa, uiSymbol.name);
        }

        // Se crea el symbolo E.
        GameObject lastSymbol = CreateSymbol(symbolE, constants.SymbolEpsilonName);

        // Se inicia el automata.
        StartCoroutine(nameof(MoveSymbols));

        CameraController.instance.followTransform = lastSymbol.transform;

        // Se ocultan elementos de la interfaz de usaurio.
        for (int i = 1; i < uiString.parent.childCount; i++)
        {
            uiString.parent.GetChild(i).gameObject.SetActive(false);
        }
        // Se elimina el drag de los symbolos de la interfaz de usuario.
        for (int i = 0; i < uiString.childCount; i++)
        {
            Destroy(uiString.GetChild(i).GetComponent<UISymbolDrag>());
        }
    }

    public GameObject CreateSymbol(GameObject symbolPrefa, string objectName)
    {
        GameObject newSymbol;
        newSymbol = Instantiate(symbolPrefa);
        newSymbol.name = objectName;
        newSymbol.transform.SetParent(stringSymbols);
        newSymbol.GetComponent<SplineAnimate>().Container = transitionInitial;
        return newSymbol;
    }

    public IEnumerator MoveSymbols()
    {
        Transform symbols = stringMove.GetChild(1);
        for (int i = 0; i < symbols.childCount; i++)
        {
            Debug.Log(symbols.GetChild(i).name);
            symbols.GetChild(i).GetComponent<SplineAnimate>().Restart(true);
            yield return new WaitForSeconds(0.3f);
        }
        //Destroy(symbols.GetChild(0).gameObject);
    }

    public void Restart(string nivelName)
    {
        SceneManager.LoadScene(nivelName);
    }

    public void RestartTest()
    {
        SceneManager.LoadScene(constants.SceneNameNivelTest);
    }
}
