using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script que almacena los datos del usuario (puntaje).
/// </summary>
public class UserData : MonoBehaviour
{
    /// <summary>
    /// Puntos del jugador. 
    /// </summary>
    //public List<string> Mod1States3_Strings = new List<string>();
    public List<string> Mod1States3_Strings = new List<string>();

    // Falta que se definan los simbolos para cada nivel. Ej 1=(A,B), 2=(AA,BB,AB,BA), etc.
    // La primer llave contiene el numero maximo de simbolos,
    // la segunda llave contiene los simbolos y el valor las transiciones que hizo.
    //public Dictionary<int, Dictionary<string, List<string>>> Mod2States2_Strings;

    /// <summary>
    /// Singleton
    /// </summary>
    public static UserData instance;
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
