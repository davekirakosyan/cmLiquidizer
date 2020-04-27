using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLogic : MonoBehaviour
{
    // global variable of the selected elixir color 
    public static InventoryManager.ElixirColor selectedColor;
    public static bool gameOn = false;
    public static int level = 1;
    public PathController pathController;
    public GameObject gameOverMsg;

    public GameObject[] PATHS;

    private void Awake()
    {
        if (PATHS.Length != 0 && level <= PATHS.Length)  // check if there is at least one level, and the current level has a path
        {
            // instantiate the level path
            GameObject newPath = Instantiate(PATHS[level-1], pathController.gameObject.transform);
            GameObject curve = newPath.transform.GetChild(0).gameObject;
            pathController.pathCreator = curve.GetComponent<PathCreation.PathCreator>();
            gameOn = true;
        }
    }

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
