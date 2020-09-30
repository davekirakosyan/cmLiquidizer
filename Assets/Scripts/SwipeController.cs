using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour
{
    public int FloorCount;
    public float moveUnit;
    public float swipeThreshold;

    public Transform moveObject;
    public GameObject FloorComfirmation;
    public TutorialManagerTreeView tutorialManager;

    private RaycastHit hit;
    private Vector3 startPosition, endPosition, topFloor;

    private Touch touch;
    private bool tap = false;
    private Vector3 beginTouchPosition, endTouchPosition;

    private void Start()
    {
        FloorComfirmation.SetActive(false);

        if (swipeThreshold < 10)
            swipeThreshold = 30.0f;

        if (moveUnit < 1)
            moveUnit = 10;

        startPosition = moveObject.transform.position;
        endPosition = startPosition;
        if (FloorCount < 1)
            FloorCount = 1;
        else
            FloorCount -= 1;

        topFloor = startPosition + Vector3.up * FloorCount * moveUnit;
    }

    void OnSwipe(Vector2 data)
    {
        if (data.magnitude > swipeThreshold)
        {
            //check direction
            float x = data.x;
            float y = data.y;

            if (Mathf.Abs(x) < Mathf.Abs(y))
            {
                if (y < 0)
                {
                    if (endPosition.y >= startPosition.y && endPosition.y < topFloor.y)
                        endPosition += Vector3.up * moveUnit;
                }
                else
                {
                    if (endPosition.y > startPosition.y && endPosition.y <= topFloor.y)
                        endPosition += Vector3.down * moveUnit;
                }
            }
        }
    }

    private void OnTouch(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 200))
        {
            if (hit.collider.tag.Contains("floor"))
            {
                if (tutorialManager.GetTutorialState())
                {
                    FloorComfirmation.SetActive(true);
                }
                else
                {
                    EnterFloor();
                }
            }
        }
    }

    public void EnterFloor()
    {
        PlayerPrefs.SetInt("World", hit.collider.gameObject.GetComponent<Floor>().floorNumber - 1);
        CrossScene.LoadTable = false;
        SceneManager.LoadScene(1);
    }

    private void processTouch(Vector3 begin, Vector3 end)
    {
        if (begin == end)
        {
            OnTouch(end);
        }
        else if (begin != end)
        {
            Vector2 delta = end - begin;
            if (delta.magnitude > swipeThreshold)
                OnSwipe(delta);
            else
                OnTouch(end);
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            switch(touch.phase)
            {
                case TouchPhase.Began:
                    beginTouchPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    processTouch(beginTouchPosition, endTouchPosition);
                    break;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !tap)
            {
                tap = true;
                beginTouchPosition = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                tap = false;
                endTouchPosition = Input.mousePosition;
                processTouch(beginTouchPosition, endTouchPosition);
            }
        }

        if (endPosition.y != moveObject.transform.position.y && PlayerPrefs.GetInt("Tutorial completed") == 1)
            moveObject.transform.position = Vector3.MoveTowards(moveObject.transform.position, endPosition, 3f * moveUnit * Time.deltaTime);
    }
}
