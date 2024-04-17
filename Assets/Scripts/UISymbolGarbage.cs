using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script que borra simbolos en la interfaz de usuario.
/// </summary>
public class UISymbolGarbage : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        UISymbolDrag.itemDragging.GetComponent<UISymbolDrag>().FinishOrdering();
        Destroy(UISymbolDrag.itemDragging);
    }
}
