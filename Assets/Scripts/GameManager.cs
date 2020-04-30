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
    public int world;
    public int level;

    public InventoryManager inventoryManager;
    public PathController pathController;
 
    public GameObject gameOverMsg;
    public GameObject winningMsg;
    public GameObject endGameMsg;
    public Dropdown worldDropdown;
    public Dropdown levelDropdown;
    public Text outputText;
    public Text worldLevelText;

    public GameObject currentPath;
    public GameObject[] PATHS;

    public Assignment currentLevel;
    public List<InventoryManager.ElixirColor> currentInput;
    public List<InventoryManager.ElixirColor> currentOutput;

    private void Awake()
    {
        // load data
        world = PlayerPrefs.GetInt("World");
        level = PlayerPrefs.GetInt("Level");
        CreateWorld();
        UpdateLevelDropdownMenuValues();
    }

    // update level data in PlayerPrefs
    void UpdateLevelData()
    {
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("World", world);
    }

    public static void SelectElixir(GameObject elixir)
    {

        if (selectedElixir != null)
        {
            //unhighlight previous selected item
            selectedElixir.GetComponent<Image>().color= new Color(1, 1, 1, 1);
        }
        selectedColor = elixir.GetComponent<InventoryItem>().colorName;
        selectedElixir = elixir;
        //highlight selected item in inventory
        selectedElixir.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
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
        endGameMsg.SetActive(false);
        winningMsg.SetActive(false);
    }

    public void NextLevel()
    {
        // go to the next level (if it exists)
        level++;
        if (level < currentPath.GetComponent<Path>().levels.Length)
        {
            UpdateLevelDropdownMenuValues();
        }
        else if (level >= currentPath.GetComponent<Path>().levels.Length && world < PATHS.Length)
        {
            // if the current path has no more levels go to the level 0 of the next path
            world++;
            level = 0;
            UpdateLevelDropdownMenuValues();
            ChangeWorld();
        }
        UpdateLevelData();

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
        if (level >= currentPath.GetComponent<Path>().levels.Length)
        {
            level = currentPath.GetComponent<Path>().levels.Length - 1;
            UpdateLevelDropdownMenuValues();
        }

        // load the current level with its recuirements
        if (currentPath.GetComponent<Path>().levels.Length != 0 && level >= 0)   // check if the path has levels and the selected level is in the range
        {
            currentLevel = currentPath.GetComponent<Path>().levels[level].GetComponent<Assignment>();
            currentInput = currentLevel.inputColors;
            currentOutput = currentLevel.outputColors;
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
        level = levelDropdown.value;

        UpdateLevelData();
        ResetGame();
        CreateWorld();
    }

    public void ChangeLevel ()
    {
        level = levelDropdown.value;
        UpdateLevelData();
        ResetGame();
        CreateLevel();
    }

    // this is temporary
    void UpdateLevelText ()
    {
        worldLevelText.GetComponent<Text>().text = "World " + (world+1) + ", Level " + (level+1);
    }

    // this is temporary too
    void UpdateLevelDropdownMenuValues ()
    {
        levelDropdown.value = level;
        worldDropdown.value = world;
    }
}
