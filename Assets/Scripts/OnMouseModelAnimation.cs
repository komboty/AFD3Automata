using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script que maneja los eventos del mouse del objeto que tenga este script.
/// </summary>
public class OnMouseModelAnimation : MonoBehaviour
{
    // Valores de animacion
    public float doScaleSizeEnter = 1.2f;
    public float doScaleSizeExit = 1f;
    public float doScaleTime = 0.2f;
    // Objeto a poner animacion.
    public Transform model;
    // Se quiere animar?
    public bool isAnimated = false;

    private void OnMouseEnter()
    {
        if (isAnimated)
            model.DOScale(doScaleSizeEnter, doScaleTime);
    }

    private void OnMouseExit() 
    {
        ExitAnimation();
    }

    public void ExitAnimation()
    {
        model.DOScale(doScaleSizeExit, doScaleTime);
    }
}
