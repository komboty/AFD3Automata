using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    // Constantes del juego.
    public Constants constants;

    public void Restart(string nivelName)
    {
        //DOTween.Clear(true);
        DOTween.KillAll(true);
        SceneManager.LoadScene(nivelName);
    }

    public void LoadMod1State3() => Restart(constants.NAME_SCENE_MOD1_STATES3);
}
