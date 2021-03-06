﻿using System;
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

    private int gapBetweenElixirs = 165;
    public GameObject usedElixir;

    Vector2 firstItemPosition = new Vector2(0, -100);

    // given a list of elixir colors, fills up the inventory
    public void FillInventory (List<ElixirColor> inputColorNamesList)
    {
        // first get rid of the old elixirs from the inventory (if there is any)
        removeInventoryItems();
        // adjust content bubbleSize
        inventoryContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 50 + inputColorNamesList.Count * gapBetweenElixirs);

        // keep the position of the first item
        Vector2 nextPos = firstItemPosition;
        nextPos.x = inventoryContent.GetComponent<RectTransform>().rect.width/2;

        // go through all the color names and create corresponding color items in the inventory
        foreach (ElixirColor elixirColor in inputColorNamesList)
        {
            GameObject newInventoryItem = Instantiate(inventoryItemPrefab);
            newInventoryItem.GetComponent<InventoryItem>().colorName = elixirColor;
            newInventoryItem.transform.SetParent(inventoryContent);
            newInventoryItem.transform.localPosition = nextPos;
            newInventoryItem.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            newInventoryItem.transform.GetChild(0).GetComponent<RawImage>().texture = getTextureByColorName(elixirColor);
            newInventoryItem.GetComponent<Button>().onClick.AddListener(() => GameManager.SelectElixir(newInventoryItem));
            nextPos.y -= gapBetweenElixirs;   // decrease y for the next item
        }
    }

    public bool IsInvenotoryEmpty()
    {
        if (inventoryContent.childCount == 0)
            return true;
        return false;
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

    IEnumerator RearrangeInventoryContent ()
    {

        // keep the position of the first item
        Vector2 nextPos = firstItemPosition;
        nextPos.x = inventoryContent.GetComponent<RectTransform>().rect.width / 2;
        yield return new WaitForEndOfFrame();

        // go through all the items and fix position
        for (int i = 0; i < inventoryContent.childCount; i++)
        {
            inventoryContent.GetChild(i).transform.localPosition = nextPos;
            nextPos.y -= gapBetweenElixirs;   // decrease y for the next item
        }
    }

    // delete the used elixir from inventory and fill its gap
    public void RemoveUsedItemFromInventory()
    {
        Destroy(GameManager.selectedElixir);
        StartCoroutine( RearrangeInventoryContent());
    }
}
