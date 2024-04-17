using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script que controla la camara.
/// </summary>
public class CameraController : MonoBehaviour
{
    // Constantes del juego.
    public Constants constants;
    // Camara.
    public static CameraController instance;
    // Seguimiento de un objeto
    public Transform followTransform;
    public Transform cameraTransform;
    // Velocidad.
    public float normalSpeed;
    public float fastSpeed;
    // Desplazamineto.
    public float movementSpeed;
    public float movementTime;
    // Rotacion.
    public float rotationAmount;
    // Zoom
    public Vector3 zoomAmount;
    // Auxiliares.
    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;
    // Mouse.
    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;
    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;

    void Start()
    {
        instance = this;
        // Se asignan valores por defecto.
        normalSpeed = constants.CameraNormalSpeed;
        fastSpeed = constants.CameraFastSpeed;
        movementSpeed = constants.CameraMovementSpeed;
        movementTime = constants.CameraMovementTime;
        rotationAmount = constants.CameraRotationAmount;
        zoomAmount = constants.CameraZoomAmount;

        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    void Update()
    {
        // Si se sigue un objetivo.
        if (followTransform != null)
        {
            transform.position = followTransform.position;
        }
        // Si NO se sigue un objetivo.
        else
        {
            // Si NO se esta moviendo un simbolo de la interfaz de usuario.
            if (UISymbolDrag.itemDragging == null)
            {
               // Movimiento de la pantalla con el mouse.
               HandleMouseInput();
            }
            // Movimiento de la pantalla con el teclado.
            HandleMovementInput();
        }

        // Se sale de la vista fijada al objetivo.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            followTransform = null;
        }
    }

    public void HandleMouseInput()
    {
        // Zoom
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }

        // Deesplazamiento
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;
            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;
            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }

        // Rotacion
        if (Input.GetMouseButtonDown(1))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            rotateCurrentPosition = Input.mousePosition;
            Vector3 difference = rotateStartPosition - rotateCurrentPosition;
            rotateStartPosition = rotateCurrentPosition;
            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }
    }

    public void HandleMovementInput()
    {
        movementSpeed = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;

        // Desplazamiento
        if (Input.GetKey(KeyCode.W))
        {
            newPosition += transform.forward * movementSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newPosition += transform.forward * -movementSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newPosition += transform.right * movementSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newPosition += transform.right * -movementSpeed;
        }

        // Rotacion
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        // Zoom
        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}
