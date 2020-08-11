//Add this script and SwipeController to EmptyGameObject attach SwipeController to swipeController variable (DUH! I hate comments)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSwipe : MonoBehaviour
{
    public SwipeController swipeController;
    public Transform moveObject;
    private Vector3 endPosition;

    private void Start()
    {
        endPosition = moveObject.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (swipeController.SwipeUp)
            endPosition += Vector3.down * 11f;
        else  if (swipeController.SwipeDown)
            endPosition += Vector3.up * 11f;

        moveObject.transform.position = Vector3.MoveTowards(moveObject.transform.position, endPosition, 33f * Time.deltaTime);
    }
}
