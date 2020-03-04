using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BlowFishCS;

public class ShopManagerWOSecurity : MonoBehaviour
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
            shopItems[i].GetChild(0).GetComponent<Text>().text = InventoryWithoutSecurity.Items[i].Name;
        }

        UpdateUI(InventoryWithoutSecurity.SelectedItemIndex);
    }
    public void BuyOrSelectItem(int index)
    {
        if (InventoryWithoutSecurity.Items[index].Bought)
        {
            InventoryWithoutSecurity.Instance.SelectItem(index);
            UpdateUI(index);
        }
        else
        {
            InventoryWithoutSecurity.Instance.BuyItem(index);
            UpdateUI(index);
        }
    }

    private void UpdateUI(int index)
    {
        CoinsTxt.text = "Coins: " + InventoryWithoutSecurity.Coins.ToString();

        shopItems[index].GetComponent<Image>().color = NonSelectedColor;
        shopItems[InventoryWithoutSecurity.SelectedItemIndex].GetComponent<Image>().color = SelectedColor;

        prevSelectedIntex = InventoryWithoutSecurity.SelectedItemIndex;
    }
}
