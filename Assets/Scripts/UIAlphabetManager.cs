using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Script que controla el alfabeto en la interfaz de usuario.
/// </summary>
public class UIAlphabetManager : MonoBehaviour, IPointerClickHandler
{
    

    // Contenedor donde se forma la cadena.
    public GameObject UIstring;
    // Prefab de un symbolo de la interfaz de usuario.
    public GameObject prefabUISymbol;
    // Valores de animacion
    public float symbolDoScaleSize = 4f;
    public float symbolDoScaleTime = 0.25f;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject newSymbol = Instantiate(prefabUISymbol);
        newSymbol.transform.name = transform.name;
        UnityEditor.GameObjectUtility.SetParentAndAlign(newSymbol, UIstring);
        //newSymbol.transform.DOLocalMoveY(doScaleSize2, doScaleTime2);
        newSymbol.transform.DOScale(symbolDoScaleSize, symbolDoScaleTime);
        newSymbol.transform.DOScale(1f, symbolDoScaleTime);
    }
}
