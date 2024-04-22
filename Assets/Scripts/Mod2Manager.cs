using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script que maneja el Modo 2 del juego (Dada una cadena construir un Automata que pueda la valide).
/// </summary>
public class Mod2Manager : Mod1Manager
{
    // Estados del automata.
    public StatesManager statesManager;    

    public override void StartAutomata()
    {
        // Se validad que todas las transiciones esten creadas.
        foreach (GameObject transition in statesManager.transitions)
        {            
            if (!transition.transform.childCount.Equals(symbolsModel.Count))
            {
                // Se muestra mensaje de error.
                uiMessages.ShowMessage(constants.MESSAGES_NO_EMPTY_TRANSITIONS);
                return;
            }            
        }

        // Se ocultan elementos de la pantalla.
        uiButtons.GetChild(2).gameObject.SetActive(false);
        statesManager.CanvasesSetActive(false);

        // Se inicia el automata.
        base.StartAutomata();
    }
}
