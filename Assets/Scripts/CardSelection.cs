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

    // counter for completed levels
    private int completedLevelsCount = 0;
    
    // minimum bitwin card space for card appearence animation
    public int spaceBetweenCards = 20;

    // game object for the game Manager
    public GameManager gameManager;

    public GameObject selectedCard;

    // game object for the tutorial Manager
    public TutorialManagerMainScreen tutorialManager;

    // Inventory manager object for filling inventory
    public InventoryManager inventoryManager;

    // current inventory set list
    public List<InventoryManager.ElixirColor> currentInventory;

    // Card Animation object for performing animations
    private CardAnimation cardAnimator;

    public CardTemplatesManager cardTemplatesManager;

    public GameObject blackCover;

    void Start()
    {
        // first boot-up value for level
        selectedLevel = PlayerPrefs.GetInt("Level");
        string completedLevels = PlayerPrefs.GetString("Completed Levels");
        if (completedLevels.Length > 0)
        {

            string[] tmpLevelArray = completedLevels.Split('x');

            foreach (string tmpLevelString in tmpLevelArray)
            {
                int tmpLevel;
                if (int.TryParse(tmpLevelString, out tmpLevel))
                    CompleteLevel(tmpLevel, true);
            }
        }

    }

    // getter function for user selected level
    public int GetLevel()
    {
        return selectedLevel;
    }

    // getter function for completed levels count
    public int GetCompletedLevelCount()
    {
        return completedLevelsCount;
    }

    // get level "name" from user selected card
    private void ClickHandler(int childIndex)
    {
        blackCover.SetActive(false);
        // parsing level name from selected card
        selectedCard = transform.GetChild(childIndex).gameObject;
        selectedLevel = int.Parse(selectedCard.transform.GetChild(0).GetComponent<Text>().text) - 1;

        // move selected card to prepared place
        selectedCard.GetComponent<CardAnimation>().AnimateSelectedCard();


        tutorialManager.clicked = true;

        // load selected level
        gameManager.ChangeLevel(selectedLevel);

        // refill inventory for selected level
        inventoryManager.FillInventory(currentInventory);

        // Disappear unselected cards
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i != childIndex)
            {
                GameObject unSelectedCard = transform.GetChild(i).gameObject;
                unSelectedCard.GetComponent<CardAnimation>().CardDisolve();
            }
        }
    }

    // show cards
    public void ShowLevelCards()
    {
        CardGeneration(false);
    }

    // calculate level cards positions for user selection
    private Vector3 CalculateCardPos(GameObject currentCard, int cardCount, int cardIndex)
    {
        Vector3 cardPos = currentCard.transform.localPosition;
        float cardWidth = currentCard.GetComponent<RectTransform>().rect.width;
        
        float boxWidth = (cardCount * cardWidth) + ((cardCount - 1) * spaceBetweenCards);
        if (cardCount > 1)
            cardPos.x = (cardIndex * (cardWidth + spaceBetweenCards)) - boxWidth / cardCount;
        else
            cardPos.x = (cardIndex * (cardWidth + spaceBetweenCards));

        cardPos.y = 0;

        return cardPos;
    }

    // card generation 
    public void CardGeneration(bool forceGeneration = true)
    {
        blackCover.SetActive(true);

        // getting available levels for selected path (world)
        int levelCount = gameManager.currentPath.GetComponent<Path>().levels.Length;

        // checking if there are old cards in Card Holder
        if (transform.childCount != 0)
        {
            if (forceGeneration)
            {
                // if there are cards and was selected forceGeneration flag destroy all existing cards
                completedLevelsCount = 0;
                for (int i = 0; i < transform.childCount; i++)
                    GameObject.Destroy(transform.GetChild(i).gameObject);
            }
            else
            {
                // if there are cards and wasn't selected forceGeneration flag reappear existing cards 
                for (int i = 0; i < transform.childCount; i++)
                {
                    GameObject existingCard = transform.GetChild(i).gameObject;
                    if (!existingCard.activeSelf)
                        existingCard.SetActive(true);

                    Vector3 tmp = CalculateCardPos(existingCard, levelCount, i);
                    cardAnimator = existingCard.GetComponent<CardAnimation>();
                    cardAnimator.AppearCard(tmp.x, tmp.y);
                }

                return;
            }
        }

        // creating new cards based on level count if needed
        for (int i = 0; i < levelCount; i++)
        {
            // local iterator for the current card
            int newLevel = i;
            
            // current card
            GameObject newCard = Instantiate(card, transform);
            cardTemplatesManager.applyCorrectTemplate(newCard, i);

            Vector3 tmp = CalculateCardPos(newCard, levelCount, i);
            cardAnimator = newCard.GetComponent<CardAnimation>();
            cardAnimator.AppearCard(tmp.x, tmp.y);
            newCard.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();

            if (forceGeneration)
                newCard.transform.GetChild(1).gameObject.SetActive(false);

            newCard.GetComponent<Button>().onClick.AddListener(() => ClickHandler(newLevel));
        }
    }

    // Show completion filter on the card and allow user to select new level
    public void CompleteLevel(int currentLevel, bool autoboot = false)
    {
        if (!autoboot)
        {
            string completedLevels = PlayerPrefs.GetString("Completed Levels");
            completedLevels += "x" + currentLevel.ToString();
            PlayerPrefs.SetString("Completed Levels", completedLevels);
        }

        completedLevelsCount++;
        GameObject currentCard = transform.GetChild(currentLevel).gameObject;
        GameObject cardCompletionFilter = currentCard.transform.GetChild(1).gameObject;
        if (!autoboot)
            currentCard.GetComponent<CardAnimation>().CardDisolve();

        cardCompletionFilter.SetActive(true);
        ShowLevelCards();
    }
}
