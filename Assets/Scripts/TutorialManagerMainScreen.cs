using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

public class TutorialManagerMainScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject FullMask;
    public GameObject PathMask;
    public GameObject Pour1Mask;
    public GameObject InventoryMask;
    public GameObject ReceipeMask;
    public GameObject guidTextForRight;
    public GameObject guidTextForLeft;
    public bool clicked = false;
    bool selection = false;
    bool pouring = false;
    bool tutorialStarted = false;

    public Transform cardHolder;
    public GameObject UIButtons;
    public GameObject inventoryBLocker;

    void Start()
    {
        //uncomment row below to uncomplete tutorial
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("Tutorial completed", 0);
        //PlayerPrefs.SetInt("Cinematic watched", 1);
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Tutorial completed"))
            PlayerPrefs.SetInt("Tutorial completed", 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !selection && !pouring)
            clicked = true;

        if (PlayerPrefs.GetInt("Cinematic watched") == 1 && !tutorialStarted && PlayerPrefs.GetInt("Tutorial completed") == 0)
        {
            StartCoroutine(tutorialWait());
            tutorialStarted = true;
        }
    }

    public void ShowFullMask()
    {
        ReceipeMask.SetActive(false);
        FullMask.SetActive(true);
    }

    public void ShowPathMask()
    {
        PathMask.SetActive(true);
    }

    public void ShowPour1Mask(bool needPouring = true)
    {
        //Pour1Mask.SetActive(true);
        pouring = needPouring;
    }

    public void ShowPour2Mask(bool needPouring = true)
    {
        Pour1Mask.SetActive(true);
        pouring = needPouring;
    }

    public void ShowInventoryMask()
    {
        InventoryMask.SetActive(true);
        selectedCardMask.GetComponent<RectTransform>().offsetMin = new Vector2(selectedCardMask.GetComponent<RectTransform>().offsetMin.x, 0);
    }

    public void ShowCardSelectionMask()
    {
        ReceipeMask.SetActive(true);
        //MaskForCard.SetActive(true);
        for (int i = 1; i < cardHolder.childCount; i++)
        {
            cardHolder.GetChild(i).GetComponent<CardAnimation>().mask.SetActive(true);
            cardHolder.GetChild(i).GetComponent<CardAnimation>().mask.transform.SetAsLastSibling();
        }
        selection = true;
    }

    public void ShowReceipeMask()
    {
        ReceipeMask.SetActive(true);
    }

    GameObject selectedCardMask;
    public void ShowReceipeMaskUpper()
    {
        ReceipeMask.SetActive(true);
        selectedCardMask = cardHolder.GetChild(0).GetComponent<CardAnimation>().mask;
        selectedCardMask.gameObject.SetActive(true);
        selectedCardMask.transform.SetAsLastSibling();
        selectedCardMask.GetComponent<RectTransform>().offsetMax = new Vector2(selectedCardMask.GetComponent<RectTransform>().offsetMax.x, -150);
        //ReceipeMaskUpper.SetActive(true);
    }

    public void ShowReceipeMaskBottom()
    {
        ReceipeMask.SetActive(true);
        selectedCardMask.gameObject.SetActive(true);
        selectedCardMask.transform.SetAsLastSibling();
        selectedCardMask.GetComponent<RectTransform>().offsetMax = new Vector2(selectedCardMask.GetComponent<RectTransform>().offsetMax.x, 0);
        selectedCardMask.GetComponent<RectTransform>().offsetMin = new Vector2(selectedCardMask.GetComponent<RectTransform>().offsetMin.x, 165);
    }

    private void ShowGuidTextForRightSide(string text, int fontSize, bool touchText)
    {
        guidTextForRight.SetActive(true);
        guidTextForRight.transform.GetChild(1).GetComponent<Text>().text = text;
        guidTextForRight.transform.GetChild(1).GetComponent<Text>().fontSize = fontSize;
        guidTextForRight.transform.GetChild(1).GetChild(0).gameObject.SetActive(touchText);
    }

    private void ShowGuidTextForLeftSide(string text, int fontSize, bool touchText)
    {
        guidTextForLeft.SetActive(true);
        guidTextForLeft.transform.GetChild(1).GetComponent<Text>().text = text;
        guidTextForLeft.transform.GetChild(1).GetComponent<Text>().fontSize = fontSize;
        guidTextForLeft.transform.GetChild(1).GetChild(0).gameObject.SetActive(touchText);
    }

    public void ShowGuidText(string text, int fontSize, bool side = true, bool touchText = false)
    {
        if (side)   // Tutorial object is on right half of the screen
            ShowGuidTextForRightSide(text, fontSize, touchText);
        else        // Tutorial object is on left half of the screen
            ShowGuidTextForLeftSide(text, fontSize, touchText);
    }

    public void HideTurorial()
    {
        guidTextForLeft.SetActive(false);
        guidTextForRight.SetActive(false);
        FullMask.SetActive(false);
        PathMask.SetActive(false);
        InventoryMask.SetActive(false);
        ReceipeMask.SetActive(false);
        Pour1Mask.SetActive(false);
        clicked = false;
        selection = false;
        pouring = false;
    }

    IEnumerator tutorialWait()
    {
        UIButtons.SetActive(false);
        inventoryBLocker.SetActive(true);

        ShowFullMask();
        ShowGuidText("Welcome to the elixir creation!", 50, true, true);
        yield return new WaitUntil(() => clicked);
        HideTurorial();

        ShowCardSelectionMask();
        ShowGuidText("Let's start from the first level, tap on the card to continue.", 43, true);
        yield return new WaitUntil(() => clicked);
        HideTurorial();


        ShowReceipeMask();
        ShowGuidText("This is your recipe card.", 40, false, true);
        yield return new WaitUntil(() => clicked);
        HideTurorial();


        ShowReceipeMaskUpper();
        ShowGuidText("The upper part shows you what elixirs you have.", 33, false, true);
        yield return new WaitUntil(() => clicked);
        HideTurorial();


        ShowReceipeMaskBottom();
        ShowGuidText("And here you can see what elixirs you need to produce.", 33, false, true);
        yield return new WaitUntil(() => clicked);
        HideTurorial();


        ShowInventoryMask();
        ShowGuidText("Your elixirs are located in the inventory.", 50, true, true);
        yield return new WaitUntil(() => clicked);
        HideTurorial();

        ShowPathMask();
        ShowGuidText("This is the glass tube in which you should pour the given elixirs.", 45, true, true);
        yield return new WaitUntil(() => clicked);
        HideTurorial();

        ShowPour1Mask();
        selectedCardMask.SetActive(false);
        inventoryBLocker.SetActive(false);
        ShowGuidText("Pour one of the elixirs into the path. You can either drag and drop or select the elixir and touch the path.", 45);
        yield return new WaitUntil(() => clicked);
        inventoryBLocker.SetActive(true);
        HideTurorial();

        ShowPour1Mask(false);
        ShowGuidText("Once you pour the elixir in the path, it will start flowing with a constant speed and direction.", 45, true, true);
        yield return new WaitUntil(() => clicked);
        HideTurorial();
        ShowPour1Mask();
        inventoryBLocker.SetActive(false);
        ShowGuidText("Now pour the second elixir. Make sure that it doesn't touch to first one, or they will mix.", 45);
        yield return new WaitUntil(() => clicked);
        HideTurorial();

        UIButtons.SetActive(true);
        for (int i = 0; i < cardHolder.childCount; i++)
            cardHolder.GetChild(i).GetComponent<CardAnimation>().mask.SetActive(false);


        PlayerPrefs.SetInt("Tutorial completed", 1);
    }

    public void loadTurorial()
    {
        StartCoroutine(tutorialWait());
    }

}
