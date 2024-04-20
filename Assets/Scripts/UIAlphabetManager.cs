using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Script que controla el alfabeto en la interfaz de usuario.
/// </summary>
public class UIAlphabetManager : MonoBehaviour, IPointerClickHandler
{
    // Constantes del juego.
    public Constants constants;
    // Contenedor donde se forma la cadena.
    public GameObject uiString;
    // Panel de mensajes.
    public UIMessagesManager uiMessages;
    // Prefab de un symbolo de la interfaz de usuario.
    public GameObject prefabUISymbol;
    public int maxNumSymbols = 6;
    // Valores de animacion
    public float symbolDoScaleSize = 1.2f;
    public float symbolDoScaleTime = 0.2f;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Maximo de simbolos.        
        if (uiString.transform.childCount >= maxNumSymbols)
        {
            string message = String.Format(constants.MESSAGES_MAX_SYMBOLS, maxNumSymbols);
            uiMessages.ShowMessage(message);
            return;
        }

        // Se agrega un simbolo en la cadena (Interfaz del usuario)
        GameObject newSymbol = Instantiate(prefabUISymbol);
        newSymbol.transform.name = transform.name;
        newSymbol.transform.SetParent(uiString.transform, false);
        newSymbol.transform.DOScale(symbolDoScaleSize, symbolDoScaleTime)
            .OnComplete(() => newSymbol.transform.DOScale(1f, symbolDoScaleTime));
    }
}
