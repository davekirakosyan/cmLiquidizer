﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public GameObject roomView;
    public GameObject tableView;
    public TutorialManagerRoom tutorialManagerRoom;

    private void Start()
    {
        if (CrossScene.LoadTable)
        {
            tableView.SetActive(true);
            roomView.SetActive(false);
        }
    }

    public void BackButton()
    {
        if (tableView.activeInHierarchy)
        {
            tableView.SetActive(false);
            roomView.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(0);
            CrossScene.LoadTable = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 200))
            {
                // detect the click on the table
                if (hit.collider.tag.Contains("work table"))
                {
                    tableView.SetActive(true);
                    roomView.SetActive(false);
                    tutorialManagerRoom.tableClick = true;
                }
                // detect the click on the path, to start the game
                else if (hit.collider.tag.Contains("path"))
                {
                    SceneManager.LoadScene(2);
                }
            }
        }
    }
}
