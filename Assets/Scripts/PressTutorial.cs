﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressTutorial : Tutorial
{

    public GameObject targetObject;    
    private Collider coll;
    

    private void Start()
    {
        coll = targetObject.GetComponent<Collider>();
    }

    public override void CheckIfHappening()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            

            if (coll.Raycast(ray, out hit,10000f))
            {
                TutorialManager.Instace.completedTutorial();
            }
        }
    }
}