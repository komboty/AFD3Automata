using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod2Manager : Mod1Manager
{
    public StatesCanvasManager statesCanvasManager;
    public List<Transform> transitions;    

    public override void StartAutomata()
    {
        // Se validad que todas las transiciones esten creadas.
        foreach (Transform transition in transitions)
        {            
            if (!transition.childCount.Equals(symbolsModel.Count))
            {
                // Se muestra mensaje de error.
                uiMessages.ShowMessage(constants.MESSAGES_NO_EMPTY_TRANSITIONS);
                return;
            }            
        }

        // Se ocultan elementos de la pantalla.
        uiButtons.GetChild(2).gameObject.SetActive(false);
        statesCanvasManager.DesActiveAll();

        // Se inicia el automata.
        base.StartAutomata();
    }
}
