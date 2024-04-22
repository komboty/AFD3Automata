using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseModelAnimation : MonoBehaviour
{
    // Valores de animacion
    public float doScaleSizeEnter = 1.2f;
    public float doScaleSizeExit = 1f;
    public float doScaleTime = 0.2f;
    public Transform model;
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
