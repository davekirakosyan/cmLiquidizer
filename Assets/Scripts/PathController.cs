﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathCreation;

public class PathController : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject elixirPrefab;
    public static bool dragged = false;
    public PathCreator[] pathCreators;
    int nextUniqueNumber = 0;
    public GameObject gameOverMsg;
    public CardSelection cardSelection;
    public InventoryManager inventoryManager;
    public List<GameObject> liveElixirs = new List<GameObject>();
    public List<InventoryManager.ElixirColor> liveElixirColors = new List<InventoryManager.ElixirColor>();
    public bool checkCountdownInProgress = false;
    public Text countdownText;
    public Coroutine countDown;
    public TutorialManagerMainScreen tutorialManager;
    public AudioSource pourSound;

    void Update()
    {
        // detect the click
        if (Clicked() && GameManager.selectedElixir != null)
        {
            PlayerClicked();
            dragged = false;
        }
    }

    public void PlayerClicked()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 4.8f; // select distance = 4.8 units from the camera
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.transform.gameObject.layer == 8) // layer 8 is Path
            {
                // if clicked on tubes create an elixir at that hit point and initialize its main components
                CreateElixir(hit.point, gameManager.currentLevel.elixirSpeed, gameManager.currentLevel.elixirLength, GameManager.selectedColor);

                pourSound.Play();

                tutorialManager.clicked = true;
            }
            if (!checkCountdownInProgress)
            {
                countDown = StartCoroutine(CheckReseults());
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
        elixir.GetComponent<Elixir>().paths = pathCreators;
        elixir.GetComponent<Elixir>().pathController = this;
        // adding a unique number to each elixir for easy tracking and self collision issues
        elixir.GetComponent<Elixir>().uniqueNumber = nextUniqueNumber;
        nextUniqueNumber++;

        liveElixirs.Add(elixir);
        liveElixirColors.Add(colorName);
        if (liveElixirColors.Contains(GameManager.selectedColor))
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
        try                           // check if the device supports touch 
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended || dragged)
                return true;
            else
                return false;
        }
        catch                        // EDITOR
        {
            if (Input.GetMouseButtonDown(0) || dragged)
                return true;
            else
                return false;
        }
    }

    IEnumerator CheckReseults()
    {
        yield return new WaitForEndOfFrame();
        if (inventoryManager.IsInvenotoryEmpty())
        {
            // find the length of the longest path
            float longestPathLength = 0;
            for (int i = 0; i < pathCreators.Length; i++)
            {
                float pathLength = pathCreators[i].path.length;
                if (pathLength > longestPathLength)
                    longestPathLength = pathLength;
            }

            // wait until elixirs make one whole cycle
            checkCountdownInProgress = true;
            countdownText.text = "";
            countdownText.gameObject.SetActive(true);
            float waitTime = longestPathLength / gameManager.currentLevel.elixirSpeed;
            for (int i = 0; i < waitTime; i++)
            {
                yield return new WaitForSeconds(1);
                countdownText.text = (Mathf.Floor(waitTime-i)).ToString();
            }
            checkCountdownInProgress = false;
            countdownText.gameObject.SetActive(false);

            // check if the output is right
            bool isRequirementDone = true;
            foreach (InventoryManager.ElixirColor color in gameManager.currentOutput)
            {
                if (!liveElixirColors.Contains(color))
                {
                    isRequirementDone = false;
                }
            }


            /* 
             * Need logic to choose next "new" level by cards.
             *  if (CurrentLevelCompleted()) {
             *    1 Action  ->  show congratulation message
             *      Code    ->  LevelWinningAction()
             *                  {
             *                      showWinningMessage();
             *                  } 
             *    2 Action  ->  check if there is unplayed level
             *      Code    ->  if (LevelsArray.Length > 0) {
             *                    1 Action  ->  show unplayed level cards
             *                      Code    ->  LevelsArray = RemoveCompletedLevelFromLevelsArray(int LevelsArray[]);
             *                      Code    ->  int selectedLevel = ShowLevelCards(int LevelsArray[]);
             *                      Code    ->  SelectLevelByCards(int selectedLevel);
             *                  } else {
             *                      if (there is a next world "path") { 
             *                        1 Action  ->  switch to the next world "path"
             *                          Code    ->  LoadWorld(int worldNumber); 
             *                      } else {
             *                        2 Action  ->  end game
             *                          Code    ->  GameWinningAction();
             *                      }
             *                  }
             *  } else { // If player fails current level
             *      Action  ->  Need to discuss
             *                      Version 1: show uncompleted level cards but current card with some countdown timer and locked status.
             *                      Version 2: force player to replay current level
             *                      Version 3: suggest to complete current level with hints
             *                      Version 4: skip level by using "COINS"
             *      Code    ->  :)
             *  }
             */

            //old implemetation
            if (isRequirementDone && gameManager.world >= gameManager.PATHS.Length - 1 && gameManager.level >= gameManager.currentPath.GetComponent<Path>().levels.Length - 1)
                gameManager.endGameMsg.SetActive(true);
            else if (isRequirementDone)
                gameManager.win();
            else
                gameManager.lose();
        }
    }
}
