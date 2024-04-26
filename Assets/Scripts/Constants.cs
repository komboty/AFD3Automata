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
    public string MESSAGES_MAX_SYMBOLS = "Solo puedes poner\n{0} S�mbolos";
    public string MESSAGES_WINNER = "Felicidades\nGanaste!!!";
    public string MESSAGES_LOSER = "No t�rmino en un\nEstado Final\nInt�ntalo otra vez";
    public string MESSAGES_NO_EMPTY_SYMBOLS = "Debes poner al\nmenos un S�mbolos";
    public string MESSAGES_NO_EMPTY_TRANSITIONS = "Debes conectar\ntodas las Transiciones";
    public string MESSAGES_REPEATED_STRING = "Ya hiciste esa Cadena\nIntent� con otros S�mbolos";
    public string MESSAGES_REPEATED_AUTOMATA = "Ya hiciste este Automata\nIntent� con otras Transiciones";

    /// <summary>
    /// Nombres de Escenas.
    /// </summary>
    public string NAME_SCENE_MAIN_MENU = "MainMenu";
    public string NAME_SCENE_MOD1_STATES3 = "Mod1States3";
    public string NAME_SCENE_MOD2_STATES2 = "Mod2States2";

    /// <summary>
    /// Tags.
    /// </summary>
    public string TAG_MAIN_CAMERA = "MainCamera";
    public string TAG_TRANSITIONS = "Transitions";
    public string TAG_PHATS_CANVAS = "PathsCanvas";

    /// <summary>
    /// Simbolos.
    /// </summary>
    public string SYMBOL_UAX_NAME = "E";
    public string SYMBOL_A_NAME = "A";
    public string SYMBOL_B_NAME = "B";

    /// <summary>
    /// Valores del juego
    /// </summary>
    public int GAME_MAX_SYMBOLS = 6;
    public string GAME_MOD2_STRING = "";

    /// <summary>
    /// Singleton
    /// </summary>
    public static Constants instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
