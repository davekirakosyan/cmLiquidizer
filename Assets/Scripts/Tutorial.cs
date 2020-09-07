using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
   
    public int order;

    [TextArea(3,10)]
    public string explanation;

    public Image indicatorImage;
    public GameObject highlightObject;
    public Image bubbleImage;
    public Image characterImage;
    public Image characterFacingBackImage;
    public GameObject highlight;
    public GameObject curtain;
    public bool left;
    public bool characterFacingBack;
    public Transform tutorialCanvas;
    public Vector2 position;
    public Vector2 rotation;
    public Vector2 bubbleSize;
    public Vector2 highlightSize;
    public Vector2 highlightPos;
    public Vector2 higlightRot;
    public Text dialogue;
    public GameObject targetObject;

    void Awake()
    {
        TutorialManager.Instace.Tutorials.Add(this);
    }
    
    public virtual void CheckIfHappening() { }
 
}
