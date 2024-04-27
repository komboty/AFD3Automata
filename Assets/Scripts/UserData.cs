using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

/// <summary>
/// Script que almacena los datos del usuario (puntaje).
/// </summary>
public class UserData : MonoBehaviour
{
    // La primer llave contiene el nombre del nivel y el valor las cadenas que hizo el usuario.
    public Dictionary<string, List<string>> Mod1Solutions = new()
    {
        {"Mod1States2", new List<string>()},
        {"Mod1States3", new List<string>()}
    };

    
    // La primer llave contiene el nombre del nivel, la segunda llave contiene una cadena
    // y el valor las transiciones que hizo el usaurio.
    public Dictionary<string, Dictionary<string, List<string>>> Mod2Solutions = new()
    {
        {"Mod2States2", new Dictionary<string, List<string>>()
            {
                {"A", new List<string>()},{"B", new List<string>()},
                {"AA", new List<string>()},{"BB", new List<string>()},{"AB", new List<string>()},{"BA", new List<string>()},
                {"AAA", new List<string>()},{"BBB", new List<string>()},{"AAB", new List<string>()},{"ABB", new List<string>()},{"BBA", new List<string>()},{"BAA", new List<string>()},{"BAB", new List<string>()},{"ABA", new List<string>()},
            }            
        },
        {"Mod2States2_2", new Dictionary<string, List<string>>()
            {
                {"A", new List<string>()},{"B", new List<string>()},
                {"AA", new List<string>()},{"BB", new List<string>()},{"AB", new List<string>()},{"BA", new List<string>()},
                {"AAA", new List<string>()},{"BBB", new List<string>()},{"AAB", new List<string>()},{"ABB", new List<string>()},{"BBA", new List<string>()},{"BAA", new List<string>()},{"BAB", new List<string>()},{"ABA", new List<string>()},
            }
        }
    };

    // Auxiliar para almacenar la solucion del modo 2 de juego.
    public Dictionary<string, string> SOLUTION_MOD2;    

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

    /// <summary>
    /// Regresa el puntaje total de un nivel.
    /// </summary>
    /// <param name="levelName">Nombre del nivel</param>
    /// <returns>Puntaje total</returns>
    public int GetMod1Score(string levelName)
    {
        int scoreTotal = 0;
        foreach (string word in Mod1Solutions[levelName])
            scoreTotal += word.Length;
        return scoreTotal;
    }

    /// <summary>
    /// Regresa todas las soluciones hechas por el usuario de un nivel. (Modo 1 de juego)
    /// </summary>
    /// <param name="levelName">Nombre del nivel</param>
    /// <returns>Todas las soluciones hechas por el usuario</returns>
    public string GetMod1Solutions(string levelName)
    {
        string words = "";
        foreach (string word in Mod1Solutions[levelName])
            words += word + "\n";
        return words;
    }

    /// <summary>
    /// Regresa la ultima solucion hecha por el usuario de un nivel. (Modo 1 de juego)
    /// </summary>
    /// <param name="levelName">Nombre del nivel</param>
    /// <returns>Ultima solucion hecha por el usuario</returns>
    public string GetLastSolutionMod1(string levelName) => Mod1Solutions[levelName].Last();

    /// <summary>
    /// Verifica si contiene una solucion en un nivel. (Modo 1 de juego)
    /// </summary>
    /// <param name="levelName">Nombre del nivel</param>
    /// <param name="solution">Solucion a verificar</param>
    /// <returns>True si la solucion ya se hizo en el nivel, de lo contrario False</returns>
    public bool HasMod1Solution(string levelName, string solution) => Mod1Solutions[levelName].Contains(solution);

    /// <summary>
    /// Regresa el numero total de soluciones de un nivel. (Modo 1 de juego)
    /// </summary>
    /// <param name="levelName">Nombre del nivel</param>
    public int GetMod1CountSolutions(string levelName) => Mod1Solutions[levelName].Count;


    /// <summary>
    /// Regresa el puntaje total de un nivel. (Modo 2 de juego)
    /// </summary>
    /// <param name="levelName">Nombre del nivel</param>
    /// <param name="word">Cadena del nivel</param>
    /// <returns>Puntaje total</returns>
    public int GetMod2Score(string levelName, string word) => Constants.instance.GAME_MOD2_SCORE_MULTIPLER * 
        word.Length * Mod2Solutions[levelName][word].Count();

    /// <summary>
    /// Regresa todas las soluciones hechas por el usuario de un nivel. (Modo 2 de juego)
    /// </summary>
    /// <param name="levelName">Nombre del nivel</param>
    /// <param name="word">Cadena del nivel</param>
    /// <returns>Todas las soluciones hechas por el usuario</returns>
    public string GetMod2Solutions(string levelName, string word)
    {
        string solutions = "";
        foreach (string transitions in Mod2Solutions[levelName][word])
            solutions += transitions + "\n-----------\n";
        return solutions;
    }

    /// <summary>
    /// Regresa la ultima solucion hecha por el usuario de un nivel. (Modo 2 de juego)
    /// </summary>
    /// <param name="levelName">Nombre del nivel</param>
    /// <param name="word">Cadena del nivel</param>
    /// <returns>Ultima solucion hecha por el usuario</returns>
    //public string GetLastSolutionMod2(string levelName, string word) => Mod2Solutions[levelName][word].Last();

    /// <summary>
    /// Verifica si contiene una solucion en un nivel. (Modo 2 de juego)
    /// </summary>
    /// <param name="levelName">Nombre del nivel</param>
    /// <param name="word">Cadena del nivel</param>
    /// <param name="solution">Solucion a verificar</param>
    /// <returns>True si la solucion ya se hizo en el nivel, de lo contrario False</returns>
    public bool HasMod2Solution(string levelName, string word, string solution) =>
        Mod2Solutions[levelName][word].Contains(solution);

    /// <summary>
    /// Regresas todas las transiciones que tiene el auxiliar
    /// </summary>
    public string GetSolutionMod2()
    {
        string automata = "";
        foreach (KeyValuePair<string, string> transition in SOLUTION_MOD2.OrderBy(t => t.Key))
            automata += "(" + transition.Key + ")=" + transition.Value + " ";
        return automata;
    }

    /// <summary>
    /// Regresa el numero total de soluciones de un nivel. (Modo 2 de juego)
    /// </summary>
    /// <param name="levelName">Nombre del nivel</param>
    /// <param name="word">Cadena del nivel</param>
    public int GetMod2CountSolutions(string levelName, string word) => Mod2Solutions[levelName][word].Count;
}
