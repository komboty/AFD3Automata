using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script que controla las estadisticas del jugador. (Modo 2 de juego)
/// </summary>
public class UIMod2Rewards : UIMod1Rewards
{
    public override void InitRewards()
    {
        //Debug.Log("UIMod2Rewards");
        string levelName = SceneManager.GetActiveScene().name;
        TextScroll.text = UserData.instance.GetMod2Solutions(levelName, Constants.instance.GAME_MOD2_STRING);
        TextScore.text = UserData.instance.GetMod2Score(levelName, Constants.instance.GAME_MOD2_STRING).ToString();
        TextCount.text = UserData.instance.GetMod2CountSolutions(levelName, Constants.instance.GAME_MOD2_STRING).ToString();
    }
}
