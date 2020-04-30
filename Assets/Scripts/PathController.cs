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
    public List<InventoryManager.ElixirColor> liveElixirColors = new List<InventoryManager.ElixirColor>();
    bool checkCountdownInProgress = false;

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
                CreateElixir(hit.point, gameManager.currentLevel.elixirSpeed, gameManager.currentLevel.elixirLength, GameManager.selectedColor);
            }
            if (!checkCountdownInProgress)
            {
                StartCoroutine(CheckReseults());
            }
        }
    }

    public void CreateElixir (Vector3 position, float speed, float length, InventoryManager.ElixirColor colorName)
    {
        GameObject elixir = Instantiate(elixirPrefab);
        elixir.transform.position = position;
        elixir.GetComponent<Elixir>().speed = speed;
        elixir.GetComponent<Elixir>().length = length;
        elixir.GetComponent<Elixir>().colorName = colorName;
        elixir.GetComponent<Elixir>().pathCreator = pathCreator;
        elixir.GetComponent<Elixir>().pathController = this;
        // adding a unique number to each elixir for easy tracking and self collision issues
        elixir.GetComponent<Elixir>().uniqueNumber = nextUniqueNumber;
        nextUniqueNumber++;

        liveElixirs.Add(elixir);
        liveElixirColors.Add(colorName);
        inventoryManager.RemoveUsedItemFromInventory();
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
        checkCountdownInProgress = true;
        yield return new WaitForSeconds(pathCreator.path.length / gameManager.currentLevel.elixirSpeed);
        checkCountdownInProgress = false;

        if (inventoryManager.IsInvenotoryEmpty())
        {
            // check if the output is right
            bool isRequirementDone = true;
            foreach (InventoryManager.ElixirColor color in gameManager.currentOutput)
            {
                if (!liveElixirColors.Contains(color))
                {
                    isRequirementDone = false;
                }
            }
            
            if (isRequirementDone && gameManager.world < gameManager.PATHS.Length - 1)
            {
                gameManager.winningMsg.SetActive(true);
            } 
            else if (isRequirementDone && gameManager.world >= gameManager.PATHS.Length - 1)
            {
                gameManager.endGameMsg.SetActive(true);
            }
            else
            {
                gameManager.gameOverMsg.SetActive(true);
            }
            GameManager.gameOn = false;
        }
    }
}
