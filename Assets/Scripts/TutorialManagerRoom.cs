using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManagerRoom : MonoBehaviour
{

    public GameObject[] targetObject;
    bool tutorialStarted = false;
    public bool clicked = false;
    private Collider coll;
    public GameObject FullMask;
    public GameObject guidTextForRight;
    public GameObject guidTextForLeft;
    public GameObject guidTextForBack;
    public GameObject Arrow;
    bool press=false;

    bool targetexists = false;

    void Start()
    {

        //DontDestroyOnLoad(this);
        //uncomment row below to uncomplete tutorial
        PlayerPrefs.SetInt("Tutorial completed", 0);
        coll = targetObject[0].GetComponent<Collider>();
    }

    public void ShowFullMask()
    {
        FullMask.SetActive(true);
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

        if (Input.GetMouseButtonUp(0) && !press)
        {
            clicked = true;
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

    private void ShowGuidTextForBackSide(string text, int fontSize)
    {
        guidTextForBack.SetActive(true);
        guidTextForBack.transform.GetChild(1).GetComponent<Text>().text = text;
        guidTextForBack.transform.GetChild(1).GetComponent<Text>().fontSize = fontSize;
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

    public void ShowArrow(Vector2 cords,Vector3 rot, bool side = false)
    {
        Arrow.SetActive(true);
        Arrow.transform.localPosition = cords;
        Arrow.transform.eulerAngles = rot;
    }

    public void HideTurorial()
    {
        Arrow.SetActive(false);
        guidTextForLeft.SetActive(false);
        guidTextForRight.SetActive(false);
        guidTextForBack.SetActive(false);
        clicked = false;
        FullMask.SetActive(false);
        targetexists = false;
    }


    IEnumerator tutorialWait()
    {
        ShowFullMask();
        ShowGuidTextForBackSide("Wow looks like no one has been here for ages!", 35);
        yield return new WaitUntil(() => clicked);
        HideTurorial();
        ShowArrow(new Vector2(-28.04f, 31.6f),new Vector3(0,0,0));
        ShowGuidTextForRightSide("Tap on the table to see what we have.", 35);
        yield return new WaitUntil(() => clicked);
        HideTurorial();
        ShowFullMask();
        yield return new WaitForSeconds(1);
        HideTurorial();
        ShowArrow(new Vector2(-259.58f,-5f), new Vector3(0, 0, 151.1f));
        ShowGuidTextForRightSide("The Path is where we will do all the magic. Touch it to start.", 35);
        yield return new WaitUntil(() => clicked);
        HideTurorial();
    }
}
