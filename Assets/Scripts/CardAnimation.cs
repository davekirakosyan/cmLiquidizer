using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardAnimation : MonoBehaviour
{
    // card positions for moving animation
    private Vector3 startPos;
    public Vector3 endPos;

    private IEnumerator animationCorountine;

    private bool disolveComplete = false;
    private bool animateComplete = false;
    private bool appearComplete = false;

    // card position after selection
    private Vector3 selectionPos;

    // card animation speeds
    public float MovingSpeed;
    public float DisolveSpeed;
    public float AppearSpeed;

    // lerp values for every animation
    private float MovingLerp = 0;
    private float DisolvingLerp = 0;
    private float AppearLerp = 0;

    // card animation states
    private bool AnimateCardState;
    private bool DisapearCardState;
    private bool AppearCardState;

    // card fade out animation variables
    private Image image;
    private Color colorStart;
    private Color colorEnd;

    void Awake()
    {
        // set all states to false prevents any animation
        AnimateCardState = DisapearCardState = AppearCardState = false;

        // getting image for fade out
        image = this.GetComponent<Image>();

        // At the start cards are transperent
        colorStart = image.color; 
        colorEnd = image.color;
        colorEnd.a = 1;
    }

    // Animation handler for selected card
    private IEnumerator _AnimateSelectedCard()
    {
        MovingLerp += Time.deltaTime / MovingSpeed;
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, endPos, MovingLerp);
        while (transform.localPosition != endPos)
        {
            animateComplete = true;
            yield return new WaitForFixedUpdate();
        }
    }

    // Card disolving animation handler, also disabling curent card gameObject
    private IEnumerator _CardDisolve()
    {
        DisolvingLerp += Time.deltaTime / DisolveSpeed;
        image.color = Color.Lerp(colorEnd, colorStart, DisolvingLerp);
        if (image.color.a != colorStart.a)
        {
            disolveComplete = true;
            if (this.gameObject.activeInHierarchy)
               this.gameObject.SetActive(false);

            yield return new WaitForFixedUpdate();
        }
    }

    // card appearence animation handler
    private IEnumerator _AppearCard()
    {
        AppearLerp += Time.deltaTime / AppearSpeed;
        image.color = Color.Lerp(colorStart, colorEnd, AppearLerp);

        MovingLerp += Time.deltaTime / MovingSpeed;
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, this.selectionPos, MovingLerp);

        if (transform.localPosition != selectionPos)
        {
            appearComplete = true;
            yield return new WaitForFixedUpdate();
        }
    }

    // Card selecting animation 
    public void AnimateSelectedCard()
    {
        DisapearCardState = false;
        AnimateCardState = true;
        AppearCardState = false;
    }

    // Card disolving animation
    public void CardDisolve()
    {
        DisapearCardState = true;
        AnimateCardState = false;
        AppearCardState = false;
    }

    // Card appearence animation
    public void AppearCard(float selectionPosX, float selectionPosY)
    {
        DisapearCardState = false;
        AnimateCardState = false;
        AppearCardState = true;
        selectionPos = new Vector3(selectionPosX, selectionPosY, 0.0f);
    }

    public bool GetDisolveComplition()
    {
        return disolveComplete;
    }

    public bool GetAppearComplition()
    {
        return appearComplete;
    }

    public bool GetAnimateComplition()
    {
        return animateComplete;
    }

    void FixedUpdate()
    {
        if (!AppearCardState && AnimateCardState && !DisapearCardState)
        {
            animationCorountine = _AnimateSelectedCard();
        }
        else if (!AppearCardState && !AnimateCardState && DisapearCardState)
        {
            animationCorountine = _CardDisolve();
        }
        else if (AppearCardState && !AnimateCardState && !DisapearCardState)
        {
            animationCorountine = _AppearCard();
        }

        StartCoroutine(animationCorountine);
    }
}
