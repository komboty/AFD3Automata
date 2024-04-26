using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script que controla el alfabeto en la interfaz de usuario. (Modo 2 de juego)
/// </summary>
public class UIAlphabetManagerMod2 : MonoBehaviour 
{
    // Contenedor donde se forma la cadena.
    public GameObject uiString;
    // Prefab de un symbolo de la interfaz de usuario.
    public List<GameObject> prefabsUISymbols;    

    void Start()
    {
        //Debug.Log("UIAlphabetManagerMod2");
        UserData.instance.SOLUTION_MOD2 = new Dictionary<string, string>();

        // Se crea la cadena seleccionada por el usuario.
        foreach (char symbolName in Constants.instance.GAME_MOD2_STRING)
            foreach (GameObject prefabUISymbol in prefabsUISymbols)
                if (prefabUISymbol.name.Equals("UI Symbol " + symbolName.ToString()))
                {
                    GameObject newSymbol = Instantiate(prefabUISymbol);
                    newSymbol.transform.name = symbolName.ToString();
                    newSymbol.transform.SetParent(uiString.transform, false);
                }
    }
}
