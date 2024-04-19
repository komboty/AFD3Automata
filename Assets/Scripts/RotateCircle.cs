using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotate : MonoBehaviour
{
    public float time = 1800f;

    void Start()
    {
        transform.DORotate(new Vector3(0f, 3600f, 0f), time, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart);
    }
}
