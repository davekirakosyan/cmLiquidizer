using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathController : MonoBehaviour
{
    public GameObject elixirPrefab;
    public PathCreator pathCreator;
    int nextUniqueNumber = 0;

    void Update()
    {
        // detect the click on the tubes to pour elixir
        if (Clicked())
        {
            // cast a ray and check if it hits any of tube coliders
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 200))
            {
                if (hit.transform.gameObject.layer == 8) // layer 8 is Path
                {
                    // if clicked on tubes create an elixir at that hit point and initialize its main components
                    GameObject elixir = Instantiate(elixirPrefab);
                    elixir.transform.position = hit.point;
                    elixir.GetComponent<Elixir>().speed = 2;
                    elixir.GetComponent<Elixir>().pathCreator = pathCreator;
                    elixir.GetComponent<Elixir>().colorName = BasicLogic.selectedColor;
                    // adding a unique number to each elixir for easy tracking and self collision issues
                    elixir.GetComponent<Elixir>().uniqueNumber = nextUniqueNumber;
                    nextUniqueNumber++;
                }
            }
        }
    }

    /// returns if screen has been clicked or touched
    private bool Clicked ()
    {
        if (Input.touchSupported)   // check if the device supports touch 
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
                return true;
            else return false;
        }
        else                        // EDITOR
        {
            if (Input.GetMouseButtonDown(0))
                return true;
            else return false;
        }
    }
}
