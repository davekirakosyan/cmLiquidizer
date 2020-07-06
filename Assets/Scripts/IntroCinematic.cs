using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCinematic : MonoBehaviour
{
    public Animator cameraAnimator;
    public Animator boatAnimator;
    public GameObject pastPlants;
    public SpriteMask sm;   // masking test

    void Start()
    {
        pastPlants.SetActive(true);
        // start camera + boat movements
        cameraAnimator.SetBool("start_intro", true);
        boatAnimator.SetBool("start_floating", true);
    }

    void DeletePastPlants()
    {
        GameObject.Destroy(pastPlants);
    }

    private void Update()
    {
        sm.alphaCutoff -= 0.001f;
    }
}
