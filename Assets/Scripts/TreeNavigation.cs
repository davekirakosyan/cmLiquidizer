using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreeNavigation : MonoBehaviour
{
    public GameObject FloorComfirmation;
    public TutorialManagerTreeView tutorialManager;
    private RaycastHit hit;
    public SwipeController swipeController;

    private void Start()
    {
        FloorComfirmation.SetActive(false);
    }

    private void Update()
    {
        if (!swipeController.IsDragging)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
        }
    }

    public void EnterFloor()
    {
        PlayerPrefs.SetInt("World", hit.collider.gameObject.GetComponent<Floor>().floorNumber - 1);
        CrossScene.LoadTable = false;
        SceneManager.LoadScene(1);
    }
}
