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
    public CardSelection cardSelection;

    public GameObject gameOverMsg;
    public GameObject winningMsg;
    public GameObject endGameMsg;
    public Dropdown worldDropdown;
    public Text outputText;
    public Text worldLevelText;

    public GameObject currentPath;
    public GameObject[] PATHS;

    public Assignment currentLevel;
    public List<InventoryManager.ElixirColor> currentInput;
    public List<InventoryManager.ElixirColor> currentOutput;

    private bool needUpdateLevelCards;
    private static bool firstBoot = true;

    private void Awake()
    {
        // Check if it first boot, if it is then initialize some variables
        if (firstBoot)
        {
            needUpdateLevelCards = true;
            cardSelection.inventoryManager = inventoryManager;

            // disable later checks for first boot
            firstBoot = false;
        }

        // load data
        world = PlayerPrefs.GetInt("World");
        level = PlayerPrefs.GetInt("Level");

        // load selected world
        LoadWorld(world);
    }

    // update level data in PlayerPrefs
    void UpdateUserData()
    {
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("World", world);
    }

    public static void SelectElixir(GameObject elixir)
    {
        if (selectedElixir != null)
        {
            //unhighlight previous selected item
            selectedElixir.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        selectedColor = elixir.GetComponent<InventoryItem>().colorName;
        selectedElixir = elixir;

        //highlight selected item in inventory
        selectedElixir.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
    }

    public void ResetGame(bool forcedReset = false)
    {
        // remove all elixir bottles from the inventory due they will create by user card choice
        inventoryManager.removeInventoryItems();

        // delete all the existing elixirs on the path
        foreach (GameObject elixir in pathController.liveElixirs)
            pathController.DestroyElixir(elixir);

        pathController.liveElixirs.Clear();
        pathController.liveElixirColors.Clear();

        gameOn = true;

        // hide popups
        gameOverMsg.SetActive(false);
        endGameMsg.SetActive(false);
        winningMsg.SetActive(false);

        // stop the existing countdown
        if (pathController.countDown != null)
        {
            StopCoroutine(pathController.countDown);
            pathController.checkCountdownInProgress = false;
            pathController.countdownText.gameObject.SetActive(false);
        }

        // if user presed the RESET button there will be generated all new level cards
        if (forcedReset)
        {
            // prepare elixir bottles for inventory
            cardSelection.currentInventory = currentInput;

            // generate level selection cards
            cardSelection.CardGeneration();
        }
    }

    // to perform refil after current level restarting
    public void RefillInvetnory()
    {
        inventoryManager.FillInventory(currentInput);
    }

    public void CleanCompletedLevelNotes()
    {
        // remove completed level notes from memory
        PlayerPrefs.DeleteKey("Completed Levels");
    }

    public void NextLevel()
    {
        // set current level card as completed
        cardSelection.CompleteLevel(level);

        level++;

        // go to the next level (if there are uncompleted levels)
        if (cardSelection.GetCompletedLevelCount() != currentPath.GetComponent<Path>().levels.Length)
        {
            needUpdateLevelCards = false;
            ResetGame();
        }

        // if the current path has no more levels to complete go to the level 0 of the next path
        else if (world < PATHS.Length)
        {
            level = 0;
            world++;
            CleanCompletedLevelNotes();
            needUpdateLevelCards = true;
            LoadWorld(world);
        }

        // User Specific data
        UpdateUserData();
    }

    public void LoadWorld(int selectedWorld)
    {
        // check if there is at least one world path, and the current world has a path
        if (PATHS.Length != 0 && selectedWorld < PATHS.Length && selectedWorld >= -1)
        {
            // perform reset to clean the user space UI
            ResetGame();

            // check if there is a new world value from UI
            if (selectedWorld == -1)
            {
                world = worldDropdown.value;
                selectedWorld = world;
            }

            // get level from user selection
            level = cardSelection.GetLevel();

            // if there is any existing path remove it
            for (int i = 0; i < pathController.gameObject.transform.childCount; i++)
                Destroy(pathController.gameObject.transform.GetChild(i).gameObject);

            // instantiate the world path
            currentPath = Instantiate(PATHS[selectedWorld], pathController.gameObject.transform);
            pathController.pathCreators = currentPath.GetComponent<Path>().pathCreators;

            // create level || TODO: Need to revisit !!!!!!
            CreateLevel();

            UpdateLevelText(); // TODO: Need to remove!!!!!!

            gameOn = true;

            // Show level selection Cards
            if (needUpdateLevelCards)
                cardSelection.CardGeneration();
            else
                cardSelection.ShowLevelCards();

            // update world drop down menu for UI
            UpdateWorldDropdownMenuValues();

            // update user Specific data
            UpdateUserData();
        }
    }

    private void CreateLevel()
    {
        if (level >= currentPath.GetComponent<Path>().levels.Length)
            level = currentPath.GetComponent<Path>().levels.Length - 1;

        // load the current level with its recuirements
        // check if the path has levels and the selected level is in the range
        if (currentPath.GetComponent<Path>().levels.Length != 0 && level >= 0)
        {
            currentLevel = currentPath.GetComponent<Path>().levels[level].GetComponent<Assignment>();
            currentInput = currentLevel.inputColors;
            currentOutput = currentLevel.outputColors;

            // prepare elixir bottles for inventory
            cardSelection.currentInventory = currentInput;

            // display output by text (temporary solution)
            outputText.GetComponent<Text>().text = "";
            foreach (InventoryManager.ElixirColor elixir in currentOutput)
                outputText.GetComponent<Text>().text += elixir + " ";

            UpdateLevelText(); // TODO: Need to remove!!!!!!
        }
    }

    // TODO: Need to revisit !!!!!
    public void ChangeLevel(int selectedLevel)
    {
        level = selectedLevel;
        UpdateUserData();
        ResetGame();
        CreateLevel();
    }

    // this is temporary
    void UpdateLevelText()
    {
        worldLevelText.GetComponent<Text>().text = "World " + (world + 1) + ", Level " + (level + 1);
    }

    // this is temporary too
    void UpdateWorldDropdownMenuValues()
    {
        // this check prevents LoadWorld() function unnecessary call
        if (worldDropdown.value != world)
        {
            worldDropdown.value = world;
        }
    }
};
