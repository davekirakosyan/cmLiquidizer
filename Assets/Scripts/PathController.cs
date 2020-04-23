using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathController : MonoBehaviour
{
    public GameObject elixirPrefab;
    public PathCreator pathCreator;

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
                if (hit.collider.CompareTag("tube"))
                {
                    // if clicked on tubes create an elixir at that hit point and initialize its main components
                    GameObject elixir = Instantiate(elixirPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    elixir.transform.position = hit.point;
                    elixir.GetComponent<Elixir>().speed = 2;
                    elixir.GetComponent<Elixir>().pathCreator = pathCreator;
                    elixir.GetComponent<Elixir>().color = new Color(Random.value, Random.value, Random.value);
                }
            }
        }
    }
}
