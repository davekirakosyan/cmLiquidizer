using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Elixir : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction; 
    float distanceTravelled;
    public float speed;
    public InventoryManager.ElixirColor colorName = InventoryManager.ElixirColor.Red;
    Color color = Color.red;
    public int uniqueNumber;

    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        // move elixir to the nearest point on path and calculate the distance from the startpoint of the path
        transform.position = pathCreator.path.GetClosestPointOnPath(transform.position);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);

        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        color = SelectColorByName(colorName);
        part.startColor = color;
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

    void OnParticleCollision(GameObject other)
    {
        // prevent self collision using unique id
        if (uniqueNumber != other.GetComponentInParent<Elixir>().uniqueNumber)
            print(other.name);
    }

    Color SelectColorByName (InventoryManager.ElixirColor name)
    {
        switch (name)
        {
            case InventoryManager.ElixirColor.Red:
                return Color.red;
            case InventoryManager.ElixirColor.Orange:
                return new Color(1, 0.5f, 0);
            case InventoryManager.ElixirColor.Yellow:
                return Color.yellow;
            case InventoryManager.ElixirColor.Green:
                return Color.green;
            case InventoryManager.ElixirColor.Blue:
                return Color.blue;
            case InventoryManager.ElixirColor.Purple:
                return new Color(0.7f, 0, 1);
            default:
                return Color.red;
        }
    }
}
