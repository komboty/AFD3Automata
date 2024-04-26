using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Activa y Desactiva el movimiento con el mouse.
/// </summary>
public class DisablingHandleMouse : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        CameraController.isActiveMouse = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CameraController.isActiveMouse = true;
    }
}
