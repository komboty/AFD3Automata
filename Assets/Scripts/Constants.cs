using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script que sirve para eliminar hardcore.
/// </summary>
public class Constants : MonoBehaviour
{
    /// <summary>
    /// Nombres de Escenas.
    /// </summary>
    //public string nameSceneMenu = "MainMenu";
    public string SceneNameNivelTest = "Nivel Test";

    /// <summary>
    /// Valores de la camara.
    /// </summary>
    public float CameraNormalSpeed = 0.5f;
    public float CameraFastSpeed = 3f;
    public float CameraMovementSpeed = 1f;
    public float CameraMovementTime = 5f;
    public float CameraRotationAmount = 1f;
    public Vector3 CameraZoomAmount = new Vector3(0f, -5f, 5f);

    /// <summary>
    /// Nombres de Inputs.
    /// </summary>
    //public string nameInputX = "Horizontal";
    //public string nameInputZ = "Vertical";
    //public string nameInputBtn1 = "Fire1";
    //public string nameInputBtn2 = "Fire2";
    //public string nameInputBtn3 = "Fire3";

    /// <summary>
    /// Tags.
    /// </summary>    
    //public string tagSymbol = "Symbol";
    //public string tagPlatform = "Platform";
    //public string tagWaypoint = "Waypoint";
    public string tagString = "String";

    /// </summary>
    /// Valores default del jugador.
    /// </summary>
    //public float speedPlayer = 9f;
    //public float limitDirectionMovePlayer = 0.1f;
    //public float turnSmoothTimePlayer = 0.1f;

    //public float speedWaypoints = 4f;

    /// <summary>
    /// Simbolos.
    /// </summary>
    public string SymbolEpsilonName = "E";
    //public string SymbolGreen = "G";
}
