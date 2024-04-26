using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMod1Rewards : MonoBehaviour
{
    public Transform uiRewards;
    public float uiRewardsTime = 0.2f;

    public TextMeshProUGUI TextStrings;
    public TextMeshProUGUI TextScore;

    private bool isShowPanel = false;

    void Start()
    {
        int scoreTotal = 0;
        TextStrings.text = "";
        foreach (var word in UserData.instance.Mod1States3_Strings)
        {
            TextStrings.text += word + "\n";
            scoreTotal += word.Length;
        }
        TextScore.text = scoreTotal.ToString();
    }

    public void OnShow()
    {
        if (isShowPanel)
        {
            uiRewards.DOLocalMoveX(71f, uiRewardsTime);
        }
        else
        {
            uiRewards.DOLocalMoveX(-86f, uiRewardsTime);
        }

        isShowPanel = !isShowPanel;
    }
}
