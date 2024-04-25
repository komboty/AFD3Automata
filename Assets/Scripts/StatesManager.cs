using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script que maneja un todos los estados.
/// </summary>
public class StatesManager : MonoBehaviour
{
    // Estados.
    public List<Transform> states;
    // Elementos de un estado.
    //public List<GameObject> models;
    public List<GameObject> transitions;
    public List<GameObject> canvases;

    void Start()
    {
        foreach (Transform state in states)
        {
            //models.Add(state.GetChild(0).GetChild(0).gameObject);
            transitions.Add(state.GetChild(1).gameObject);
            canvases.Add(state.GetChild(2).gameObject);
        }
    }

    /// <summary>
    /// Activa o Desactiva los canvas de todos los estados.
    /// </summary>
    /// <param name="value">Valor para activar o desactivar</param>
    public void CanvasesSetActive(bool value)
    {   
        foreach (GameObject canva in canvases)
            canva.SetActive(value);
    }

    /// <summary>
    /// Activa o Desactiva las animaciones de los modelos de todos los estados.
    /// </summary>
    /// <param name="value">Valor para activar o desactivar</param>
    public void ModelsAnimationSetActive(bool value)
    {
        foreach (GameObject transition in transitions)
            transition.GetComponent<OnMouseModelAnimation>().isAnimated = value;
    }

    /// <summary>
    /// Cambia entre activar o desactivar los canvas de todos los estados.
    /// </summary>
    public void CanvasesSwitchActive()
    {
        foreach (GameObject canva in canvases)
            canva.SetActive(!canva.activeSelf);
    }
}
