using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElixirController : MonoBehaviour
{
    public Transform pathPoints;
    public GameObject elixirPrefab;

    void Start()
    {
        int n = pathPoints.childCount;
        if (n != 0)
        {
            Vector3[] movementCoordinates = new Vector3[n];

            for (int i=0; i<n; i++)
            {
                movementCoordinates[i] = pathPoints.GetChild(i).position;
            }
        
            GameObject elixir1 = Instantiate(elixirPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            elixir1.transform.localScale = new Vector3(0.07f, 0.15f, 0.07f);
            elixir1.GetComponent<Elixir>().movementCoordinates = movementCoordinates;
            elixir1.GetComponent<Elixir>().speed = 2;
            elixir1.GetComponent<Elixir>().lastPos = 0;
        }

    }
}
