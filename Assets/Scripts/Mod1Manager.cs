using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;

/// <summary>
/// Script que maneja el Modo 1 del juego (Dado un automata poner una cadena valida).
/// </summary>
public class Mod1Manager : ModManager
{
    public override void StartAutomata()
    {
        // Si el usaurio ya hizo la cadena anteriormente, se manda error.
        if (IsRepeatedSolution())
            return;

        // Se inicia el automata.
        base.StartAutomata();
    }

    public override bool IsRepeatedSolution()
    {
        string word = GetString();
        string levelName = SceneManager.GetActiveScene().name;
        bool containsString = UserData.instance.HasMod1Solution(levelName, word);
        // Si ya hizo la cadena, se muestra mensaje de error.
        if (containsString)
            uiMessages.ShowMessage(Constants.instance.MESSAGES_REPEATED_STRING);
        
        return containsString;
    }

    public override void SaveSolution()
    {
        // Se guarda la cadena
        string word = GetString();
        string levelName = SceneManager.GetActiveScene().name;
        UserData.instance.Mod1Solutions[levelName].Add(word);

        // Se muestra mensaje.
        string lastString = UserData.instance.GetLastSolutionMod1(levelName);
        int oldScore = UserData.instance.GetMod1Score(levelName) - lastString.Length;
        uiMessages.ShowWinner(oldScore.ToString(), lastString.Length.ToString());
    }

    /// <summary>
    /// Obtiene la cadena hecha por el usaurio.
    /// </summary>
    /// <returns>Cadena</returns>
    private string GetString()
    {
        string word = "";
        foreach (Transform symbol in uiString)
            word += symbol.name;
        return word;
    }
}
