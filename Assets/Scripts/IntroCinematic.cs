using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCinematic : MonoBehaviour
{
    public bool SKIP_CINEMATIC = false;

    public Animator cameraAnimator;
    public Animator boatAnimator;
    public Animator characterAnimator;
    public GameObject pastPlants;
    public SpriteMask sm;   // masking test
    public GameObject deadPlant;
    public GameObject pouringParticleEmitters;
    public GameObject swipeControls;
    public GameObject treeNavigation;

    void Start()
    {
        if (!SKIP_CINEMATIC)
        {
            pastPlants.SetActive(true);
            // start camera + boat movements
            cameraAnimator.SetBool("start_intro", true);
            boatAnimator.SetBool("start_floating", true);
            swipeControls.SetActive(false);
        }
        else
        {
            swipeControls.SetActive(true);
            treeNavigation.SetActive(true);
            GetComponent<Animator>().enabled = false;
        }
    }
    private void Update()
    {
        sm.alphaCutoff -= 0.001f;
    }

    void DeletePastPlants()
    {
        GameObject.Destroy(pastPlants);
    }

    void StartDeadPlantWalk ()  
    {
        cameraAnimator.SetBool("start_intro", false);
        cameraAnimator.SetBool("walk_to_plant", true);
        characterAnimator.SetBool("walk_to_plant", true);
    }

    void PourElixirOnPlant ()
    {
        cameraAnimator.SetBool("pour_elixir_on_plant", true);
        characterAnimator.SetBool("pour_elixir_on_plant", true);
    }

    void ElixirPouringVFX ()
    {
        for (int i = 0; i < pouringParticleEmitters.transform.childCount; i++)
        {
            pouringParticleEmitters.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
        }
    }

    void StartPlantTransformation ()
    {
        deadPlant.GetComponent<SpriteAnimator>().enabled = true;
    }

    void RaccoonGoToYourRoom ()
    {
        characterAnimator.SetBool("walk_to_tree", true);
    }

    void EndCinematicPart1 ()
    {
        swipeControls.SetActive(true);
        GetComponent<Animator>().enabled = false;
    }
}
