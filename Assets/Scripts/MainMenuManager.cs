using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// Script que controla el menu principal.
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    // Obejtos a animar y sus valores de animacion.
    public Transform buttonPlay;
    public Transform titleText;
    public float btnPlayTime = 1f;
    public float titleTime = 2f;
    public float titleDelay = 2f;
    public Transform mainCamera;
    public float cameraTime = 1f;
    public float levelScale = 1.15f;
    public float levelTime = 0.2f;
    public Transform uiMaxSymbols;
    public float uiMaxSymbolsScale = 1.15f;
    public float uiMaxSymbolsTime = 0.2f;

    // Grid con los niveles.
    public Transform uiLevels;
    // Esta en la pantalla principal
    private bool isMainScreen = true;


    void Start()
    {
        // Animacion del boton para mostrar los niveles.
        buttonPlay.DOPunchScale(new Vector3(0.3f, 0.3f, 0f), btnPlayTime, 0, 1f)
            .SetLoops(-1);
        // Animacion del titulo.
        StartCoroutine(nameof(AnimationTitle));
    }

    /// <summary>
    /// Realiza animacion del titulo.
    /// </summary>
    public IEnumerator AnimationTitle()
    {
        while (isMainScreen)
        {            
            titleText.DOMove(new Vector3(getTitlePositionX(), 0.1f, 1.25f), titleTime)
                    .OnComplete(() => titleText.DOMove(new Vector3(getTitlePositionX(), 0.5f, 1.25f), titleTime));
            yield return new WaitForSeconds(titleDelay + (titleTime * 2));
            
            titleText.DOMove(new Vector3(getTitlePositionX(), 2.01f, 1.25f), titleTime);
            yield return new WaitForSeconds((titleDelay / 4) + titleTime);
        }
    }

    /// <summary>
    /// Muestra pantalla de niveles.
    /// </summary>
    public void OnPlay()
    {
        // Se cocultan elementos.
        isMainScreen = false;
        buttonPlay.gameObject.SetActive(false);
        titleText.DOMove(new Vector3(getTitlePositionX(), 2.1f, 1.25f), titleTime)
            .OnComplete(() => titleText.gameObject.SetActive(false));

        // Se muestran los niveles.
        mainCamera.DORotate(new Vector3(10f, 60f, 0f), cameraTime)
            .OnComplete(() =>
            {
                uiLevels.gameObject.SetActive(true);
                foreach (Transform level in uiLevels.GetChild(0))
                {
                    level.DOScale(levelScale, levelTime)
                        .OnComplete(() => level.DOScale(1f, levelTime));
                }
            }); 
        
    }

    /// <summary>
    /// Regresa la posion donde deberia estar el titulo en X.
    /// </summary>
    private float getTitlePositionX() => isMainScreen ? 1.32f : -1f;

    /// <summary>
    /// Muestra el panel para seleccionar el numero de maximo de simbolos.
    /// </summary>
    /// <param name="numBtnClick">Nivel que el usuario dio click</param>
    public void OnActiveUIMaxSymbols(int numBtnClick)
    {

        uiMaxSymbols.gameObject.SetActive(true);
        uiMaxSymbols.DOScale(0f, 0.01f)
            .OnComplete(() => uiMaxSymbols.DOScale(uiMaxSymbolsScale, uiMaxSymbolsTime)
                .OnComplete(() => uiMaxSymbols.DOScale(1f, uiMaxSymbolsTime))
            );
        // Se ocultan los otros niveles.
        Transform grid = uiLevels.GetChild(0);
        grid.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.UpperCenter;
        for (int i = 0; i < grid.childCount; i++)
            grid.GetChild(i).gameObject.SetActive(numBtnClick == i);
    }

    /// <summary>
    /// Se oculta el panel para seleccionar el numero de maximo de simbolos.
    /// </summary>
    public void OnDeactivateUIMaxSymbols()
    {
        uiMaxSymbols.DOScale(0f, uiMaxSymbolsTime)
            .OnComplete(() => uiMaxSymbols.gameObject.SetActive(false));
        // Se ponen de nuevo los niveles.
        Transform grid = uiLevels.GetChild(0);
        grid.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.UpperRight;
        for (int i = 0; i < grid.childCount; i++)
            grid.GetChild(i).gameObject.SetActive(true);
    }
}
