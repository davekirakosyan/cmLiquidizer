using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
   
    public int order;

    [TextArea(3,10)]
    public string explanation;

    public Image indicatorImage;
    public GameObject highlightObject;
    public Image bubbleImage;
    public Image characterImage;
    public Image characterFacingBackImage;
    public bool left;
    public bool characterFacingBack;
    public Transform tutorialCanvas;
    public Vector2 position;
    public Vector2 rotation;
    public Vector2 bubbleSize;
    public Text dialogue;

    // Start is called before the first frame update
    void Awake()
    {
        //ShowArrow(0, 0, 100, 100, 50);
        //ShowBottomScreenDialog("blah blah blah");
        //Highlight(-250, 150, 150, 150, 0);

        TutorialManager.Instace.Tutorials.Add(this);
    }


    public virtual void CheckIfHappening() { }
    
  /*  void ShowArrow(float x, float y, float width, float height, float rot)
    {
        Image arrow = Instantiate(arrowImage) as Image;
        arrow.rectTransform.position = new Vector3(x, y, 0);
        arrow.rectTransform.eulerAngles = new Vector3(0, 0,rot);
        arrow.rectTransform.sizeDelta = new Vector2(width, height);
        arrow.transform.SetParent(tutorialCanvas, false);
    }
    
    void ShowBottomScreenDialog(string dialougeText)
    {
        Image bubble = Instantiate(bubbleImage) as Image;
        bubble.transform.SetParent(tutorialCanvas, false);
        GameObject dialogue = new GameObject("DialogueText");
        dialogue.transform.SetParent(bubble.transform, false);
        dialogue.transform.localPosition = new Vector3(0, 15, 0);
        dialogue.AddComponent<Text>().text = dialougeText;
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        dialogue.GetComponent<Text>().font = ArialFont;
        dialogue.GetComponent<Text>().color = Color.black;
        dialogue.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
    }

    void Highlight(float x, float y, float width, float height, float rot)
    {
        Image highlighter = Instantiate(highlightImage) as Image;
        highlighter.rectTransform.position = new Vector3(x, y, 0);
        highlighter.rectTransform.eulerAngles = new Vector3(0, 0, rot);
        highlighter.rectTransform.sizeDelta = new Vector2(width, height);
        highlighter.transform.SetParent(tutorialCanvas, false);
    }
    */
}
