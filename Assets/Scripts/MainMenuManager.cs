using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public Transform buttonPlay;
    public Transform title;
    public float timeBtnPlay = 1f;
    public float timeTitle = 2f;
    public float delayTitle = 2f;

    void Start()
    {
        buttonPlay.DOPunchScale(new Vector3(0.3f, 0.3f, 0f), timeBtnPlay, 0, 1f)
            .SetLoops(-1);

        //DOTween.Sequence().SetDelay(delayTitle)
        //    .Append(
        //        title.transform.DOMove(new Vector3(1.32f, title.transform.position.y - 1.5f, 1.25f), timeTitle)
        //            .OnComplete(() => title.transform.DOMove(new Vector3(1.32f, title.transform.position.y - 0.5f, 1.25f), timeTitle / 2)
        //                .OnComplete(() => title.transform.DOMove(new Vector3(1.32f, title.transform.position.y + 0.5f, 1.25f), timeTitle / 2))
        //                )
        //    )
        //    .AppendInterval(delayTitle)
        //    .SetLoops(-1, LoopType.Yoyo);

        StartCoroutine(nameof(AnimationTitle));
    }

    public IEnumerator AnimationTitle()
    {
        while (true)
        {            
            title.transform.DOMove(new Vector3(1.32f, 0.1f, 1.25f), timeTitle)
                    .OnComplete(() => title.transform.DOMove(new Vector3(1.32f, 0.5f, 1.25f), timeTitle));
            yield return new WaitForSeconds(delayTitle + (timeTitle * 2));
            title.transform.DOMove(new Vector3(1.32f, 2.01f, 1.25f), timeTitle);
            yield return new WaitForSeconds((delayTitle / 4) + timeTitle);
        }
    }
}
