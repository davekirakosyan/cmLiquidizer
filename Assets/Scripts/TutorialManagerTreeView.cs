using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TutorialManagerTreeView : MonoBehaviour
{
    public GameObject targetObject;
    bool tutorialStarted = false;
    bool clicked = false;
    private Collider coll;
    public GameObject guidTextForRight;
    public GameObject Arrow;
    public GameObject treeMask;
    private bool tutorialState = false;

    int cinematicWatched;
    int tutorialCompleted;

    public bool GetTutorialState()
    {
        return tutorialState;
    }

    void Start()
    {

        //DontDestroyOnLoad(this);
        //uncomment row below to uncomplete tutorial
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("Tutorial completed", 0);
        coll = targetObject.GetComponent<Collider>();
        tutorialState = true;
    }

    private void Awake()
    {
        JSON_API.ReadJSONFromMemory(); // Memory access is slow operation

        cinematicWatched = JSON_API.GetJSONData<int>("Cinematic watched");
        tutorialCompleted = JSON_API.GetJSONData<int>("Tutorial completed");

        /*//PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("Tutorial completed"))
            PlayerPrefs.SetInt("Tutorial completed", 0);*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if (coll.Raycast(ray, out hit, 10000f))
            {
                clicked = true;
            }
        }
        //JSON_API.ReadJSONFromMemory(); // Memory access is slow operation

        cinematicWatched = JSON_API.GetJSONData<int>("Cinematic watched");
        tutorialCompleted = JSON_API.GetJSONData<int>("Tutorial completed");

        if (cinematicWatched == 1 && !tutorialStarted && tutorialCompleted == 0)
        {
            StartCoroutine(tutorialWait());
            tutorialStarted = true;
        }
    }

    private void ShowGuidTextForRightSide(string text, int fontSize)
    {
        guidTextForRight.SetActive(true);
        guidTextForRight.transform.GetChild(1).GetComponent<Text>().text = text;
        guidTextForRight.transform.GetChild(1).GetComponent<Text>().fontSize = fontSize;
    }

    public void ShowGuidText(string text, int fontSize, bool side = true)
    {
        if (side)   // Tutorial object is on right half of the screen
            ShowGuidTextForRightSide(text, fontSize);
    }

    public void ShowArrow(Vector2 cords, bool side = false)
    {
        Arrow.SetActive(true);
        Arrow.transform.localPosition = new Vector3(cords.x, cords.y);
        
    }

    public void HideTurorial()
    {
        Arrow.SetActive(false);
        guidTextForRight.SetActive(false);
        treeMask.SetActive(false);
        tutorialState = false;
    }


    IEnumerator tutorialWait()
    {
        treeMask.SetActive(true);
        ShowGuidText("Tap on tree to enter.", 70);
        ShowArrow(new Vector2(107.9f, 125.37f));
        yield return new WaitUntil(() => clicked);
        HideTurorial();
    }
}
