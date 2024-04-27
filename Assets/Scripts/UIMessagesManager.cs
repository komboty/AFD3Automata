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
    public void ShowWinner(string message,string oldScoreTotal, string scoreLevel)
    {
        panelBack.gameObject.SetActive(true);
        panelWinner.gameObject.SetActive(true);
        panelWinner.GetChild(0).GetComponent<TextMeshProUGUI>().text = message;

        // Se pone el puntaje        
        panelWinnerScore.GetChild(1).GetComponent<TextMeshProUGUI>().text = oldScoreTotal;
        panelWinnerScore.GetChild(2).GetComponent<TextMeshProUGUI>().text = "+" + scoreLevel;

        // Se anima el panel
        panelWinner.DOScale(0f, 0f);
        panelWinnerScore.DOScale(0f, 0f);
        panelWinner.DOScale(doScaleSize, doScaleTime)
                .OnComplete(() => panelWinner.DOScale(1f, doScaleTime)
                    .OnComplete(() => panelWinnerScore.DOScale(doScaleSize, doScaleTime)
                        .OnComplete(() => panelWinnerScore.DOScale(1f, doScaleTime))
                    )
                );
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
