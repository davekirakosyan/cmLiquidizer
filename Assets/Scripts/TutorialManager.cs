using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
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
        Image.Destroy(bubble);
        Image.Destroy(indicator);
        if (currentTutorial.highlightObject)
        {
            Destroy(currentTutorial.highlightObject.transform.GetChild(0).gameObject.GetComponent<Outline>());
        }
        SetNextTutorial(currentTutorial.order + 1);
        Debug.Log("Tutorial completed");
    }

    void CreateBubble()
    {
        Image bubble = Instantiate(currentTutorial.bubbleImage) as Image;
        bubble.transform.SetParent(currentTutorial.tutorialCanvas, false);
        currentTutorial.dialogue.transform.SetParent(bubble.transform, false);
        currentTutorial.dialogue.transform.localPosition = new Vector3(0, 15, 0);
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

        if (currentTutorial.indicatorImage != null)
        {

            indicator = Instantiate(currentTutorial.indicatorImage) as Image;
            indicator.rectTransform.position = new Vector3(currentTutorial.position.x, currentTutorial.position.y, 0);
            indicator.rectTransform.eulerAngles = new Vector3(0, currentTutorial.rotation.x, currentTutorial.rotation.y);
            indicator.rectTransform.sizeDelta = new Vector2(currentTutorial.size.x, currentTutorial.size.y);
            indicator.transform.SetParent(currentTutorial.tutorialCanvas, false);

            if (currentTutorial.explanation != "")
            {
                CreateBubble();
            }
        } else
        {
            GameObject target = currentTutorial.highlightObject.transform.GetChild(0).gameObject;
            target.AddComponent<Outline>();
            target.GetComponent<Outline>().effectColor = new Color32(232, 255, 0, 150);
            target.GetComponent<Outline>().effectDistance = new Vector2(2.5f, -2.5f);
            CreateBubble();

        }
        
    }

    public void CompletedAllTutorials()
    {
        expText.text = "You have copleted all the tutorials";

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
