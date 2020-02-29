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
        public bool Bought, Selected;
        public int Price;
        public string Name;

        public ShopItem(bool bought, bool selected, int price, string name)
        {
            Bought = bought; Selected = selected; Price = price;Name = name.Replace("/n","");
        }
    }

    public static List<ShopItem> Items;
    public static int Coins { get; private set; }
    
    private void Awake()
    {
        Instance = this;

        //if (!PlayerPrefs.HasKey("Items")) {
            PlayerPrefs.SetString("Items", "{\"Items\":[{\"name\":\"22dd4ba56c29eaa871e2533dabbd57bec1ccc05372694ca8\",\"bought\":\"22dd4ba56c29eaa8952945b465dbbeb5\",\"selected\":\"22dd4ba56c29eaa80d26f3c47703c712\",\"price\":\"22dd4ba56c29eaa8d6178b5743fb96f3\"},{\"name\":\"22dd4ba56c29eaa84ba4f2bba938b313e11346ca624de8a4\",\"bought\":\"22dd4ba56c29eaa80d26f3c47703c712\",\"selected\":\"22dd4ba56c29eaa80d26f3c47703c712\",\"price\":\"22dd4ba56c29eaa805d3523dd8d2a989\"},{\"name\":\"22dd4ba56c29eaa83773ecf707ab61587317cf29eff62d20\",\"bought\":\"22dd4ba56c29eaa80d26f3c47703c712\",\"selected\":\"22dd4ba56c29eaa80d26f3c47703c712\",\"price\":\"22dd4ba56c29eaa800fc87faa9e734a9\"}]}");
            PlayerPrefs.SetInt("Coins",1000);    
       // }

        Coins = PlayerPrefs.GetInt("Coins");
        itemsData = JSONObject.Parse(PlayerPrefs.GetString("Items"));
        Items = new List<ShopItem>();

        for (int i = 0; i < itemsData.GetArray("Items").Length; i++) {
            Items.Add(new ShopItem(bool.Parse(bf.Decrypt_CBC(itemsData.GetArray("Items")[i].Obj.GetString("bought"))),
                                   bool.Parse(bf.Decrypt_CBC(itemsData.GetArray("Items")[i].Obj.GetString("selected"))),
                                   int.Parse(bf.Decrypt_CBC(itemsData.GetArray("Items")[i].Obj.GetString("price"))),
                                   bf.Decrypt_CBC(itemsData.GetArray("Items")[i].Obj.GetString("name"))));
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
