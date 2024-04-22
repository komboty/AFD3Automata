using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesManager : MonoBehaviour
{
    // Constantes del juego.
    public Constants constants;

    public List<Transform> states;
    //public List<GameObject> models;
    public List<GameObject> transitions;
    public List<GameObject> canvases;

    void Start()
    {
        //canvases = GameObject.FindGameObjectsWithTag(constants.TAG_PHATS_CANVAS);
        foreach (Transform state in states)
        {
            //models.Add(state.GetChild(0).GetChild(0).gameObject);
            transitions.Add(state.GetChild(1).gameObject);
            canvases.Add(state.GetChild(2).gameObject);
        }
    }

    public void CanvasesSetActive(bool value)
    {   
        foreach (GameObject canva in canvases)
            canva.SetActive(value);
    }

    public void ModelsAnimationSetActive(bool value)
    {
        foreach (GameObject transition in transitions)
            transition.GetComponent<OnMouseModelAnimation>().isAnimated = value;
    }

    public void CanvasesSwitchActive()
    {
        foreach (GameObject canva in canvases)
            canva.SetActive(!canva.activeSelf);
    }
}
