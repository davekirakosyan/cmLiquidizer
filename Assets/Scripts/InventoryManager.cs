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
        //FillInventory(new ElixirColor[] { ElixirColor.Red, ElixirColor.Orange, ElixirColor.Yellow, ElixirColor.Green, ElixirColor.Blue, ElixirColor.Purple});
    }

    // given a list of elixir colors, fills up the inventory
    public void FillInventory (ElixirColor[] inputColorNamesList)
    {
        // first get rid of the old elixirs from the inventory (if there is any)
        removeInventoryItems();
        // adjust content size
        inventoryContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 50 + inputColorNamesList.Length * 120);

        // keep the position of the first item
        Vector2 nextPos = new Vector2(GetComponent<RectTransform>().rect.width/2, -70);

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
            newInventoryItem.AddComponent(System.Type.GetType("ItemDragHandler" + ",Assembly-CSharp"));
            nextPos.y -= 120;   // decrease y for the next item
        }
    }

    // removes the exicting inventory items
    public void removeInventoryItems()
    {
        if (inventoryContent.childCount != 0)
        {
            for (int i = 0; i < inventoryContent.childCount; i++)
            {
                Destroy(inventoryContent.GetChild(i).gameObject);
            }
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
