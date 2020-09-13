using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseTutorial : Tutorial
{

    public GameObject winMsg;
    public GameObject loseMsg;
    
    public override void CheckIfHappening()
    {
        if (winMsg || loseMsg)
        {
            TutorialManager.Instace.completedTutorial();
        }
    }
}
