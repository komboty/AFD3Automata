using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script que controla todos los mensajes de texto que se muestra al usuario.
/// </summary>
public class UIMessagesManager : MonoBehaviour
{
    // Panel que bloquea las acciones de la pantalla.
    public Transform panelBack;
    // Panel donde muestran mensajes al usuario.
    public Transform panelMessages;
    // Panel del mensaje de ganador.
    public Transform panelWinner;
    public Transform panelWinnerScore;
    // Panel del mensaje de perdor.
    public Transform panelLoser;
    // Valores de animacion.
    public float doScaleSize = 1.3f;
    public float doScaleTime = 0.35f;
    public float doScoreSize = 1.1f;
    public float doScoreTime = 0.3f;

    // Auxiliares Score
    private int oldScoreTotalAux;
    private int levelScoreAux;
    private int animationSumAux;

    /// <summary>
    /// Muestra panel en pantalla con un mensajes de texto.
    /// </summary>
    /// <param name="text">Texto a mostrar</param>
    public void ShowMessage(string text)
    {
        panelBack.gameObject.SetActive(true);
        panelMessages.gameObject.SetActive(true);
        panelMessages.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        panelMessages.DOScale(0f, 0f);                
        panelMessages.DORotate(new Vector3(0f, 360f, 0f), doScaleTime, RotateMode.FastBeyond360);
        panelMessages.DOScale(1f, doScaleTime);
    }

    /// <summary>
    /// Evento que ejecuta el boton del panel para mensajes de texto.
    /// </summary>
    public void OnOKMessage()
    {
        panelBack.gameObject.SetActive(false);
        panelMessages.gameObject.SetActive(false);
        panelMessages.DOScale(0f, doScaleTime);
    }

    /// <summary>
    /// Muestra panel en pantalla con un mensajes de ganador.
    /// </summary>
    /// <param name="message">Mensaje de ganador</param>
    /// <param name="oldScoreTotal">Puntaje antes de ganar</param>
    /// <param name="levelScore">Puntaje de la partida actual</param>
    /// <param name="animationSum">Saltos del puntaje para la animacion</param>
    public void ShowWinner(string message,int oldScoreTotal, int levelScore, int animationSum)
    {
        panelBack.gameObject.SetActive(true);
        panelWinner.gameObject.SetActive(true);
        panelWinner.GetChild(0).GetComponent<TextMeshProUGUI>().text = message;

        // Se pone el puntaje        
        levelScoreAux = levelScore;
        oldScoreTotalAux = oldScoreTotal;
        animationSumAux = animationSum;
        panelWinnerScore.GetChild(1).GetComponent<TextMeshProUGUI>().text = oldScoreTotal.ToString();
        panelWinnerScore.GetChild(2).GetComponent<TextMeshProUGUI>().text = "+" + levelScore;

        // Se anima el panel
        panelWinner.DOScale(0f, 0f);
        panelWinner.DOScale(doScaleSize, doScaleTime)
                .OnComplete(() => panelWinner.DOScale(1f, doScaleTime));
        StartCoroutine(nameof(AnimationScore));
    }

    /// <summary>
    /// Anima el panel del puntaje
    /// </summary>
    /// <returns></returns>
    public IEnumerator AnimationScore()
    {
        yield return new WaitForSeconds(doScaleTime * 2);

        string oldScoreText;
        string levelScoreText;
        for (int i = 0; i < levelScoreAux / animationSumAux; i++)
        {
            oldScoreText = (oldScoreTotalAux + ((i + 1) * animationSumAux)).ToString();
            levelScoreText = "+" + (levelScoreAux - ((i + 1) * animationSumAux));
            panelWinnerScore.GetChild(1).GetComponent<TextMeshProUGUI>().text = oldScoreText;
            panelWinnerScore.GetChild(2).GetComponent<TextMeshProUGUI>().text = levelScoreText;
            panelWinnerScore.DOPunchScale(new Vector3(doScoreSize, doScoreSize, 0f), doScoreTime, 0);
            yield return new WaitForSeconds(doScoreTime);
        }

        panelWinnerScore.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
    }

    /// <summary>
    /// Muestra panel en pantalla con un mensajes de perdedor.
    /// </summary>
    public void ShowLoser()
    {
        panelBack.gameObject.SetActive(true);
        panelLoser.gameObject.SetActive(true);
        panelLoser.GetChild(0).GetComponent<TextMeshProUGUI>().text = Constants.instance.MESSAGE_LOSER;
        panelLoser.DOShakeScale(doScaleTime);
    }
}
