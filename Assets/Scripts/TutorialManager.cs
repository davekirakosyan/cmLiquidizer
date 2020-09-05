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
    Image arrow;

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
        Image.Destroy(arrow);
        SetNextTutorial(currentTutorial.order + 1);
        Debug.Log("Tutorial completed");
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

        arrow = Instantiate(currentTutorial.arrowImage) as Image;
        arrow.rectTransform.position = new Vector3(currentTutorial.position.x,currentTutorial.position.y, 0);
        arrow.rectTransform.eulerAngles = new Vector3(0, currentTutorial.rotation.x, currentTutorial.rotation.y);
        arrow.rectTransform.sizeDelta = new Vector2(currentTutorial.size.x, currentTutorial.size.y);
        arrow.transform.SetParent(currentTutorial.tutorialCanvas, false);

        if (currentTutorial.explanation != "")
        {
            Image bubble = Instantiate(currentTutorial.bubbleImage) as Image;
            bubble.transform.SetParent(currentTutorial.tutorialCanvas, false);
            currentTutorial.dialogue.transform.SetParent(bubble.transform, false);
            currentTutorial.dialogue.transform.localPosition = new Vector3(0, 15, 0);
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
