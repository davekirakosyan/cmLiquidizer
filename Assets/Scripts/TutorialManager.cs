using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject FullMask;
    public GameObject PathMask;
    public GameObject Pour1Mask;
    public GameObject Pour2Mask;
    public GameObject InventoryMask;
    public GameObject MaskForCard;
    public GameObject ReceipeMask;
    public GameObject ReceipeMaskUpper;
    public GameObject ReceipeMaskBottom;
    public GameObject guidTextForRight;
    public GameObject guidTextForLeft;
    public GameObject Arrow;
    public bool clicked = false;
    public bool selection = false;
    public bool pouring = false;

    public void ShowFullMask()
    {
        FullMask.SetActive(true);
    }

    public void ShowPathMask()
    {
        PathMask.SetActive(true);
    }

    public void ShowPour1Mask(bool needPouring = true)
    {
        Pour1Mask.SetActive(true);
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
    }

    public void ShowCardSelectionMask()
    {
        MaskForCard.SetActive(true);
        selection = true;
    }

    public void ShowReceipeMask()
    {
        ReceipeMask.SetActive(true);
    }

    public void ShowReceipeMaskUpper()
    {
        ReceipeMaskUpper.SetActive(true);
    }

    public void ShowReceipeMaskBottom()
    {
        ReceipeMaskBottom.SetActive(true);
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
        if (side)   // Arrow points to the right
            Arrow.transform.localPosition = new Vector3(0f, 180.0f, Arrow.transform.localPosition.z);
        else        // Arrow points to the left
            Arrow.transform.localPosition = new Vector3(0f, 0f, Arrow.transform.localPosition.z);
    }

    public void HideTurorial()
    {
        Arrow.SetActive(false);
        guidTextForLeft.SetActive(false);
        guidTextForRight.SetActive(false);
        FullMask.SetActive(false);
        PathMask.SetActive(false);
        InventoryMask.SetActive(false);
        MaskForCard.SetActive(false);
        ReceipeMask.SetActive(false);
        ReceipeMaskUpper.SetActive(false);
        ReceipeMaskBottom.SetActive(false);
        Pour1Mask.SetActive(false);
        Pour2Mask.SetActive(false);
        clicked = false;
        selection = false;
        pouring = false;
    }

    IEnumerator tutorialWait()
    {
        ShowFullMask();
        ShowGuidText("Welcome to main game scene, touch to continue.", 35);
        yield return new WaitUntil(() => clicked);
        HideTurorial();
        ShowCardSelectionMask();
        ShowArrow(new Vector2(0,0));
        ShowGuidText("Let's start from first level, touch to card to continue.", 50, false);
        yield return new WaitUntil(() => clicked);
        HideTurorial();
        ShowReceipeMask();
        ShowGuidText("This is your receipe card, touch to continue.", 35);
        yield return new WaitUntil(() => clicked);
        HideTurorial();
        ShowReceipeMaskUpper();
        ShowGuidText("The upper part shows you what elixirs you have, touch to continue.", 35);
        yield return new WaitUntil(() => clicked);
        HideTurorial();
        ShowReceipeMaskBottom();
        ShowGuidText("Here you can see what elixirs you neet to produce, touch to continue.", 35);
        yield return new WaitUntil(() => clicked);
        HideTurorial();
        ShowInventoryMask();
        ShowGuidText("Your elixirs are located in this inventory, touch to continue.", 50, false);
        yield return new WaitUntil(() => clicked);
        HideTurorial();
        ShowPathMask();
        ShowGuidText("This is the glass tube in which you should pour the given elixirs, touch to continue.", 50, false);
        yield return new WaitUntil(() => clicked);
        HideTurorial();
        ShowPour1Mask();
        ShowGuidText("Pour one of the elixirs into the path. You can either drag and drop or select the elixir and thouch the path.", 45, false);
        yield return new WaitUntil(() => clicked);
        HideTurorial();
        ShowPour2Mask(false);
        ShowGuidText("Once you pour the elixir in the path, it will start flowing with a constant speed and direction, touch to continue.", 45, false);
        yield return new WaitUntil(() => clicked);
        HideTurorial();
        ShowPour1Mask();
        ShowGuidText("Now pour the second elixir. Make sure that it doesn't touch to first one, or they will mix.", 50, false);
        yield return new WaitUntil(() => clicked);
        HideTurorial();

        
    }

    public void loadTurorial()
    {
        StartCoroutine(tutorialWait());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !selection && !pouring)
            clicked = true;
    }
}
