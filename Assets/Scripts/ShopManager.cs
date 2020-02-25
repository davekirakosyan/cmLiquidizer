﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] shopItems;

    public Text CoinsTxt;
    public Color SelectedColor, NonSelectedColor;

    private int prevSelectedIntex;

    private void Start()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItems[i].GetChild(0).GetComponent<Text>().text = Inventory.Items[i].Name;
            if (Inventory.Items[i].Bought)
            {
                shopItems[i].GetComponent<Image>().raycastTarget = false;
                shopItems[i].GetComponent<Image>().color = NonSelectedColor;
            }
        }

        UpdateUI(Inventory.SelectedItemIndex);
    }
    public void BuyOrSelectItem(int index)
    {
        if (Inventory.Items[index].Bought)
        {
            Inventory.Instance.SelectItem(index);
            UpdateUI(index);
        }
        else
        {
            Inventory.Instance.BuyItem(index);
            UpdateUI(index);
        }
    }

    private void UpdateUI(int index)
    {
        CoinsTxt.text = "Coins: " + Inventory.Coins.ToString();

        shopItems[prevSelectedIntex].GetComponent<Image>().color = NonSelectedColor;
        shopItems[prevSelectedIntex].GetComponent<Image>().raycastTarget = false;
        shopItems[Inventory.SelectedItemIndex].GetComponent<Image>().color = SelectedColor;

        prevSelectedIntex = Inventory.SelectedItemIndex;
    }
}
