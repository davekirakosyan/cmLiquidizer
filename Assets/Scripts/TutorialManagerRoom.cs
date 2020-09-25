using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManagerRoom : MonoBehaviour
{

    public GameObject[] targetObject;
    bool tutorialStarted = false;
    public bool clicked = false;
    public GameObject guidTextForRight;
    public GameObject Arrow;
    public Collider tabelColl;
    bool press=false;
    public bool tableClick = false;
    public GameObject backButton;

    void Start()
    {
        //uncomment rows below to uncomplete tutorial
        //PlayerPrefs.SetInt("Tutorial completed", 0);
        //PlayerPrefs.SetInt("Cinematic watched", 1);
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

    public void ShowArrow(Vector2 cords,Vector3 rot, bool side = false)
    {
        Arrow.SetActive(true);
        Arrow.transform.localPosition = cords;
        Arrow.transform.eulerAngles = rot;
    }

    public void HideTurorial()
    {
        Arrow.SetActive(false);
        guidTextForRight.SetActive(false);
        clicked = false;
    }


    IEnumerator tutorialWait()
    {
        backButton.SetActive(false);
        tabelColl.enabled = false;
        ShowGuidTextForRightSide("Wow! Looks like no one has been here for ages!", 50);
        yield return new WaitUntil(() => clicked);
        HideTurorial();
        ShowArrow(new Vector2(-28.04f, 31.6f),new Vector3(0,0,0));
        ShowGuidTextForRightSide("Tap on the table to see what we have.", 35);
        tabelColl.enabled = true;
        yield return new WaitUntil(() => tableClick);
        HideTurorial();
        ShowArrow(new Vector2(-259.58f,-5f), new Vector3(0, 0, 151.1f));
        ShowGuidTextForRightSide("The Path is where we will do all the magic. Touch it to start.", 35);
    }
}
