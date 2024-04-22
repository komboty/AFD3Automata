using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Script que realiza rotar al objeto que tenga este script hacia la camara principal.
/// </summary>
public class LookAt : MonoBehaviour
{
    // Constantes del juego.
    public Constants constants;    
    //public float duration = 0.5f;
    // El objetivo esta rotado?.
    public bool isRotated;
    // Objetivo a seguir.
    private GameObject target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(constants.TAG_MAIN_CAMERA);
    }

    void Update()
    {
        // Si el objetivo esta rotado.
        if (isRotated)
            transform.LookAt(target.transform.position);
        else
            transform.forward = target.transform.forward; 

        //Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        //transform.DORotateQuaternion(targetRotation, duration);
    }
}
