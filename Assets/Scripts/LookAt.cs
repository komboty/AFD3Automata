using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LookAt : MonoBehaviour
{
    // Constantes del juego.
    public Constants constants;
    // Objetivo a seguir.
    public GameObject target;
    //public float duration = 0.5f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(constants.TAG_MAIN_CAMERA);
    }

    void Update()
    {        
        transform.LookAt(target.transform.position);
        //Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        //transform.DORotateQuaternion(targetRotation, duration);
    }
}
