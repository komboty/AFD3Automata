using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;

/// <summary>
/// Clase que sirve como base para cambiar de modo de juego.
/// </summary>
public class ModManager: MonoBehaviour
{
    // Panel de mensajes.
    public UIMessagesManager uiMessages;
    // Contenedor de simbolos (cadena).
    public Transform symbols;
    //Tiempo para moverse entre cada symbolo.
    public float symbolTimeout = 0.4f;
    // Transicion inicial.
    public SplineContainer transitionInitial;
    // Cadena hecha por el usuario.
    public Transform uiString;
    // Botones.
    public Transform uiButtons;
    public GameObject uiButtonReturn;
    // Recompensas.
    public GameObject uiRewards;
    // Prefabs del simbolo E.
    public GameObject symbolE;
    // Prefabs de cada simbolo del alfabeto del automata.
    public List<GameObject> symbolsModel;

    /// <summary>
    /// Inicia el automata.
    /// </summary>
    public virtual void StartAutomata()
    {
        // Si el usaurio ya gano anteriormente con la misma solucion, No se inica el automata.
        //if (IsRepeatedSolution())
        //    return;

        // Si el usaurio no ha puesto ningun simbolo, No se inica el automata y se manda mensaje de error.
        if (uiString.childCount == 0)
        {
            uiMessages.ShowMessage(Constants.instance.MESSAGES_NO_EMPTY_SYMBOLS);
            return;
        }

        // Objeto auxiliar para saber donde inicia la cadena
        CreateSymbol(symbolE);

        // Se crean los modelos 3d de cada simbolo.
        foreach (Transform uiSymbol in uiString)
            foreach (GameObject symbolModel in symbolsModel)
                if (symbolModel.name.Equals(uiSymbol.name))
                    CreateSymbol(symbolModel);
                
        // Se sigue a la cadena con la camara.
        CameraController.instance.followTransform = symbols.GetChild(0);

        // Se ocultan elementos de la interfaz de usaurio.
        uiString.parent.GetChild(2).gameObject.SetActive(false);
        uiButtons.GetChild(0).gameObject.SetActive(false);
        uiButtonReturn.SetActive(false);
        uiRewards.SetActive(false);

        // Se desabilita el drag de los symbolos de la interfaz de usuario.
        for (int i = 0; i < uiString.childCount; i++)
        {
            uiString.GetChild(i).GetComponent<UISymbolDrag>().enabled = false;
            uiString.GetChild(i).GetComponent<UIPointerAnimation>().enabled = false;
        }

        // Se incia el movimiento del los simbolos por el automata.
        StartCoroutine(nameof(MoveSymbols));

    }

    /// <summary>
    /// Validad si la solucion ya la hizo el usuario anteriormente. Y muestra el mensaje.
    /// </summary>
    /// <returns>True si existe la solucion en los datos del usuario, de lo contrario False</returns>
    public virtual bool IsRepeatedSolution() => true;

    /// <summary>
    /// Almacena la solucion del usuario.
    /// </summary>
    public virtual void SaveSolution() { }

    /// <summary>
    /// Crea un simbolo que recorrera el automata.
    /// </summary>
    /// <param name="symbolModel">Modelo del simbolo</param>
    /// <returns>Simbolo</returns>
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

    /// <summary>
    /// Inicia el moviemnto de los simbolos por el automata.
    /// </summary>
    /// <returns></returns>
    public IEnumerator MoveSymbols()
    {
        // Se agrega la transicion inicial a los simbolos.
        for (int i = 0; i < symbols.childCount; i++)
        {
            TransitionMove transitionMove = symbols.GetChild(i).GetComponent<TransitionMove>();
            transitionMove.AddSplineContainer(transitionInitial);
            transitionMove.UpdateValues();
        }
        // Se inicia el moviemnto por el automata.
        for (int i = 0; i < symbols.childCount; i++)
        {
            symbols.GetChild(i).GetComponent<TransitionMove>().isPlay = true;
            yield return new WaitForSeconds(symbolTimeout);
        }
    }
}
