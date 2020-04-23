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

    void Start()
    {
        // move elixir to the nearest point on path and calculate the distance from the startpoint of the path
        transform.position = pathCreator.path.GetClosestPointOnPath(transform.position);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);

        GetComponent<ParticleSystem>().startColor = color;
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
}
