using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;
using BlowFishCS;

public class Inventory : MonoBehaviour
{
    BlowFish bf = new BlowFish("04B915BA43FEB5B6");
    public static Inventory Instance { get; private set; }

    private JSONObject itemsData;
    public static int SelectedItemIndex { get; private set; }

    public class ShopItem {
        public string Bought, Selected;
        public string Price;
        public string Name;

        public ShopItem(string bought, string selected, string price, string name)
        {
            Bought = bought; Selected = selected; Price = price;Name = name.Replace("/n","");
        }
    }

    public static List<ShopItem> Items;
    public static string Coins { get; private set; }
    
    private void Awake()
    {
        Instance = this;

        //Load prefabs (if needed) from json file
        if (!PlayerPrefs.HasKey("Items"))
        {
            PlayerPrefs.SetString("Items", "{\"Items\":[{\"name\":\"0e7a66abd5daa98536ff177e27eedb5b\",\"bought\":\"0e7a66abd5daa985f47a5b16e3a8ddbe\",\"selected\":\"0e7a66abd5daa985f47a5b16e3a8ddbe\",\"price\":\"0e7a66abd5daa985636d838bdd125470\"},{\"name\":\"0e7a66abd5daa9857d3e2b1e96033c4a\",\"bought\":\"0e7a66abd5daa9850983cb872d5ede1d\",\"selected\":\"0e7a66abd5daa9850983cb872d5ede1d\",\"price\":\"0e7a66abd5daa985636d838bdd125470\"},{\"name\":\"0e7a66abd5daa98567391b0b57e5575d\",\"bought\":\"0e7a66abd5daa9850983cb872d5ede1d\",\"selected\":\"0e7a66abd5daa9850983cb872d5ede1d\",\"price\":\"0e7a66abd5daa985636d838bdd125470\"},{\"name\":\"0e7a66abd5daa9857a7b16d6cd7ae61f\",\"bought\":\"0e7a66abd5daa9850983cb872d5ede1d\",\"selected\":\"0e7a66abd5daa9850983cb872d5ede1d\",\"price\":\"0e7a66abd5daa9858702f017dbcf56d6\"},{\"name\":\"0e7a66abd5daa985a98333f7f8c47ad3\",\"bought\":\"0e7a66abd5daa9850983cb872d5ede1d\",\"selected\":\"0e7a66abd5daa9850983cb872d5ede1d\",\"price\":\"0e7a66abd5daa985433f717a73d0141e\"},{\"name\":\"0e7a66abd5daa985ade7e05c8e5baac1\",\"bought\":\"0e7a66abd5daa9850983cb872d5ede1d\",\"selected\":\"0e7a66abd5daa9850983cb872d5ede1d\",\"price\":\"0e7a66abd5daa98545b825fb94731cb2\"}]}");
            PlayerPrefs.SetString("Coins",bf.Encrypt_CBC("300"));    
        }

        //initialize data
        Coins = PlayerPrefs.GetString("Coins");
        itemsData = JSONObject.Parse(PlayerPrefs.GetString("Items"));
        Items = new List<ShopItem>();

        for (int i = 0; i < itemsData.GetArray("Items").Length; i++) {
            Items.Add(new ShopItem(itemsData.GetArray("Items")[i].Obj.GetString("bought"),
                                   itemsData.GetArray("Items")[i].Obj.GetString("selected"),
                                   itemsData.GetArray("Items")[i].Obj.GetString("price"),
                                   itemsData.GetArray("Items")[i].Obj.GetString("name")));
            if (bool.Parse(bf.Decrypt_CBC(Items[i].Selected)))
            {
                SelectedItemIndex = i;
            }
        }
    }

    //selecting items from shop
    public void SelectItem(int index) {
        for (int i=0;i< Items.Count; i++)
        {
            if (bool.Parse(bf.Decrypt_CBC(Items[i].Selected)))
            {
                Items[i].Selected = bf.Encrypt_CBC("false");
                itemsData.GetArray("Items")[i].Obj.GetValue("selected").Str = bf.Encrypt_CBC("false");
            }
        }
        Items[index].Selected = bf.Encrypt_CBC("true");
        itemsData.GetArray("Items")[index].Obj.GetValue("selected").Str = bf.Encrypt_CBC("true");

        SelectedItemIndex = index;

        PlayerPrefs.SetString("Items", itemsData.ToString());
        PlayerPrefs.Save();
    }

    //buying items from shop
    public void BuyItem(int index)
    {
        if (SubtractCoins(int.Parse(bf.Decrypt_CBC(Items[index].Price))))
        {
            Items[index].Bought = bf.Encrypt_CBC("true");
            itemsData.GetArray("Items")[index].Obj.GetValue("bought").Str = bf.Encrypt_CBC("true");
            SelectItem(index);
        }
    }

    //coin subtraction after buyinng item
    private bool SubtractCoins(int value)
    {
        if (int.Parse(bf.Decrypt_CBC(Coins)) - value < 0)
            return false;

        Coins = bf.Encrypt_CBC((int.Parse(bf.Decrypt_CBC(Coins))-value).ToString());
        PlayerPrefs.SetString("Coins", Coins);
        return true;
    }

}
