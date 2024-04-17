using DG.Tweening;
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
    //// Cadena a mover por las transiciones.
    //public Transform stringMove;
    // Contenedor de simbolos (cadena).
    public Transform stringSymbols;
    //Tiempo para moverse entre cada symbolo.
    public float symbolTimeout;
    // Transicion inicial.
    public SplineContainer transitionInitial;
    // Cadena hecha por el usuario.
    public Transform uiString;
    // Boton de play.
    public GameObject btnPlay;
    // Prefabs del simbolo E.
    public GameObject symbolE;
    // Prefabs de cada simbolo del alfabeto del automata.
    public List<GameObject> symbolsModel;

    public void StartAutomata()
    {
        // Si ya se inicio el automata. No hacer nada.
        if (!uiString.parent.GetChild(2).gameObject.activeSelf)
            return;

        // Se crean los modelos 3d de cada simbolo.
        for (int i = 0; i < uiString.childCount; i++)
        {
            Transform uiSymbol = uiString.GetChild(i);
            // Se busca el modelo 3d del symbolo.
            foreach (GameObject symbolModel in symbolsModel)
            {
                // Se crea el symbolo.
                if (symbolModel.name.Equals(uiSymbol.name))
                    CreateSymbol(symbolModel);
            }
        }

        // Se crea el symbolo E.
        GameObject lastSymbol = CreateSymbol(symbolE);

        // Se sigue a la cadena con la camara.
        CameraController.instance.followTransform = lastSymbol.transform;

        // Se ocultan elementos de la interfaz de usaurio.
        //for (int i = 1; i < uiString.parent.childCount; i++)
        uiString.parent.GetChild(2).gameObject.SetActive(false);
        btnPlay.GetComponent<UIPointerAnimation>().enabled = false;

        // Se desabilita el drag de los symbolos de la interfaz de usuario.
        for (int i = 0; i < uiString.childCount; i++)
        {
            uiString.GetChild(i).GetComponent<UISymbolDrag>().enabled = false;
            uiString.GetChild(i).GetComponent<UIPointerAnimation>().enabled = false;
        }

        // Se inicia el automata
        StartCoroutine(nameof(MoveSymbols));
    }

    //public void StartAutomata()
    //{
    //    // Si ya se inicio el automata. No hacer nada.
    //    if (!uiString.parent.gameObject.activeSelf)
    //    {
    //        return;
    //    }

    //    // Auxiliar para la creacion de un simbolo.
    //    GameObject symbolPrefa = null;        
    //    //CreateSymbol(symbolE, constants.SymbolEpsilonName);

    //    for (int i = 0; i < uiString.childCount; i++)
    //    {
    //        Transform uiSymbol = uiString.GetChild(i);
    //        // Se busca el modelo 3d del symbolo.
    //        foreach (GameObject symbol in symbolsAlphabet)
    //        {
    //            if (symbol.name.Equals(uiSymbol.name))
    //            {
    //                symbolPrefa = symbol;
    //            }
    //        }
    //        // Se crea el symbolo.            
    //        CreateSymbol(symbolPrefa, uiSymbol.name);
    //    }

    //    // Se crea el symbolo E.
    //    GameObject lastSymbol = CreateSymbol(symbolE, constants.SymbolEpsilonName);

    //    // Se inicia el automata.
    //    StartCoroutine(nameof(MoveSymbols));

    //    CameraController.instance.followTransform = lastSymbol.transform;

    //    // Se ocultan elementos de la interfaz de usaurio.
    //    for (int i = 1; i < uiString.parent.childCount; i++)
    //    {
    //        uiString.parent.GetChild(i).gameObject.SetActive(false);
    //    }
    //    // Se elimina el drag de los symbolos de la interfaz de usuario.
    //    for (int i = 0; i < uiString.childCount; i++)
    //    {
    //        Destroy(uiString.GetChild(i).GetComponent<UISymbolDrag>());
    //    }
    //}

    public GameObject CreateSymbol(GameObject symbolModel)
    {
        GameObject newSymbol;
        newSymbol = Instantiate(symbolModel);
        newSymbol.name = symbolModel.name;
        newSymbol.transform.SetParent(stringSymbols);
        return newSymbol;
    }

    public IEnumerator MoveSymbols()
    {        
        for (int i = 0; i < stringSymbols.childCount; i++)
        {
            TransitionMove transitionMove = stringSymbols.GetChild(i).GetComponent<TransitionMove>();
            transitionMove.AddSplineContainer(transitionInitial);
            transitionMove.UpdateValues();
            transitionMove.isPlay = true;
            yield return new WaitForSeconds(symbolTimeout);
        }
        //Destroy(symbols.GetChild(0).gameObject);
    }

    public void Restart(string nivelName)
    {
        //DOTween.KillAll();
        //DOTween.Clear(true);
        SceneManager.LoadScene(nivelName);
    }

    //public void RestartTest()
    //{
    //    SceneManager.LoadScene(constants.SceneNameNivelTest);
    //}
}
