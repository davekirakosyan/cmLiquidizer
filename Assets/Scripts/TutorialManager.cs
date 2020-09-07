﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    public List<Tutorial> Tutorials = new List<Tutorial>();
    public Text expText;
    private static TutorialManager instance;
    public static TutorialManager Instace
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<TutorialManager>();
                Debug.Log("There is no TutorialManager");
            }

            return instance;
        }
    }

    private Tutorial currentTutorial;
    Image bubble;
    Image indicator;
    Image character;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this);
        SetNextTutorial(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTutorial)
            currentTutorial.CheckIfHappening();
    }

    public void completedTutorial()
    {
        Destroy(bubble);
        Destroy(indicator);
        Destroy(character);
        if (currentTutorial.highlightObject)
        {
            Destroy(currentTutorial.highlightObject.transform.GetChild(0).gameObject.GetComponent<Outline>());
        }
        SetNextTutorial(currentTutorial.order + 1);
        Debug.Log("Tutorial completed");
    }


    void CreateBubbleAndCharacter()
    {
        float charactersPositionY=-76;
        bubble = Instantiate(currentTutorial.bubbleImage, currentTutorial.tutorialCanvas) as Image;
        bubble.rectTransform.sizeDelta = currentTutorial.bubbleSize;
        currentTutorial.dialogue.transform.SetParent(bubble.transform, false);
        currentTutorial.dialogue.transform.localPosition = new Vector3(0, 0, 0);
        character = Instantiate(currentTutorial.characterImage) as Image;
        if (currentTutorial.characterFacingBack)
        {
            character = Instantiate(currentTutorial.characterFacingBackImage) as Image;
            charactersPositionY = -170;
        }
        character.rectTransform.position = new Vector3(270, charactersPositionY, 0);
        character.rectTransform.eulerAngles = new Vector3(0, 180, 0);
        if (currentTutorial.left)
        {
            character.rectTransform.position = new Vector2(-220, charactersPositionY);
            character.rectTransform.eulerAngles = new Vector3(0, 0, 0);
        }
        character.transform.SetParent(currentTutorial.tutorialCanvas, false);
    }

    public void SetNextTutorial(int currentOrder)
    {
        
        currentTutorial = GetTutorialByOrder(currentOrder);

        if (!currentTutorial)
        {
            CompletedAllTutorials();
            return;
        }

        expText.text = currentTutorial.explanation;

        if (currentTutorial.indicatorImage != null && currentTutorial.highlightObject == null && currentTutorial.characterImage != null)
        {

            indicator = Instantiate(currentTutorial.indicatorImage) as Image;
            indicator.rectTransform.position = new Vector3(currentTutorial.position.x, currentTutorial.position.y, 0);
            indicator.rectTransform.eulerAngles = new Vector3(0, currentTutorial.rotation.x, currentTutorial.rotation.y);
            indicator.transform.SetParent(currentTutorial.tutorialCanvas, false);

            if (currentTutorial.explanation != "")
            {
                CreateBubbleAndCharacter();
            }
        }
        else if (currentTutorial.indicatorImage == null && currentTutorial.highlightObject != null && currentTutorial.characterImage != null)
        {
            CreateBubbleAndCharacter();
            GameObject target = currentTutorial.highlightObject.transform.GetChild(0).gameObject;
            Outline outline = target.AddComponent<Outline>();
            outline.effectColor = new Color32(232, 255, 0, 150);
            outline.effectDistance = new Vector2(2.5f, -2.5f);

        }
        else if (currentTutorial.indicatorImage == null && currentTutorial.highlightObject == null && currentTutorial.characterImage != null)
        {
            CreateBubbleAndCharacter();
        }
        
    }

    public void CompletedAllTutorials()
    {
        expText.text = "";

        //load next scene
    }

    public Tutorial GetTutorialByOrder(int order)
    {
        for(int i = 0; i < Tutorials.Count; i++)
        {
            if (Tutorials[i].order == order)
                return Tutorials[i];
        }

        return null;
    }

}
