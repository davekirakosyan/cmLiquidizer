﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Image arrowImage;
    public Image bubbleImage;
    public Image highlightImage;
    public Transform tutorialCanvas;
    // Start is called before the first frame update
    void Start()
    {
        ShowArrow(0, 0, 100, 100, 50);
        ShowBottomScreenDialog("blah blah blah");
        Highlight(-250, 150, 150, 150, 0);
    }

    void ShowArrow(float x, float y, float width, float height, float rot)
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
}
