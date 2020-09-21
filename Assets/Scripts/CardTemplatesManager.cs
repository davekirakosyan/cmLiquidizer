using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardTemplatesManager : MonoBehaviour
{
    public GameManager gameManager;

    // enum Template contains the names of all the templates with their indices
    public enum Template { 
        _2_1 = 0, 
        _2_2 = 1, 
        _3_1 = 2, 
        _3_2 = 3, 
        _3_3 = 4, 
        _4_1 = 5, 
        _4_2_A = 6, 
        _4_2_B = 7,
        _4_3 = 8, 
        _4_4 = 9, 
        _5_1 = 10,
        _5_2_A = 11,
        _5_2_b = 12,
        _5_3_A = 13, 
        _5_3_B = 14, 
        _5_4 = 15,
        _5_5 = 16,
        _6_6 = 17
    };
    
    // this contains all the template prefabs with the same indices as the enum
    public GameObject[] templateSprites;

    private GameObject getTemplateSprite(Template tmp)
    {
        // return the sprite with the corresponding index
        return templateSprites[(int)tmp];
    }

    // adding the corresponding temple to the card
    public void applyCorrectTemplate (GameObject card, int index)
    {
        GameObject instructionTemplate = Instantiate(getTemplateSprite(gameManager.currentPath.GetComponent<Path>().levels[index].instructionsTemplate), card.transform);
        instructionTemplate.transform.localPosition = new Vector3(0, 0, 0);
        
        GameObject inputs = instructionTemplate.transform.GetChild(0).gameObject;
        GameObject outputs = instructionTemplate.transform.GetChild(1).gameObject;

        // get correct textures for input elixirs
        for (int i = 0; i < inputs.transform.childCount; i++)
        {
            InventoryManager.ElixirColor col = gameManager.currentPath.GetComponent<Path>().levels[index].inputColors[i];
            inputs.transform.GetChild(i).GetComponent<RawImage>().texture = gameManager.inventoryManager.getTextureByColorName(col);
        }

        // get correct textures for output elixirs
        for (int i = 0; i < outputs.transform.childCount; i++)
        {
            InventoryManager.ElixirColor col = gameManager.currentPath.GetComponent<Path>().levels[index].outputColors[i];
            outputs.transform.GetChild(i).GetComponent<RawImage>().texture = gameManager.inventoryManager.getTextureByColorName(col);
        }
    }
}
