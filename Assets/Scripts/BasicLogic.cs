using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLogic : MonoBehaviour
{
    // global variable of the selected elixir color 
    public static InventoryManager.ElixirColor selectedColor;

    public static void SelectElixir(InventoryManager.ElixirColor color)
    {
        selectedColor = color;
    }
}
