/*****************************************************************************************************************

Old Player prefs saving version is not removed, instead is commented in its respective place,, will be removed after testing. TODO : Remove old saving system after testing.

********************************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Boomlagoon.JSON;
using BlowFishCS;
using System.IO;

public class GameManager : MonoBehaviour
{
    // global variables
    public JSONObject userData;

    public static InventoryManager.ElixirColor selectedColor;
    public static GameObject selectedElixir;
    public int world;
    public int level;

    public InventoryManager inventoryManager;
    public PathController pathController;
    public CardSelection cardSelection;

    public GameObject gameOverMsg;
    public GameObject winningMsg;
    public GameObject endGameMsg;
    public Dropdown worldDropdown;

    public GameObject currentPath;
    public GameObject[] PATHS;

    public Assignment currentLevel;
    public List<InventoryManager.ElixirColor> currentInput;
    public List<InventoryManager.ElixirColor> currentOutput;

    private bool needUpdateLevelCards;
    private static bool firstBoot = true;

    private bool needToUpdateGameOver = false;
    private bool needToUpdateWinning = false;

    //Color blind mode, 0-normal, 1-protanopia, 2-deuteranopia, 3-tritanopia
    public int colorBlindMode = 0;
    public Dropdown colorBlindDropdown;
    public Image[] colorExample;

    BlowFish bf = new BlowFish("04B915BA43FEB5B6");

    string path = "Assets/Resources/Text/User data.txt";

    public bool GetUpdateGameOver()
    {
        return needToUpdateGameOver;
    }

    public bool GetUpdateWinning()
    {
        return needToUpdateWinning;
    }

    public void SetUpdateGameOver(bool value)
    {
        needToUpdateGameOver = value;
    }

    public void SetUpdateWinning(bool value)
    {
        needToUpdateWinning = value;
    }

    private void Awake()
    {

        StreamReader reader = new StreamReader(path);
        userData = JSONObject.Parse(reader.ReadToEnd());
        reader.Close();

        // Check if it first boot, if it is then initialize some variables
        if (firstBoot)
        {
            needUpdateLevelCards = true;

            // disable later checks for first boot
            firstBoot = false;
        }

        // load data

        //world = PlayerPrefs.GetInt("World");
        world = int.Parse(bf.Decrypt_CBC(userData.GetString("World")));

        //level = PlayerPrefs.GetInt("Level");
        level = int.Parse(bf.Decrypt_CBC(userData.GetString("Level")));

        // load selected world
        LoadWorld(world);

        /*if (!PlayerPrefs.HasKey("Color blind mode"))
            PlayerPrefs.SetInt("Color blind mode", 0);
        */
        if (!userData.ContainsKey("Color blind mode"))
            userData.Add("Color blind mode", bf.Encrypt_CBC("0"));

        //colorBlindMode = PlayerPrefs.GetInt("Color blind mode");
        colorBlindMode = int.Parse(bf.Decrypt_CBC(userData.GetString("Color blind mode")));
        colorBlindDropdown.value = colorBlindMode;
    }

    // update level data in PlayerPrefs
    void UpdateUserData()
    {
        /*PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("World", world);*/

        userData.Remove("Level");
        userData.Add("Level", bf.Encrypt_CBC(level.ToString()));
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(userData.ToString());
        writer.Close();

        userData.Remove("World");
        userData.Add("World", bf.Encrypt_CBC(world.ToString()));
        writer = new StreamWriter(path, false);
        writer.Write(userData.ToString());
        writer.Close();

        //TODO : Add save to file
    }

    private void OnApplicationPause(bool pause)
    {
        Debug.Log("Paused");
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(userData.ToString());
        writer.Close();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowGameObject(GameObject toggleObject)
    {
            toggleObject.SetActive(true);
    }

    public void HideGameObject(GameObject toggleObject)
    {
        toggleObject.SetActive(false);
    }

    public void ChangeColorBlindMode(Dropdown dropDown)
    {
        //PlayerPrefs.SetInt("Color blind mode", dropDown.value);

        userData.Remove("Color blind mode");
        userData.Add("Color blind mode", bf.Encrypt_CBC(dropDown.value.ToString()));
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(userData.ToString());
        writer.Close();

        colorBlindMode = dropDown.value;

        //TODO : Add save to file
    }

    public void ChangeExamples()
    {
        if (colorBlindMode == 0)
        {
            colorExample[0].GetComponent<Image>().color = new Color(1, 0, 0);
            colorExample[1].GetComponent<Image>().color = new Color(1, 0.5f, 0);
            colorExample[2].GetComponent<Image>().color = new Color(1, 1, 0);
            colorExample[3].GetComponent<Image>().color = new Color(0, 1, 0);
            colorExample[4].GetComponent<Image>().color = new Color(0, 0, 1);
            colorExample[5].GetComponent<Image>().color = new Color(0.7f, 0, 1);
        } else if(colorBlindMode == 1)
        {
            colorExample[0].GetComponent<Image>().color = new Color(0.56f, 0.49f, 0.12f);
            colorExample[1].GetComponent<Image>().color = new Color(0.72f, 0.64f, 0.08f);
            colorExample[2].GetComponent<Image>().color = new Color(1, 0.97f, 0.85f);
            colorExample[3].GetComponent<Image>().color = new Color(0.97f, 0.86f, 0);
            colorExample[4].GetComponent<Image>().color = new Color(0, 0.18f, 0.36f);
            colorExample[5].GetComponent<Image>().color = new Color(0, 0.43f, 0.89f);
        } else if (colorBlindMode == 2)
        {
            colorExample[0].GetComponent<Image>().color = new Color(0.63f, 0.47f, 0);
            colorExample[1].GetComponent<Image>().color = new Color(0.81f, 0.6f, 0);
            colorExample[2].GetComponent<Image>().color = new Color(1, 0.96f, 0.92f);
            colorExample[3].GetComponent<Image>().color = new Color(1, 084f, 0.6f);
            colorExample[4].GetComponent<Image>().color = new Color(0, 0.19f, 0.31f);
            colorExample[5].GetComponent<Image>().color = new Color(0, 0.45f, 0.77f);
        }
        else if (colorBlindMode == 3)
        {
            colorExample[0].GetComponent<Image>().color = new Color(0.99f, 0.09f, 0);
            colorExample[1].GetComponent<Image>().color = new Color(1, 0.47f, 0.5f);
            colorExample[2].GetComponent<Image>().color = new Color(1, 0.96f, 0.98f);
            colorExample[3].GetComponent<Image>().color = new Color(0.46f, 0.93f, 1);
            colorExample[4].GetComponent<Image>().color = new Color(0, 0.2f, 0.21f);
            colorExample[5].GetComponent<Image>().color = new Color(0.58f, 0.4f, 0.42f);
        }
    }

    public static void SelectElixir(GameObject elixir)
    {
        if (selectedElixir != null)
        {
            //unhighlight previous selected item
            selectedElixir.transform.GetChild(1).gameObject.SetActive(false);
            selectedElixir.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }

        selectedColor = elixir.GetComponent<InventoryItem>().colorName;
        selectedElixir = elixir;

        //highlight selected item in inventory
        selectedElixir.transform.GetChild(1).gameObject.SetActive(true);
        selectedElixir.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
    }

    public void ResetGame(bool forcedReset = false)
    {
        if (cardSelection.selectedCard != null)
            cardSelection.selectedCard.SetActive(true);
        // remove all elixir bottles from the inventory due they will create by user card choice
        inventoryManager.removeInventoryItems();

        // delete all the existing elixirs on the path
        foreach (GameObject elixir in pathController.liveElixirs)
            pathController.DestroyElixir(elixir);

        pathController.liveElixirs.Clear();
        pathController.liveElixirColors.Clear();


        // hide popups
        gameOverMsg.SetActive(false);
        endGameMsg.SetActive(false);
        winningMsg.SetActive(false);

        if (needToUpdateWinning)
        {
            Destroy(winningMsg.transform.GetChild(0).gameObject);
            needToUpdateWinning = false;
        }

        if (needToUpdateGameOver)
        {
            Destroy(gameOverMsg.transform.GetChild(0).gameObject);
            needToUpdateGameOver = false;
        }

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

    public void BackToRoom()
    {
        SceneManager.LoadScene(1);
        CrossScene.LoadTable = true;
    }

    // to perform refil after current level restarting
    public void RefillInvetnory()
    {
        inventoryManager.FillInventory(currentInput);
    }

    public void CleanCompletedLevelNotes()
    {
        // remove completed level notes from memory
        //PlayerPrefs.DeleteKey("Completed Levels");
        userData.Remove("Completed Levels");
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(userData.ToString());
        writer.Close();
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

            // Show level selection Cards
            cardSelection.CardGeneration();

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

    // this is temporary too
    void UpdateWorldDropdownMenuValues()
    {
        // this check prevents LoadWorld() function unnecessary call
        if (worldDropdown.value != world)
        {
            worldDropdown.value = world;
        }
    }

    public void win()
    {
        // move the instruction card under the winning message
        moveCardUnderTheMessage(winningMsg);
        SetUpdateWinning(true);
        winningMsg.SetActive(true);
    }

    public void lose ()
    {
        // move the instruction card under the gameover message
        moveCardUnderTheMessage(gameOverMsg);
        SetUpdateGameOver(true);
        gameOverMsg.SetActive(true);
    }

    public void moveCardUnderTheMessage(GameObject message)
    {
        GameObject selectedCard = Instantiate(cardSelection.selectedCard, message.transform);
        cardSelection.selectedCard.SetActive(false);
        selectedCard.transform.SetSiblingIndex(0);
        selectedCard.transform.localPosition = new Vector3(10, -10);
        selectedCard.GetComponent<CardAnimation>().enabled = false;
        selectedCard.transform.localScale = new Vector3(.8f, .8f, .8f);
    }
};
