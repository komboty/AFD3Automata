using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script que sirve para eliminar hardcore.
/// </summary>
public class Constants : MonoBehaviour
{
    /// <summary>
    /// Mensajes.
    /// </summary>
    public string MESSAGES_MAX_SYMBOLS = "Por el momento\nSolo puedes poner {0} Símbolos";
    public string MESSAGES_WINNER = "Ganaste!!!\nFelicidades";
    public string MESSAGES_LOSER = "Inténtalo otra vez";
    public string MESSAGES_NO_EMPTY_SYMBOLS = "Debes poner al\nmenos un Símbolos";
    public string MESSAGES_NO_EMPTY_TRANSITIONS = "Debes crear\ntodas las Transiciones";

    /// <summary>
    /// Nombres de Escenas.
    /// </summary>
    public string NAME_SCENE_MOD1_STATES3 = "Mod1States3";
    public string NAME_SCENE_MOD2_STATES2 = "Mod2States2";

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
    //public string tagString = "String";
    public string TAG_MAIN_CAMERA = "MainCamera";
    public string TAG_TRANSITIONS = "Transitions";
    public string TAG_PHATS_CANVAS = "PathsCanvas";

    /// <summary>
    /// Simbolos.
    /// </summary>
    public string SYMBOL_UAX_NAME = "E";
    public string SYMBOL_A_NAME = "A";
    public string SYMBOL_B_NAME = "B";
}
