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
    // El objetivo esta rotado?.
    public bool isRotated;
    // Objetivo a seguir.
    private GameObject target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(Constants.instance.TAG_MAIN_CAMERA);
    }

    void Update()
    {
        Vector3 relativePos = isRotated ? target.transform.position - transform.position : 
            transform.position - target.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 2f * Time.deltaTime);
    }
}
