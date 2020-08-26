using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNavigation : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 200))
            {
                if (hit.collider.tag.Contains("floor"))
                {
                    print(hit.collider.GetComponent<Floor>().floorNumber);
                }
            }
        }
    }
}
