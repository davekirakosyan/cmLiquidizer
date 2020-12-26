using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollableArea : ScrollRect
{
    private void Update()
    {
        float contentHeight = content.GetComponent<RectTransform>().sizeDelta.y;
        float canvasHeight = transform.parent.parent.GetComponent<RectTransform>().sizeDelta.y;
        if (contentHeight <= canvasHeight)
            verticalScrollbar.transform.GetChild(0).gameObject.SetActive(false);
        else
            verticalScrollbar.transform.GetChild(0).gameObject.SetActive(true);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        //Dummy:))
    }
}
