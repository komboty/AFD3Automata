using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script que maneja el Modo 2 del juego (Dada una cadena construir un Automata que pueda la valide).
/// </summary>
public class Mod2Manager : ModManager
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
                uiMessages.ShowMessage(Constants.instance.MESSAGES_NO_EMPTY_TRANSITIONS);
                return;
            }  
        }

        // Si el usaurio ya hizo el automata anteriormente, se manda error.
        if (IsRepeatedSolution())
            return;

        // Se ocultan elementos de la pantalla.
        uiButtons.GetChild(2).gameObject.SetActive(false);
        statesManager.CanvasesSetActive(false);

        // Se inicia el automata.
        base.StartAutomata();
    }

    public override bool IsRepeatedSolution()
    {
        string levelName = SceneManager.GetActiveScene().name;
        bool containsAutomata = UserData.instance.HasMod2Solution(levelName,
            Constants.instance.GAME_MOD2_STRING, UserData.instance.GetSolutionMod2());
        // Si ya se hizo el automata, se muestra mensaje de error.
        if (containsAutomata)
            uiMessages.ShowMessage(Constants.instance.MESSAGES_REPEATED_AUTOMATA);

        return containsAutomata;
    }

    public override void SaveSolution()
    {
        // Se guarda el automata            
        string levelName = SceneManager.GetActiveScene().name;
        UserData.instance.Mod2Solutions[levelName][Constants.instance.GAME_MOD2_STRING]
            .Add(UserData.instance.GetSolutionMod2());

        // Se muestra mensaje.
        int newScore = 10 * Constants.instance.GAME_MOD2_STRING.Length;
        int oldScore = UserData.instance.GetMod2Score(levelName, Constants.instance.GAME_MOD2_STRING) - newScore;
        uiMessages.ShowWinner(oldScore.ToString(), newScore.ToString());
    }
}
