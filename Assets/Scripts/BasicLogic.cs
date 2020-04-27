using System;
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
    public Dropdown worldDropdown;
    public Dropdown levelDropdown;
    public Text outputText;
    public Text worldLevelText;

    public GameObject[] PATHS;
    public GameObject currentPath;
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
        if (PATHS.Length != 0 
            && world < PATHS.Length && world >= 0)  // check if there is at least one world path, and the current world has a path
        {
            // instantiate the world path
            currentPath = Instantiate(PATHS[world], pathController.gameObject.transform);
            pathController.pathCreator = currentPath.GetComponent<Path>().pathCreator;

            CreateLevel();
            UpdateLevelText();

            gameOn = true;
        }
    }

    private void CreateLevel()
    {
        // load the current level with its recuirements
        if (currentPath.GetComponent<Path>().levels.Length != 0 && 
            level < currentPath.GetComponent<Path>().levels.Length && level >= 0)   // check if the path has levels and the selected level is in the range
        {
            Assignment lvl = currentPath.GetComponent<Path>().levels[level].GetComponent<Assignment>();
            currentInput = lvl.inputColors;
            currentOutput = lvl.outputColors;
            inventoryManager.FillInventory(currentInput);

            // display output by text (temporary solution)
            outputText.GetComponent<Text>().text = "";
            for (int i = 0; i < currentOutput.Length; i++)
            {
                outputText.GetComponent<Text>().text += currentOutput[i] + " ";
            }
            UpdateLevelText();
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
        world = worldDropdown.value;
        ResetGame();
        CreateWorld();
    }

    public void ChangeLevel ()
    {
        level = levelDropdown.value;
        ResetGame();
        CreateLevel();
    }

    void UpdateLevelText ()
    {
        worldLevelText.GetComponent<Text>().text = "World " + (world+1) + ", Level " + (level+1);
    }
}
