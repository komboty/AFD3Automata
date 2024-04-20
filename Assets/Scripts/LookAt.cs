using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LookAt : MonoBehaviour
{
    public Constants constants;
    public GameObject target;
    //public float duration = 0.5f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(constants.tagMainCamera);
    }

    void Update()
    {        
        transform.LookAt(target.transform.position);
        //Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        //transform.DORotateQuaternion(targetRotation, duration);
    }
}
