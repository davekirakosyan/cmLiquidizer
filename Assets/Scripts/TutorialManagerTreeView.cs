using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManagerTreeView : MonoBehaviour
{
    
    public GameObject targetObject;
    bool tutorialStarted = false;
    bool clicked = false;
    private Collider coll;
    public GameObject guidTextForRight;
    public GameObject guidTextForLeft;
    public GameObject Arrow;
    private bool tutorialState = false;

    public bool GetTutorialState()
    {
        return tutorialState;
    }

    void Start()
    {

        //DontDestroyOnLoad(this);
        //uncomment row below to uncomplete tutorial
        //PlayerPrefs.SetInt("Tutorial completed", 0);
        coll = targetObject.GetComponent<Collider>();
        tutorialState = true;
    }

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("Tutorial completed"))
            PlayerPrefs.SetInt("Tutorial completed", 0);
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


        if (PlayerPrefs.GetInt("Cinematic watched") == 1 && !tutorialStarted && PlayerPrefs.GetInt("Tutorial completed") == 0)
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

    private void ShowGuidTextForLeftSide(string text, int fontSize)
    {
        guidTextForLeft.SetActive(true);
        guidTextForLeft.transform.GetChild(1).GetComponent<Text>().text = text;
        guidTextForLeft.transform.GetChild(1).GetComponent<Text>().fontSize = fontSize;
    }

    public void ShowGuidText(string text, int fontSize, bool side = true)
    {
        if (side)   // Tutorial object is on right half of the screen
            ShowGuidTextForRightSide(text, fontSize);
        else        // Tutorial object is on left half of the screen
            ShowGuidTextForLeftSide(text, fontSize);
    }

    public void ShowArrow(Vector2 cords, bool side = false)
    {
        Arrow.SetActive(true);
        Arrow.transform.localPosition = new Vector3(cords.x, cords.y);
        
    }

    public void HideTurorial()
    {
        Arrow.SetActive(false);
        guidTextForLeft.SetActive(false);
        guidTextForRight.SetActive(false);
        tutorialState = false;
    }


    IEnumerator tutorialWait()
    {
        ShowGuidText("Tap on tree to enter.", 70);
        ShowArrow(new Vector2(107.9f, 125.37f));
        yield return new WaitUntil(() => clicked);
        HideTurorial();
        
    }
}
