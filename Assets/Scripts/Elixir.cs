using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Elixir : MonoBehaviour
{
    public PathCreator[] paths;
    public PathController pathController;
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    float distanceTravelled;
    public float speed;
    public float length;
    public InventoryManager.ElixirColor colorName = InventoryManager.ElixirColor.Red;
    Color color;
    public int uniqueNumber;
    bool canMix;

    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        // find the nearest path
        float minDistancesToThePath = float.MaxValue;
        for (int i = 0; i < paths.Length; i++)
        {
            float nextDistance = Vector3.Distance(paths[i].path.GetClosestPointOnPath(transform.position), transform.position);
            if (nextDistance < minDistancesToThePath)
            {
                minDistancesToThePath = nextDistance;
                pathCreator = paths[i];
            }
        }

        // move elixir to the nearest point on path and calculate the distance from the startpoint of the path
        transform.position = pathCreator.path.GetClosestPointOnPath(transform.position);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);

        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        color = SelectColorByName(colorName);
        part.startColor = color;
        part.startLifetime = length;
    }

    void Update()
    {
        // follow the path
        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }

    // collision logic
    void OnParticleCollision(GameObject other)
    {
        // prevent self collision using unique id
        if (uniqueNumber != other.GetComponentInParent<Elixir>().uniqueNumber)
        {
            InventoryManager.ElixirColor newColor = MixElixirs(other.transform.parent.GetComponent<Elixir>().colorName, colorName);
            if (canMix)
            {
                // create elixir on the position of collision
                pathController.CreateElixir(other.transform.position, speed, length, newColor);
            }
            else
            {
                pathController.gameManager.lose();

                // stop the existing countdown
                if (pathController.countDown != null)
                {
                    StopCoroutine(pathController.countDown);
                    pathController.checkCountdownInProgress = false;
                    pathController.countdownText.gameObject.SetActive(false);
                }
            }

            // add the elixir to the lists
            pathController.liveElixirs.Remove(this.gameObject);
            pathController.liveElixirs.Remove(other.transform.parent.gameObject);
            pathController.liveElixirColors.Remove(this.colorName);
            pathController.liveElixirColors.Remove(other.transform.parent.GetComponent<Elixir>().colorName);

            // destroy the existing elixirs
            pathController.DestroyElixir(this.gameObject);
            pathController.DestroyElixir(other.transform.parent.gameObject);
        }
    }

    // color mixing logic
    InventoryManager.ElixirColor MixElixirs(InventoryManager.ElixirColor color1, InventoryManager.ElixirColor color2)
    {
        InventoryManager.ElixirColor mix;
        canMix = true;

        if (color1.Equals(InventoryManager.ElixirColor.Red) && color2.Equals(InventoryManager.ElixirColor.Yellow) ||
            color2.Equals(InventoryManager.ElixirColor.Red) && color1.Equals(InventoryManager.ElixirColor.Yellow))
        {
            mix = InventoryManager.ElixirColor.Orange;
        }
        else if (color1.Equals(InventoryManager.ElixirColor.Red) && color2.Equals(InventoryManager.ElixirColor.Blue) ||
                 color2.Equals(InventoryManager.ElixirColor.Red) && color1.Equals(InventoryManager.ElixirColor.Blue))
        {
            mix = InventoryManager.ElixirColor.Purple;
        }
        else if (color1.Equals(InventoryManager.ElixirColor.Yellow) && color2.Equals(InventoryManager.ElixirColor.Blue) ||
                 color2.Equals(InventoryManager.ElixirColor.Yellow) && color1.Equals(InventoryManager.ElixirColor.Blue))
        {
            mix = InventoryManager.ElixirColor.Green;
        }
        else
        {
            mix = InventoryManager.ElixirColor.Red;
            canMix = false;
        }

        return mix;
    }

    // color mixing logic
    Color SelectColorByName(InventoryManager.ElixirColor name)
    {
        if (PlayerPrefs.GetInt("Color blind mode") == 0)
        {
            switch (name)
            {
                case InventoryManager.ElixirColor.Red:
                    return new Color(1, 0, 0);
                case InventoryManager.ElixirColor.Orange:
                    return new Color(1, 0.5f, 0);
                case InventoryManager.ElixirColor.Yellow:
                    return new Color(1, 1, 0);
                case InventoryManager.ElixirColor.Green:
                    return new Color(0, 1, 0);
                case InventoryManager.ElixirColor.Blue:
                    return new Color(0, 0, 1);
                case InventoryManager.ElixirColor.Purple:
                    return new Color(0.7f, 0, 1);
                default:
                    return new Color(1, 0, 0);
            }
        } else if (PlayerPrefs.GetInt("Color blind mode") == 1)
        {
            switch (name)
            {
                case InventoryManager.ElixirColor.Red:
                    return new Color(0.56f, 0.49f, 0.12f);
                case InventoryManager.ElixirColor.Orange:
                    return new Color(0.72f, 0.64f, 0.08f);
                case InventoryManager.ElixirColor.Yellow:
                    return new Color(1, 0.97f, 0.85f);
                case InventoryManager.ElixirColor.Green:
                    return new Color(0.97f, 0.86f, 0);
                case InventoryManager.ElixirColor.Blue:
                    return new Color(0, 0.18f, 0.36f);
                case InventoryManager.ElixirColor.Purple:
                    return new Color(0, 0.43f, 0.89f);
                default:
                    return new Color(1, 0, 0);
            }
        }
        else if (PlayerPrefs.GetInt("Color blind mode") == 2)
        {
            switch (name)
            {
                case InventoryManager.ElixirColor.Red:
                    return new Color(0.63f, 0.47f, 0);
                case InventoryManager.ElixirColor.Orange:
                    return new Color(0.81f, 0.6f, 0);
                case InventoryManager.ElixirColor.Yellow:
                    return new Color(1, 0.96f, 0.92f);
                case InventoryManager.ElixirColor.Green:
                    return new Color(1, 084f, 0.6f);
                case InventoryManager.ElixirColor.Blue:
                    return new Color(0, 0.19f, 0.31f);
                case InventoryManager.ElixirColor.Purple:
                    return new Color(0, 0.45f, 0.77f);
                default:
                    return new Color(1, 0, 0);
            }
        }
        else if (PlayerPrefs.GetInt("Color blind mode") == 3)
        {
            switch (name)
            {
                case InventoryManager.ElixirColor.Red:
                    return new Color(0.99f, 0.09f, 0);
                case InventoryManager.ElixirColor.Orange:
                    return new Color(1, 0.47f, 0.5f);
                case InventoryManager.ElixirColor.Yellow:
                    return new Color(1, 0.96f, 0.98f);
                case InventoryManager.ElixirColor.Green:
                    return new Color(0.46f, 0.93f, 1);
                case InventoryManager.ElixirColor.Blue:
                    return new Color(0, 0.2f, 0.21f);
                case InventoryManager.ElixirColor.Purple:
                    return new Color(0.58f, 0.4f, 0.42f);
                default:
                    return new Color(1, 0, 0);
            }
        }
        return new Color(1, 0, 0);
    }
}
