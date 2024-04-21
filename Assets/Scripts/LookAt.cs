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
    // Bandera que rota el objetivo.
    public bool isRotated;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(constants.TAG_MAIN_CAMERA);
    }

    void Update()
    {
        if (isRotated)
            transform.LookAt(target.transform.position);
        else
            transform.forward = target.transform.forward; 

        //Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        //transform.DORotateQuaternion(targetRotation, duration);
    }
}
