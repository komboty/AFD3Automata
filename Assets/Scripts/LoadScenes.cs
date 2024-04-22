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
    // Constantes del juego.
    public Constants constants;

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

    // Carga la escena con el Modo 1 con 3 Estados.
    public void LoadMod1State3() => Restart(constants.NAME_SCENE_MOD1_STATES3);

    // Carga la escena con el Modo 2 con 2 Estados.
    public void LoadMod2State2() => Restart(constants.NAME_SCENE_MOD2_STATES2);
}
