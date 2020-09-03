using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public InventoryManager.ElixirColor colorName;
    private Transform parent;
    private Vector3 itemPosition;
    private Vector3 itemSize;
    private bool holding = false;
    private bool selected = false;
    private float startHeld;
    private float targetHeld = 0.5f;
    private float currentSBSize;
    private Color imageColor;
    public Vibrator vibrator;

    IEnumerator waitForHold()
    {
        yield return new WaitUntil(() => (holding && Time.time >= startHeld + targetHeld));
        selectItem();
    }

    private void UpdateData()
    {
        itemPosition = transform.localPosition;
        itemSize = transform.localScale;
        parent = transform.parent;
        currentSBSize = 1.0f;

        imageColor = transform.gameObject.GetComponent<Image>().color;
    }
    
    private void selectItem()
    {
        vibrator.Vibrate();
        selected = true;
        transform.localScale = itemSize / 2;
        transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateData();
        holding = true;
        startHeld = Time.time;
        transform.gameObject.GetComponent<Image>().color = new Color(imageColor.r, imageColor.g, imageColor.b, imageColor.a / 2);
        StartCoroutine(waitForHold());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (selected)
        {
            transform.SetParent(parent);
            transform.localPosition = itemPosition;
            transform.localScale = itemSize;
            PathController.dragged = true;
            selected = false;
        }

        transform.gameObject.GetComponent<Image>().color = imageColor;
        holding = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (selected)
        {
            transform.SetParent(GetComponentInParent<Canvas>().transform);
            transform.position = Input.mousePosition;
        }
        else
        {
            holding = false;
            float currentPos = GameObject.Find("Inventory").GetComponent<ScrollRect>().verticalNormalizedPosition;

            if (Input.GetAxis("Mouse Y") < 0)
            {
                GameObject.Find("Inventory").GetComponent<ScrollRect>().verticalNormalizedPosition = Mathf.Lerp(currentPos, 1, 0.2f);
                if (GameObject.Find("Inventory").GetComponent<ScrollRect>().verticalNormalizedPosition + 0.2 >= 1)
                {
                    currentPos = GameObject.Find("Inventory").GetComponent<ScrollRect>().verticalScrollbar.size;
                    GameObject.Find("Inventory").GetComponent<ScrollRect>().verticalScrollbar.size = Mathf.Lerp(currentPos, 0, 0.2f);
                }
            }
            else if (Input.GetAxis("Mouse Y") > 0)
            {
                GameObject.Find("Inventory").GetComponent<ScrollRect>().verticalNormalizedPosition = Mathf.Lerp(currentPos, 0, 0.2f);
                if (GameObject.Find("Inventory").GetComponent<ScrollRect>().verticalNormalizedPosition - 0.2 <= 0)
                {
                    currentPos = GameObject.Find("Inventory").GetComponent<ScrollRect>().verticalScrollbar.size;
                    GameObject.Find("Inventory").GetComponent<ScrollRect>().verticalScrollbar.size = Mathf.Lerp(currentPos, 0, 0.2f);
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (selected)
        {
            GameManager.SelectElixir(this.gameObject);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject.Find("Inventory").GetComponent<ScrollRect>().verticalScrollbar.size = currentSBSize;
    }
}
