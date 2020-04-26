using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLogic : MonoBehaviour
{
    // global variable of the selected elixir color 
    public static InventoryManager.ElixirColor selectedColor;
    public static bool gameOn = true;
    public PathController pathController;
    public GameObject gameOverMsg;

    public static void SelectElixir(InventoryManager.ElixirColor color)
    {
        selectedColor = color;
    }

    public void ResetGame()
    {
        // remove all the existing ellixirs
        GameObject[] allElixirs = GameObject.FindGameObjectsWithTag("elixir");
        for (int i = 0; i < allElixirs.Length; i++)
        {
            pathController.DestroyElixir(allElixirs[i]);
        }
        gameOn = true;
        gameOverMsg.SetActive(false);
    }
}
