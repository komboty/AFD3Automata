using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script que rota al objeto que tenga este script con animacion.
/// </summary>
public class CircleRotate : MonoBehaviour
{
    public float time = 150f;

    void Start()
    {
        transform.DORotate(new Vector3(0f, 360f, 0f), time, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart);
    }
}
