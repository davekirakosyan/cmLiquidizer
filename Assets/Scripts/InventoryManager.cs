﻿using System.Collections;
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
    public enum ElixirColors { Red, Orange, Yellow, Green, Blue, Purple };

    public GameObject inventoryItemPrefab;
    public Transform inventoryContent;

    void Start()
    {
        // This is temporary, should be called during level construction
        FillInventory(new ElixirColors[] { ElixirColors.Yellow, ElixirColors.Red, ElixirColors.Green });
    }

    void Update()
    {
        
    }

    // given a list of elixir colors, fills up the inventory
    void FillInventory (ElixirColors[] inputColorNamesList)
    {
        Vector2 lastPos = new Vector2(0, 0);
        if (inputColorNamesList.Length <= 3)
        {
            lastPos = new Vector2(GetComponent<RectTransform>().rect.width/2, -100);
        }

        for (int i = 0; i < inputColorNamesList.Length; i++)
        {
            GameObject newInventoryItem = Instantiate(inventoryItemPrefab);
            newInventoryItem.GetComponent<InventoryItem>().colorName = inputColorNamesList[i];
            newInventoryItem.transform.SetParent(inventoryContent);
            if (inputColorNamesList.Length <= 3)
            {
                newInventoryItem.transform.localPosition = lastPos;
                lastPos.y -= 120;
                newInventoryItem.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                newInventoryItem.transform.GetChild(0).GetComponent<RawImage>().texture = getTextureByColorName(inputColorNamesList[i]);
            }
        }
    }

    Texture getTextureByColorName (ElixirColors color)
    {
        switch (color)
        {
            case ElixirColors.Red:
                return red;
            case ElixirColors.Orange:
                return orange;
            case ElixirColors.Yellow:
                return yellow;
            case ElixirColors.Green:
                return green;
            case ElixirColors.Blue:
                return blue;
            case ElixirColors.Purple:
                return purple;
            default:
                return red;
        }
    }
}
