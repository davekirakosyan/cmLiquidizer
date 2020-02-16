using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMovement : MonoBehaviour
{
    public Vector3[] movementPoints=new Vector3[4];
    private float startTime;
    private float[] journeyLength = new float[4];
    public float speed = 0.5F;
    public int state = 0;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        transform.position= movementPoints[0];
        journeyLength[0] = Vector3.Distance(movementPoints[0], movementPoints[1]);
        journeyLength[1] = Vector3.Distance(movementPoints[1], movementPoints[2]);
        journeyLength[2] = Vector3.Distance(movementPoints[2], movementPoints[3]);
        journeyLength[3] = Vector3.Distance(movementPoints[3], movementPoints[0]);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == 0)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength[0];
            transform.position = Vector3.Lerp(movementPoints[0], movementPoints[1], fractionOfJourney);
            if (transform.position == movementPoints[1])
            {
                startTime = Time.time;
                state++;
            }
        } else if (state == 1)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength[1];
            transform.position = Vector3.Lerp(movementPoints[1], movementPoints[2], fractionOfJourney);
            if (transform.position == movementPoints[2])
            {
                startTime = Time.time;
                state++;
            }
        }
        else if (state == 2)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength[2];
            transform.position = Vector3.Lerp(movementPoints[2], movementPoints[3], fractionOfJourney);
            if (transform.position == movementPoints[3])
            {
                startTime = Time.time;
                state++;
            }
        }
        else if (state == 3)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength[3];
            transform.position = Vector3.Lerp(movementPoints[3], movementPoints[0], fractionOfJourney);
            if (transform.position == movementPoints[0])
            {
                startTime = Time.time;
                state=0;
            }
        }
    }
}
