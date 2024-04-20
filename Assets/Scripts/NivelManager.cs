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
    // Panel de mensajes.
    public UIMessagesManager uiMessages;
    // Contenedor de simbolos (cadena).
    public Transform symbols;
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

        //Debug.Log("Ini");
        //StartCoroutine(nameof(DeleteSymbols));
        //Debug.Log("Fin");

        // Objeto auxiliar para saber donde inicia la cadena
        //GameObject lastSymbol = CreateSymbol(symbolE);
        CreateSymbol(symbolE);

        // Se crean los modelos 3d de cada simbolo.        
        for (int i = 0; i < uiString.childCount; i++)
        {
            Transform uiSymbol = uiString.GetChild(i);
            // Se busca el modelo 3d del symbolo.
            foreach (GameObject symbolModel in symbolsModel)
            {
                // Se crea el symbolo.
                if (symbolModel.name.Equals(uiSymbol.name))
                    //lastSymbol = CreateSymbol(symbolModel);
                    CreateSymbol(symbolModel);
            }
        }

        // Se inicia el automata, si al menos existe un simbolo.
        if (symbols.childCount > 1)
        {
            // Se sigue a la cadena con la camara.
            CameraController.instance.followTransform = symbols.GetChild(0);

            // Se ocultan elementos de la interfaz de usaurio.
            //for (int i = 1; i < uiString.parent.childCount; i++)
            uiString.parent.GetChild(2).gameObject.SetActive(false);
            btnPlay.SetActive(false);

            // Se desabilita el drag de los symbolos de la interfaz de usuario.
            for (int i = 0; i < uiString.childCount; i++)
            {
                uiString.GetChild(i).GetComponent<UISymbolDrag>().enabled = false;
                uiString.GetChild(i).GetComponent<UIPointerAnimation>().enabled = false;
            }

            StartCoroutine(nameof(MoveSymbols));
        }
        else
        {
            // Se limpia la cedena de simbolos.
            for (int i = 0; i < symbols.childCount; i++)
                Destroy(symbols.GetChild(i).gameObject);
            // Se muestra mensaje de error.
            uiMessages.ShowMessage(constants.MESSAGES_NO_EMPTY);
        }

    }

    public GameObject CreateSymbol(GameObject symbolModel)
    {
        GameObject newSymbol;
        newSymbol = Instantiate(symbolModel);
        newSymbol.name = symbolModel.name;
        newSymbol.transform.position = symbols.transform.position;
        newSymbol.GetComponent<TransitionMove>().speed = 10f / symbolTimeout;
        newSymbol.transform.SetParent(symbols);
        return newSymbol;
    }

    public IEnumerator MoveSymbols()
    {
        for (int i = 0; i < symbols.childCount; i++)
        {
            TransitionMove transitionMove = symbols.GetChild(i).GetComponent<TransitionMove>();
            transitionMove.AddSplineContainer(transitionInitial);
            transitionMove.UpdateValues();
            //transitionMove.isPlay = true; 
            //yield return new WaitForSeconds(symbolTimeout);
        }
        for (int i = 0; i < symbols.childCount; i++)
        {
            symbols.GetChild(i).GetComponent<TransitionMove>().isPlay = true;
            yield return new WaitForSeconds(symbolTimeout);
        }
    }

    public void Restart(string nivelName)
    {
        //DOTween.Clear(true);
        DOTween.KillAll(true);
        SceneManager.LoadScene(nivelName);
    }

    //public IEnumerator DeleteSymbols()
    //{
    //    for (int i = 0; i < symbols.childCount; i++)
    //    {
    //        Destroy(symbols.GetChild(i).gameObject);
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}
}
