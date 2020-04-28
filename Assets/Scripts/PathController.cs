using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathController : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject elixirPrefab;
    public static bool dragged = false;
    public PathCreator pathCreator;
    int nextUniqueNumber = 0;
    public GameObject gameOverMsg;
    public InventoryManager inventoryManager;
    public List<GameObject> liveElixirs = new List<GameObject>();

    void Update()
    {
        // detect the click on the tubes to pour elixir
        if (Clicked() && GameManager.gameOn)
        {
            PourElixir();
            dragged = false;
        }
    }

    public void PourElixir()
    {
        // cast a ray and check if it hits any of tube coliders
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 200))
        {
            if (hit.transform.gameObject.layer == 8) // layer 8 is Path
            {
                // if clicked on tubes create an elixir at that hit point and initialize its main components
                CreateElixir(hit.point, 2, GameManager.selectedColor);
            }
        }
    }

    public void CreateElixir (Vector3 position, float speed, InventoryManager.ElixirColor colorName)
    {
        GameObject elixir = Instantiate(elixirPrefab);
        elixir.transform.position = position;
        elixir.GetComponent<Elixir>().speed = speed;
        elixir.GetComponent<Elixir>().colorName = colorName;
        elixir.GetComponent<Elixir>().pathCreator = pathCreator;
        elixir.GetComponent<Elixir>().pathController = this;
        // adding a unique number to each elixir for easy tracking and self collision issues
        elixir.GetComponent<Elixir>().uniqueNumber = nextUniqueNumber;
        nextUniqueNumber++;

        liveElixirs.Add(elixir);
        inventoryManager.RemoveUsedItemFromInventory();
        StartCoroutine(CheckReseults());
    }


    public void DestroyElixir(GameObject elixir)
    {
        Destroy(elixir.transform.GetChild(0).gameObject);   // first get rid of their heads to prevent future collision
        elixir.GetComponent<ParticleSystem>().Stop();       // stop emmision
        Destroy(elixir.GetComponent<Elixir>());             // destroy the running script
        StartCoroutine(WaitAndDestroy(elixir, 0.5f));       // start waiting for particle to die and destroy the elixir gameobject
    }

    IEnumerator WaitAndDestroy (GameObject elixir, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(elixir);
    }

    /// returns if screen has been clicked or touched
    private bool Clicked ()
    {
        if (Input.touchSupported)   // check if the device supports touch 
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended|| dragged)
                return true;
            else return false;
        }
        else                        // EDITOR
        {
            if (Input.GetMouseButtonDown(0)||dragged)
                return true;
            else return false;
        }
    }

    IEnumerator CheckReseults()
    {
        yield return new WaitForEndOfFrame();
        if (inventoryManager.IsInvenotoryEmpty())
        {
            List<InventoryManager.ElixirColor> usedColors = new List<InventoryManager.ElixirColor>();
            foreach (GameObject elixir in liveElixirs)
            {
                usedColors.Add(elixir.GetComponent<Elixir>().colorName);
            }

            bool isRequirementDone = true;
            // check if the output is right
            foreach (InventoryManager.ElixirColor color in gameManager.currentOutput)
            {
                print(color);
                if (!usedColors.Contains(color))
                {
                    isRequirementDone = false;
                }
            }
            print(isRequirementDone); 
        }
    }
}
