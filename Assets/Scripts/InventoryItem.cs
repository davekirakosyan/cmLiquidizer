using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public InventoryManager.ElixirColor colorName;
    Transform parent;
    Vector3 itemPosition;
    Vector3 itemSize;

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemPosition = transform.localPosition;
        parent = transform.parent;
        itemSize = transform.localScale;

        BasicLogic.SelectElixir(this.gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.SetParent(GetComponentInParent<Canvas>().transform);
        transform.position = Input.mousePosition;
        transform.localScale = itemSize / 3;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parent);
        transform.localPosition = itemPosition;
        transform.localScale = itemSize;
        PathController.dragged = true;
    }
}
