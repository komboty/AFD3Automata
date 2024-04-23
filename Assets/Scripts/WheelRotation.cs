using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public float time = 0.5f;
    
    void Start()
    {
        //transform.DORotate(new Vector3(0f, 360f, 0f), time, RotateMode.FastBeyond360)
        //    .SetLoops(-1, LoopType.Restart);
        transform.DORotate(new Vector3(360f, 0f, 0f), time, RotateMode.LocalAxisAdd)
            .SetLoops(-1, LoopType.Restart);
    }
}
