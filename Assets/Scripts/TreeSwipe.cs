//Add this script and SwipeController to EmptyGameObject attach SwipeController to swipeController variable (DUH! I hate comments)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSwipe : MonoBehaviour
{
    public SwipeController swipeController;
    public Transform moveObject;
    private Vector3 startPosition, endPosition;

    public float moveUnit;

    private void Start()
    {
        startPosition = moveObject.transform.position;
        endPosition = startPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveUnit = 10;

        if (swipeController.SwipeUp)
            endPosition += Vector3.down * moveUnit;
        else  if (swipeController.SwipeDown)
            endPosition += Vector3.up * moveUnit;

        if(endPosition.y>=startPosition.y)
            moveObject.transform.position = Vector3.MoveTowards(moveObject.transform.position, endPosition, 3f * moveUnit * Time.deltaTime);
    }
}
