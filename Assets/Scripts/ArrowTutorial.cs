using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowTutorial : Tutorial
{

    public GameObject targetObject;
    public Image arrowImage;
    public Image bubbleImage;
    public Transform tutorialCanvas;
    public Vector2 position;
    public float rotation;
    public Vector2 size;
    private Collider coll;
    public Text dialogue;

    private void Start()
    {
        Image arrow = Instantiate(arrowImage) as Image;
        arrow.rectTransform.position = new Vector3(position.x, position.y, 0);
        arrow.rectTransform.eulerAngles = new Vector3(0, 0, rotation);
        arrow.rectTransform.sizeDelta = new Vector2(size.x, size.y);
        arrow.transform.SetParent(tutorialCanvas, false);

        coll = targetObject.GetComponent<Collider>();
        if (explanation != "")
        {
            Image bubble = Instantiate(bubbleImage) as Image;
            bubble.transform.SetParent(tutorialCanvas, false);
            dialogue.transform.SetParent(bubble.transform, false);
            dialogue.transform.localPosition = new Vector3(0, 15, 0);
        }
    }

    public override void CheckIfHappening()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (coll.Raycast(ray, out hit, 100.0f))
            {
                TutorialManager.Instace.completedTutorial();
            }
        }
    }
}
