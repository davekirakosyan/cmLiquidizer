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
    public Color color = Color.red;
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
}
