using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPointerAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Valores de animacion
    public float doScaleSizeEnter = 1.2f;
    public float doScaleSizeExit = 1f;
    public float doScaleTime = 0.2f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(doScaleSizeEnter, doScaleTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(doScaleSizeExit, doScaleTime);
    }
}
