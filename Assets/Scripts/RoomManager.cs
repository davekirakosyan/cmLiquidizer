using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public GameObject roomView;
    public GameObject tableView;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
