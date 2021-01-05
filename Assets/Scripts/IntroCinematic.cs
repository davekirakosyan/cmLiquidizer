using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public GameObject floor;
    public GameObject tutorial;

    private void Awake()
    {
        /*if (!PlayerPrefs.HasKey("Cinematic watched"))
             PlayerPrefs.SetInt("Cinematic watched", 0);*/

        JSON_API.ReadJSONFromMemory(); // Memory access is slow operation
    }

    void Start()
    {
        //uncomment rows below to unwatch cinematic
        //PlayerPrefs.SetInt("Cinematic watched", 0);

        int cinematicWatched = JSON_API.GetJSONData<int>("Cinematic watched");
    
        if (!SKIP_CINEMATIC && cinematicWatched == 0)
        {
            pastPlants.SetActive(true);
            // start camera + boat movements
            cameraAnimator.SetBool("start_intro", true);
            boatAnimator.SetBool("start_floating", true);
            swipeControls.SetActive(false);
            floor.SetActive(false);
        }
        else if (SKIP_CINEMATIC || cinematicWatched == 1)
        {
            // enable all the tree controls
            swipeControls.SetActive(true);
            floor.SetActive(true);
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
        floor.SetActive(true);
        tutorial.SetActive(true);
        GetComponent<Animator>().enabled = false;

        JSON_API.Remove("Cinematic watched");
        JSON_API.Add<int>("Cinematic watched", 1);

        JSON_API.UpdateJSONInMemory(); // Memory access is slow operation
    }
}
