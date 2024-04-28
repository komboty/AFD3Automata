using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script que carga escenas.
/// </summary>
public class LoadScenes : MonoBehaviour
{
    /// <summary>
    /// Recarga una escena
    /// </summary>
    /// <param name="sceneName">Nombre de la escena</param>
    public void Restart(string sceneName)
    {
        //DOTween.Clear(true);
        DOTween.KillAll(true);
        SceneManager.LoadScene(sceneName);
    }

    // Recarga la escena con el Modo 1 con 3 Estados.
    public void LoadMod1State3() => Restart(Constants.instance.NAME_SCENE_MOD1_STATES3);

    // Recarga la escena con el Modo 1 con 2 Estados.
    public void LoadMod1State2() => Restart(Constants.instance.NAME_SCENE_MOD1_STATES2);

    // Recarga la escena con el Modo 2 con 2 Estados.
    public void LoadMod2State2() => Restart(Constants.instance.NAME_SCENE_MOD2_STATES2);

    // Recarga la escena con el Modo 2 con 2 Estados Version 2.
    public void LoadMod2State2_2() => Restart(Constants.instance.NAME_SCENE_MOD2_STATES2_2);

    // Recarga la escena con el Menu principal.
    public void LoadMainMenu() => Restart(Constants.instance.NAME_SCENE_MAIN_MENU);


    //// Inicia la escena con el Modo 1 con 3 Estados.
    //public void StartMod1State3(int maxSymbols)
    //{
    //    Constants.instance.GAME_MAX_SYMBOLS = maxSymbols;
    //    LoadMod1State3();
    //}

    //// Inicia la escena con el Modo 2 con 2 Estados.
    //public void StartMod2State2(string word)
    //{
    //    Constants.instance.GAME_MOD2_STRING = word;
    //    LoadMod2State2();
    //}

    //// Inicia la escena con el Modo 2 con 2 Estados version 2.
    //public void StartMod2State2_2(string word)
    //{
    //    Constants.instance.GAME_MOD2_STRING = word;
    //    LoadMod2State2_2();
    //}
}
