using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelection : MonoBehaviour
{
    // user selected level
    private int selectedLevel;

    // game object for the card prefab
    public GameObject card;

    // game object for the game Manager
    public GameManager gameManager;

    // Inventory manager object for filling inventory
    public InventoryManager inventoryManager;

    // current inventory set list
    public List<InventoryManager.ElixirColor> currentInventory;

    void Start()
    {
        // first boot-up value for level
        selectedLevel = PlayerPrefs.GetInt("Level");
    }

    // getter function for user selected level
    public int Get_Level()
    {
        return selectedLevel;
    }

    // get level "name" from user selected card
    private void ClickHandler(int childIndex)
    {
        GameObject selectedCard = transform.GetChild(childIndex).gameObject;
        selectedLevel = int.Parse(selectedCard.transform.GetChild(0).GetComponent<Text>().text);

        // load selected level
        gameManager.ChangeLevel(selectedLevel);

        // refill inventory for selected level
        inventoryManager.FillInventory(currentInventory);
    }

    // show cards
    public void ShowLevelCards()
    {
        CardGeneration(false);
    }

    // card generation 
    public void CardGeneration(bool forceGeneration = true)
    {
        // getting available levels for selected path (world)
        int levelCount = gameManager.currentPath.GetComponent<Path>().levels.Length;

        // checking if there are old cards in Card Holder and destroy them
        if (transform.childCount != 0)
        {
            for (int i = 0; i < transform.childCount; i++)
                GameObject.Destroy(transform.GetChild(i).gameObject);
        }

        // creating new cards based on level count
        for (int i = 0; i < levelCount; i++)
        {
            // local iterator for the current card
            int newLevel = i;
            
            // current card
            GameObject newCard = Instantiate(card, transform);
            newCard.transform.position = new Vector3(newCard.transform.position.x, newCard.transform.position.y - (150 * i), newCard.transform.position.z);
            newCard.transform.GetChild(0).GetComponent<Text>().text = i.ToString();

            if (forceGeneration)
                newCard.transform.GetChild(1).gameObject.SetActive(false);

            newCard.GetComponent<Button>().onClick.AddListener(() => ClickHandler(newLevel));
        }
    }

    // Show completion filter on the card
    public void CompleteLevel(int currentLevel)
    {
        GameObject currentCard = transform.GetChild(currentLevel).gameObject;
        GameObject cardCompletionFilter = currentCard.transform.GetChild(1).gameObject;
        cardCompletionFilter.SetActive(true);
    }
}
