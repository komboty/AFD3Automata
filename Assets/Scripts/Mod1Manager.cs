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
            uiMessages.ShowMessage(Constants.instance.MESSAGE_REPEATED_STRING);
        
        return containsString;
    }

    public override void SaveSolution()
    {
        // Se guarda la cadena
        string word = GetString();
        string levelName = SceneManager.GetActiveScene().name;
        UserData.instance.Mod1Solutions[levelName].Add(word);

        // Se muestra mensaje.
        int levelScore = UserData.instance.GetLastSolutionMod1(levelName).Length;
        int oldScore = UserData.instance.GetMod1Score(levelName) - levelScore;
        uiMessages.ShowWinner(Constants.instance.MESSAGE_WINNER_MOD1, oldScore, levelScore, 1);
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
