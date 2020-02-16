using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;

public class Inventory : MonoBehaviour
{
  public static Inventory Instance { get; private set; }

    private JSONObject itemsData;
    public static int SelectedItemIndex { get; private set; }

    public class ShopItem {
        public bool Bought, Selected;
        public int Price;
        public string Name;

        public ShopItem(bool bought, bool selected, int price, string name)
        {
            Bought = bought; Selected = selected; Price = price;Name = name;
        }
    }

    public static List<ShopItem> Items;
    public static int Coins { get; private set; }
    
    private void Awake()
    {
        Instance = this;

        if (!PlayerPrefs.HasKey("Items")) {
            PlayerPrefs.SetString("Items", "{\"Items\":[{\"name\":\"red-\n elixire\",\"bought\":true,\"selected\":true,\"price\":100},{\"name\":\" yellow- \nelixire\",\"bought\":false,\"selected\":false,\"price\":100},{\"name\":\"orange- \nelixire \",\"bought\":false,\"selected\":false,\"price\":200}]}");
            PlayerPrefs.SetInt("Coins",1000);    
        }

        Coins = PlayerPrefs.GetInt("Coins");
        itemsData = JSONObject.Parse(PlayerPrefs.GetString("Items"));
        Items = new List<ShopItem>();

        for (int i = 0; i < itemsData.GetArray("Items").Length; i++) {
            Items.Add(new ShopItem(itemsData.GetArray("Items")[i].Obj.GetBoolean("bought"),
                                   itemsData.GetArray("Items")[i].Obj.GetBoolean("selected"),
                                   (int)itemsData.GetArray("Items")[i].Obj.GetNumber("price"),
                                   itemsData.GetArray("Items")[i].Obj.GetString("name")));
            if (Items[i].Selected)
                SelectedItemIndex = i;
        }
    }

    public void SelectItem(int index) {
        for (int i=0;i< Items.Count; i++)
        {
            if (Items[i].Selected)
            {
                Items[i].Selected = false;
                itemsData.GetArray("Items")[i].Obj.GetValue("selected").Boolean = false;
            }
        }
        Items[index].Selected = true;
        itemsData.GetArray("Items")[index].Obj.GetValue("selected").Boolean = true;

        SelectedItemIndex = index;

        PlayerPrefs.SetString("Items", itemsData.ToString());
        PlayerPrefs.Save();
    }

    public void BuyItem(int index)
    {
        if (SubtractCoins(Items[index].Price))
        {
            Items[index].Bought = true;
            itemsData.GetArray("Items")[index].Obj.GetValue("bought").Boolean = true;
            SelectItem(index);
        }
    }

    private bool SubtractCoins(int value)
    {
        if (Coins - value < 0)
            return false;

        Coins -= value;
        PlayerPrefs.SetInt("Coins", Coins);
        return true;
    }

}
