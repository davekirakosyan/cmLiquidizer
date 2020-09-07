using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTutorial : Tutorial
{

    bool waited = false;
    public float waitTime;

    void Start()
    {
        
        
    }

    IEnumerator WaitCoroutine()
    {
        
        yield return new WaitForSeconds(waitTime);

        waited = true;
    }

    public override void CheckIfHappening()
    {
        StartCoroutine(WaitCoroutine());
        if (waited)
        {
            TutorialManager.Instace.completedTutorial();
        }
    }
}
