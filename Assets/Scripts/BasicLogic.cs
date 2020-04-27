using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicLogic : MonoBehaviour
{
    // global variables
    public static InventoryManager.ElixirColor selectedColor;
    public static bool gameOn = false;
    public static int world = 0;
    public static int level = 0;

    public InventoryManager inventoryManager;
    public PathController pathController;

    public GameObject gameOverMsg;
    public Dropdown levelDropdown;

    public GameObject[] PATHS;
    public InventoryManager.ElixirColor[] currentInput;
    public InventoryManager.ElixirColor[] currentOutput;

    private void Awake()
    {
        CreateWorld();
    }

    public static void SelectElixir(InventoryManager.ElixirColor color)
    {
        selectedColor = color;
    }

    public void ResetGame()
    {
        // remove all the existing ellixirs
        GameObject[] allElixirs = GameObject.FindGameObjectsWithTag("elixir");
        for (int i = 0; i < allElixirs.Length; i++)
        {
            pathController.DestroyElixir(allElixirs[i]);
        }
        gameOn = true;
        gameOverMsg.SetActive(false);
    }

    public void CreateWorld()
    {
        if (PATHS.Length != 0 && world < PATHS.Length && world >= 0)  // check if there is at least one world path, and the current world has a path
        {
            // instantiate the world path
            GameObject newPath = Instantiate(PATHS[world], pathController.gameObject.transform);
            pathController.pathCreator = newPath.GetComponent<Path>().pathCreator;

            Assignment lvl = newPath.GetComponent<Path>().levels[level].GetComponent<Assignment>();

            currentInput = lvl.inputColors;
            currentOutput = lvl.outputColors;

           inventoryManager.FillInventory(currentInput);

            gameOn = true;
        }
    }

    public void ChangeWorld ()
    {
        // if there is any existing path remove it
        if (pathController.gameObject.transform.childCount != 0)
        {
            Destroy(pathController.gameObject.transform.GetChild(0).gameObject);
        }
        // change into a new world from the dropdown list
        world = levelDropdown.value;
        ResetGame();
        CreateWorld();
    }
}
