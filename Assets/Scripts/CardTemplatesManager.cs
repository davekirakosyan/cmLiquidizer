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
        _5_4 = 13, 
        _5_3_A = 14, 
        _5_3_B = 15, 
        _5_5 = 16,
        _6_6 = 17
    };

    // this contains all the template sprites with the same indices as the enum
    public Sprite[] templateSprites;

    private Sprite getTemplateSprite(Template tmp)
    {
        // return the sprite with the corresponding index
        return templateSprites[(int)tmp];
    }

    public void applyCorrectTemplate (GameObject card, int index)
    {
        // find the right image in the card prefab
        Transform schemeTransform = card.transform.Find("instruction scheme");
        if (schemeTransform != null)
        {
            // update the sprite of the image with the corresponding instruction template
            GameObject scheme = schemeTransform.gameObject;
            scheme.GetComponent<Image>().sprite = getTemplateSprite(gameManager.currentPath.GetComponent<Path>().levels[index].instructionsTemplate);
        }
    }

}
