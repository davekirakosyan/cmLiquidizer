﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathController : MonoBehaviour
{
    public GameObject elixirPrefab;
    public PathCreator pathCreator;
    public Color[] colors;
    int nextUniqueNumber = 0;

    void Start()
    {
        
    }

    void Update()
    {
        // detect the click on the tubes to pour elixir
        if (Input.GetMouseButtonDown(0))
        {
            // cast a ray and check if it hits any of tube coliders
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 200))
            {
                if (hit.transform.gameObject.layer == 8) // layer 8 is Path
                {
                    // if clicked on tubes create an elixir at that hit point and initialize its main components
                    GameObject elixir = Instantiate(elixirPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    elixir.transform.position = hit.point;
                    elixir.GetComponent<Elixir>().speed = 2;
                    elixir.GetComponent<Elixir>().pathCreator = pathCreator;
                    if (colors.Length != 0)
                        elixir.GetComponent<Elixir>().color = colors[Random.Range(0, colors.Length)];
                    // adding a unique number to each elixir for easy tracking and self collision issues
                    elixir.GetComponent<Elixir>().uniqueNumber = nextUniqueNumber;
                    nextUniqueNumber++;
                }
            }
        }
    }
}
