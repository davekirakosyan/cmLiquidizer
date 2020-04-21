using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elixir : MonoBehaviour
{

    public Vector3[] movementCoordinates;
    private float[] journeyLength;
    public float startTime;
    public float speed;
    public int lastPos;
    private int nextPos;
    private int pathSize;

    void Start()
    {
        // first initializations
        startTime = Time.time;
        pathSize = movementCoordinates.Length;
        journeyLength = new float[pathSize];
        nextPos = lastPos + 1;

        //elixir setup
        transform.position = movementCoordinates[lastPos];

        // calculate lengths between consecutive points
        for (int i = 0; i < pathSize; i++)
        {
            if (lastPos >= movementCoordinates.Length)
                lastPos = 0;
            if (nextPos >= movementCoordinates.Length)
                nextPos = 0;
            journeyLength[lastPos] = Vector3.Distance(movementCoordinates[lastPos], movementCoordinates[nextPos]);
            lastPos++;
            nextPos++;
        }
    }

    void Update()
    {
        // elixir lerp movement from lastPos to nextBoz
        if (lastPos >= movementCoordinates.Length)
            lastPos = 0;
        if (nextPos >= movementCoordinates.Length)
            nextPos = 0;
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength[lastPos];
        transform.position = Vector3.Lerp(movementCoordinates[lastPos], movementCoordinates[nextPos], fractionOfJourney);
        if (transform.position == movementCoordinates[nextPos])
        {
            startTime = Time.time;
            lastPos++; nextPos++;
        }
    }
}
