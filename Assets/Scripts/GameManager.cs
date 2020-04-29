using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // global variables
    public static InventoryManager.ElixirColor selectedColor;
    public static GameObject selectedElixir;
    public static bool gameOn = false;
    public static int world = 0;
    public static int level = 0;

    public InventoryManager inventoryManager;
    public PathController pathController;

    public GameObject content;
    public static GameObject staticContent;

    public GameObject gameOverMsg;
    public GameObject winningMsg;
    public Dropdown worldDropdown;
    public Dropdown levelDropdown;
    public Text outputText;
    public Text worldLevelText;

    public GameObject[] PATHS;
    public GameObject currentPath;

    public List<InventoryManager.ElixirColor> currentInput;
    public List<InventoryManager.ElixirColor> currentOutput;

    private void Awake()
    {
        CreateWorld();
        staticContent = content;
    }

    public static void SelectElixir(GameObject elixir)
    {
        selectedColor = elixir.GetComponent<InventoryItem>().colorName;
        selectedElixir = elixir;
        for (int childIndex = 0; childIndex < staticContent.transform.childCount; childIndex++)
        {
            if (staticContent.transform.GetChild(childIndex).gameObject == selectedElixir)
                staticContent.transform.GetChild(childIndex).GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            else
                staticContent.transform.GetChild(childIndex).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    public void ResetGame()
    {
        // delete all the existing elixirs on the path
        foreach (GameObject elixir in pathController.liveElixirs)
        {
            pathController.DestroyElixir(elixir);
        }
        pathController.liveElixirs.Clear();
        pathController.liveElixirColors.Clear();
       
        inventoryManager.FillInventory(currentInput);   // refill inventory
        gameOn = true;
        gameOverMsg.SetActive(false);
        winningMsg.SetActive(false);
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
            foreach (InventoryManager.ElixirColor elixir in currentOutput)
            {
                outputText.GetComponent<Text>().text += elixir + " ";
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
