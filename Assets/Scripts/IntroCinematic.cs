using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;
using BlowFishCS;
using System.IO;

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

    public JSONObject userData;
    BlowFish bf = new BlowFish("04B915BA43FEB5B6");
    string path = "Assets/Resources/Text/User data.txt";

    private void Awake()
    {
        /*if (!PlayerPrefs.HasKey("Cinematic watched"))
             PlayerPrefs.SetInt("Cinematic watched", 0);*/

        StreamReader reader = new StreamReader(path);
        userData = JSONObject.Parse(reader.ReadToEnd());
        reader.Close();
    }

    void Start()
    {
        //uncomment rows below to unwatch cinematic
        //PlayerPrefs.SetInt("Cinematic watched", 0);

        int cinematicWatched = int.Parse(bf.Decrypt_CBC(userData.GetString("Cinematic watched")));
    
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
        //PlayerPrefs.SetInt("Cinematic watched", 1);
        userData.Remove("Cinematic watched");
        userData.Add("Cinematic watched", bf.Encrypt_CBC("1"));
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(userData.ToString());
        writer.Close();
    }
}
