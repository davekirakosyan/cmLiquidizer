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

        GameManager.SelectElixir(this.gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.SetParent(GetComponentInParent<Canvas>().transform);
        transform.localPosition = new Vector3(Input.mousePosition.x-Screen.width/2, Input.mousePosition.y-Screen.height/2, 0);
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
