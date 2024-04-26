using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

/// <summary>
/// Script que controla las estadisticas del jugador. (Modo 1 de juego)
/// </summary>
public class UIMod1Rewards : MonoBehaviour
{
    public Transform uiRewards;
    public float uiRewardsTime = 0.2f;

    public TextMeshProUGUI TextScroll;
    public TextMeshProUGUI TextScore;

    private bool isShowPanel = false;

    private void Start()
    {
        InitRewards();
    }

    /// <summary>
    /// Inicia las estadisticas.
    /// </summary>
    public virtual void InitRewards()
    {
        //Debug.Log("UIMod1Rewards");
        string levelName = SceneManager.GetActiveScene().name;
        TextScroll.text = UserData.instance.GetMod1Solutions(levelName);
        TextScore.text = UserData.instance.GetMod1Score(levelName).ToString();
    }

    /// <summary>
    /// Muestra las estadisticas en pantalla.
    /// </summary>
    public void OnShow()
    {
        if (isShowPanel)
            uiRewards.DOLocalMoveX(71f, uiRewardsTime);
        else
            uiRewards.DOLocalMoveX(-86f, uiRewardsTime);

        isShowPanel = !isShowPanel;
    }
}
