using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                if (hit.collider.tag.Contains("work table"))
                {
                    tableView.SetActive(true);
                    roomView.SetActive(false);
                }
            }
        }
    }
}
