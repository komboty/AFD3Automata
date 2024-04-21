using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesCanvasManager : MonoBehaviour
{
    // Constantes del juego.
    public Constants constants;

    public GameObject[] canvases;

    //void Start()
    //{
    //    canvases = GameObject.FindGameObjectsWithTag(constants.TAG_PHATS_CANVAS);
    //}

    public void DesActiveAll()
    {   
        foreach (GameObject canva in canvases)
            canva.SetActive(false);
    }

    public void ActiveAll()
    {
        foreach (GameObject canva in canvases)
            canva.SetActive(true);
    }

    public void SwitchActiveAll()
    {
        foreach (GameObject canva in canvases)
            canva.SetActive(!canva.activeSelf);
    }
}
