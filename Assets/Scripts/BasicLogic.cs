using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicLogic : MonoBehaviour
{
    // global variable of the selected elixir color 
    public static InventoryManager.ElixirColor selectedColor;
    public static bool gameOn = false;
    public static int level = 0;
    public PathController pathController;
    public GameObject gameOverMsg;
    public Dropdown levelDropdown;

    public GameObject[] PATHS;

    private void Awake()
    {
        CreateLevel();
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

    public void CreateLevel()
    {
        if (PATHS.Length != 0 && level < PATHS.Length && level >= 0)  // check if there is at least one level, and the current level has a path
        {
            // instantiate the level path
            GameObject newPath = Instantiate(PATHS[level], pathController.gameObject.transform);
            GameObject curve = newPath.transform.GetChild(0).gameObject;
            pathController.pathCreator = curve.GetComponent<PathCreation.PathCreator>();
            gameOn = true;
        }
    }

    public void ChangeLevel ()
    {
        // if there is any existing path remove it
        if (pathController.gameObject.transform.childCount != 0)
        {
            Destroy(pathController.gameObject.transform.GetChild(0).gameObject);
        }
        // change into a new level from the dropdown list
        level = levelDropdown.value;
        ResetGame();
        CreateLevel();
    }
}
