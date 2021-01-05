using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BlowFishCS;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] shopItems;
    [SerializeField]
    private Sprite[] elixires;

    BlowFish bf = new BlowFish("04B915BA43FEB5B6");

    public Text CoinsTxt;
    public Color SelectedColor, NonSelectedColor;

    private int prevSelectedIntex;

    private void Start()
    {
        //initialize shop ui
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItems[i].GetChild(0).GetComponent<Text>().text = bf.Decrypt_CBC(Inventory.Items[i].Price);
            shopItems[i].GetChild(1).GetComponent<Image>().sprite = elixires[i];
            if (bool.Parse(bf.Decrypt_CBC(Inventory.Items[i].Bought)))
            {
                shopItems[i].GetComponent<Image>().raycastTarget = false;
                shopItems[i].GetComponent<Image>().color = NonSelectedColor;
            }
        }

        UpdateUI(Inventory.SelectedItemIndex);
    }

    //update UI after buying or selecting item
    public void BuyOrSelectItem(int index)
    {
        if (bool.Parse(bf.Decrypt_CBC(Inventory.Items[index].Bought)))
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
        CoinsTxt.text = "Coins: " + bf.Decrypt_CBC(Inventory.Coins);

        shopItems[prevSelectedIntex].GetComponent<Image>().color = NonSelectedColor;
        shopItems[prevSelectedIntex].GetComponent<Image>().raycastTarget = false;
        shopItems[prevSelectedIntex].GetComponent<Button>().enabled = false;
        shopItems[Inventory.SelectedItemIndex].GetComponent<Image>().color = SelectedColor;

        prevSelectedIntex = Inventory.SelectedItemIndex;
    }
}
