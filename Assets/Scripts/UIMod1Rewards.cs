using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UIMod1Rewards : MonoBehaviour
{
    public Transform uiRewards;
    public float uiRewardsTime = 0.2f;

    public TextMeshProUGUI TextStrings;
    public TextMeshProUGUI TextScore;

    private bool isShowPanel = false;

    void Start()
    {
        TextStrings.text = UserData.instance.GetStrings();
        TextScore.text = UserData.instance.GetScore().ToString();
    }

    public void OnShow()
    {
        if (isShowPanel)
            uiRewards.DOLocalMoveX(71f, uiRewardsTime);
        else
            uiRewards.DOLocalMoveX(-86f, uiRewardsTime);

        isShowPanel = !isShowPanel;
    }
}
