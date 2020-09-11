using System.Collections;
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
    bool tutorialStarted = false;

    // Start is called before the first frame update
    void Start()
    {

        //DontDestroyOnLoad(this);
        //uncomment row below to uncomplete tutorial
        PlayerPrefs.SetInt("Tutorial completed", 0);
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Tutorial completed"))
            PlayerPrefs.SetInt("Tutorial completed", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Cinematic watched") == 1 && !tutorialStarted && PlayerPrefs.GetInt("Tutorial completed")==0)
        {
            SetNextTutorial(0);
            tutorialStarted = true;
        }
        if (currentTutorial)
            currentTutorial.CheckIfHappening();
    }

    public void completedTutorial()
    {
        Destroy(bubble);
        Destroy(indicator);
        Destroy(character);
        
        if (currentTutorial.highlight != null && currentTutorial.curtain != null)
        {
            currentTutorial.highlight.SetActive(false);
            currentTutorial.curtain.SetActive(false);
            
        }
        if (currentTutorial.highlightObject)
        {
            Destroy(currentTutorial.highlightObject.transform.GetChild(0).gameObject.GetComponent<Outline>());
            if (currentTutorial.highlightObject.name == "Viewport")
                currentTutorial.highlightObject.GetComponent<Mask>().enabled = true;
        }
        SetNextTutorial(currentTutorial.order + 1);
        Debug.Log("Tutorial completed");
    }


    void CreateBubbleAndCharacter()
    {
        float charactersPositionY=0;
        bubble = Instantiate(currentTutorial.bubbleImage, currentTutorial.tutorialCanvas) as Image;
        bubble.rectTransform.sizeDelta = currentTutorial.bubbleSize;
        currentTutorial.dialogue.transform.SetParent(bubble.transform, false);
        currentTutorial.dialogue.rectTransform.sizeDelta = new Vector2(290, 55);
        character = Instantiate(currentTutorial.characterImage) as Image;
        bubble.rectTransform.localPosition = new Vector3(0.3f, -170, 0);
        if (currentTutorial.characterFacingBack)
        {
            character = Instantiate(currentTutorial.characterFacingBackImage) as Image;
        }
        character.rectTransform.position = new Vector3(270, charactersPositionY, 0);
        character.rectTransform.eulerAngles = new Vector3(0, 180, 0);
        if (currentTutorial.left)
        {
            character.rectTransform.position = new Vector2(-240, charactersPositionY);
            character.rectTransform.eulerAngles = new Vector3(0, 0, 0);
        }
        character.transform.SetParent(currentTutorial.tutorialCanvas, false);
    }

    public void SetNextTutorial(int currentOrder)
    {
        int previousOrder=0;
        currentTutorial = GetTutorialByOrder(currentOrder);
        if (currentOrder > 0)
            previousOrder = currentOrder-1;
 
        if (GetTutorialByOrder(previousOrder).name == "step 16")
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
            currentTutorial.curtain.SetActive(true);
            currentTutorial.highlight.SetActive(true);
            currentTutorial.highlight.transform.localScale = currentTutorial.highlightSize;
            currentTutorial.highlight.transform.localPosition = currentTutorial.highlightPos;
            currentTutorial.highlight.transform.eulerAngles = currentTutorial.higlightRot;
            if (currentTutorial.highlightObject.name == "Viewport")
                currentTutorial.highlightObject.GetComponent<Mask>().enabled = false;
        }
        else if (currentTutorial.indicatorImage == null && currentTutorial.highlightObject == null && currentTutorial.characterImage != null)
        {
            CreateBubbleAndCharacter();
        }
        
    }

    public void CompletedAllTutorials()
    {
        expText.text = "";
        Debug.Log("completed all tutorials");
        PlayerPrefs.SetInt("Tutorial completed", 1);
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
