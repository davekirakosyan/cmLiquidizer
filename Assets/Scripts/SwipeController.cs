using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private bool swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDragging = false;
    private Vector2 swipeDelta;

    public float swipeThreshold = 100.0f;

    void OnSwipe(Vector2 data)
    {
        isDragging = true;
        if (data.magnitude > swipeThreshold)
        {
            //check direction
            float x = data.x;
            float y = data.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or right
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                //up or down
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }
            swipeDelta = Vector2.zero;
        }
    }

    IEnumerator SwipeInput(Action<Vector2> onSwipe)
    {
        Dictionary<int, Touch> activeTouches = new Dictionary<int, Touch>();
        Dictionary<int, Vector3> activeButtons = new Dictionary<int, Vector3>();
        while (true)
        {
            if (Input.touches.Length > 0)
            {
                switch (Input.touches[0].phase)
                {
                    case TouchPhase.Began:
                        activeTouches[0] = Input.touches[0];
                        break;
                    case TouchPhase.Ended:
                        if (activeTouches.ContainsKey(0))
                        {
                            Vector2 delta = Input.touches[0].position - activeTouches[0].position;
                            if (delta.magnitude > swipeThreshold)
                                onSwipe(delta);
                        }
                        break;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    activeButtons[0] = Input.mousePosition;
                }
                else if (Input.GetMouseButtonUp(0) && activeButtons.ContainsKey(0))
                {
                    Vector2 delta = Input.mousePosition - activeButtons[0];
                    if (delta.magnitude > swipeThreshold)
                        onSwipe(delta);
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    void FixedUpdate()
    {
        isDragging = false;
        swipeLeft = swipeDown = swipeUp = swipeRight = false;

        // Mouse input
        // or
        // Mobile input
        if (Input.GetMouseButtonDown(0) || ((Input.touches.Length != 0) && (Input.touches[0].phase == TouchPhase.Began)))
            StartCoroutine(SwipeInput(OnSwipe));
    }

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool IsDragging { get { return isDragging; } }
}
