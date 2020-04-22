using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElixirController : MonoBehaviour
{
    public Transform pathPoints;
    public GameObject elixirPrefab;
    Vector3[] movementCoordinates;

    void Start()
    {
        // construct movement coordinates
        int n = pathPoints.childCount;
        if (n != 0)
        {
            movementCoordinates = new Vector3[n];
            for (int i=0; i<n; i++)
            {
                movementCoordinates[i] = pathPoints.GetChild(i).position;
            }
        }
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
                if (hit.collider.CompareTag("tube"))
                {
                    print("kakashkaa");
                    GameObject elixir = Instantiate(elixirPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    elixir.GetComponent<Elixir>().movementCoordinates = movementCoordinates;
                    elixir.GetComponent<Elixir>().speed = 4;
                    elixir.GetComponent<Elixir>().lastPos = 0;
                }
                //Debug.DrawLine(ray.origin, hit.point, Color.red);
            }
        }
    }
}
