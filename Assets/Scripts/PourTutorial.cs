using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourTutorial : Tutorial
{

    public PathController pathController;
    public int elixirCount;

    void Start()
    {

    }

    public override void CheckIfHappening()
    {
        if (pathController.liveElixirs.Count == elixirCount)
            TutorialManager.Instace.completedTutorial();
    }
}
