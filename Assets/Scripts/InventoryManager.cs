using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Texture red;
    public Texture orange;
    public Texture yellow;
    public Texture green;
    public Texture blue;
    public Texture purple;
    public enum ElixirColor { Red, Orange, Yellow, Green, Blue, Purple };

    public GameObject inventoryItemPrefab;
    public Transform inventoryContent;

    void Start()
    {
        // This is temporary, should be called during level construction
        FillInventory(new ElixirColor[] { ElixirColor.Yellow, ElixirColor.Red, ElixirColor.Green, ElixirColor.Purple });
    }

    void Update()
    {
        
    }

    // given a list of elixir colors, fills up the inventory
    void FillInventory (ElixirColor[] inputColorNamesList)
    {
        // keep the position of the first item
        Vector2 nextPos = new Vector2(GetComponent<RectTransform>().rect.width/2, -100);
        
        // go through all the color names and create corresponding color items in the inventory
        for (int i = 0; i < inputColorNamesList.Length; i++)
        {
            GameObject newInventoryItem = Instantiate(inventoryItemPrefab);
            newInventoryItem.GetComponent<InventoryItem>().colorName = inputColorNamesList[i];
            newInventoryItem.transform.SetParent(inventoryContent);
            newInventoryItem.transform.localPosition = nextPos;
            newInventoryItem.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            newInventoryItem.transform.GetChild(0).GetComponent<RawImage>().texture = getTextureByColorName(inputColorNamesList[i]);
            newInventoryItem.GetComponent<Button>().onClick.AddListener(() => BasicLogic.SelectElixir(newInventoryItem.GetComponent<InventoryItem>().colorName));
            nextPos.y -= 120;   // decrease y for the next item
        }
    }
    
    // return texture given the color
    public Texture getTextureByColorName (ElixirColor color)
    {
        switch (color)
        {
            case ElixirColor.Red:
                return red;
            case ElixirColor.Orange:
                return orange;
            case ElixirColor.Yellow:
                return yellow;
            case ElixirColor.Green:
                return green;
            case ElixirColor.Blue:
                return blue;
            case ElixirColor.Purple:
                return purple;
            default:
                return red;
        }
    }
}
