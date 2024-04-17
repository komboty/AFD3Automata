using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Script que controla el desplazamiento de los simbolos en la interfaz de usuario.
/// </summary>
public class UISymbolDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    // Item moviendo.
    public static GameObject itemDragging;

    // Auxiliares para el ordenamiento dinamico de los simbolos.
    private Transform parentToReturnTo = null;
    private Transform placeHolderParent = null;
    private GameObject placeHolder = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Se guarda la referencia del item que se esta moviendo.
        itemDragging = gameObject;

        // Codigo para iniciar el ordenamiento dinamico de los simbolos.
        placeHolder = new GameObject();
        placeHolder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeHolder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;
        placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        parentToReturnTo = this.transform.parent;
        placeHolderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // El simbolo se mueve a la posicion del mouse.
        transform.position = Input.mousePosition;

        // Codigo que ordena dinamicamente los simbolos.
        if (placeHolder.transform.parent != placeHolderParent)
            placeHolder.transform.SetParent(placeHolderParent);
        int newSiblingIndex = placeHolderParent.childCount;
        for (int i = 0; i < placeHolderParent.childCount; i++)
            if (this.transform.position.x < placeHolderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;
                if (placeHolder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;
                break;
            }
        placeHolder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Se elimina la referencia del item que se estaba moviendo.
        itemDragging = null;

        // Codigo que restablece los simbolos despues de hacer el ordenamiento dinamico.
        FinishOrdering();
    }

    public void FinishOrdering()
    {
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(placeHolder);
    }
}
