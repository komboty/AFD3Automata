using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMessagesManager : MonoBehaviour
{
    // Constantes del juego.
    public Constants constants;
    // Panel que bloquea las acciones de la pantalla.
    public Transform panelBack;
    // Panel donde muestran mensajes al usuario.
    public Transform panelMessages;
    // Panel del mensaje de ganador.
    public Transform panelWinner;
    // Panel del mensaje de perdor.
    public Transform panelLoser;
    // Valores de animacion.
    public float doScaleSize = 1.3f;
    public float doScaleTime = 0.35f;    

    public void ShowMessage(string text)
    {
        panelBack.gameObject.SetActive(true);
        panelMessages.gameObject.SetActive(true);
        panelMessages.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        panelMessages.DOScale(0f, 0.01f)
                .OnComplete(() => {
                    panelMessages.DORotate(new Vector3(0f, 360f, 0f), doScaleTime, RotateMode.FastBeyond360);
                    panelMessages.DOScale(1f, doScaleTime);
                });
    }

    public void OnOKMessage()
    {
        panelBack.gameObject.SetActive(false);
        panelMessages.gameObject.SetActive(false);
    }

    public void ShowWinner()
    {
        panelBack.gameObject.SetActive(true);
        panelWinner.gameObject.SetActive(true);
        panelWinner.GetChild(0).GetComponent<TextMeshProUGUI>().text = constants.MESSAGES_WINNER;
        panelWinner.DOScale(0f, 0.01f)
                .OnComplete(() => panelWinner.DOScale(doScaleSize, doScaleTime)
                    .OnComplete(() => panelWinner.DOScale(1f, doScaleTime))
                );
    }

    public void ShowLoser()
    {
        panelBack.gameObject.SetActive(true);
        panelLoser.gameObject.SetActive(true);
        panelLoser.GetChild(0).GetComponent<TextMeshProUGUI>().text = constants.MESSAGES_LOSER;
        panelLoser.DOShakeScale(doScaleTime);
    }
}
