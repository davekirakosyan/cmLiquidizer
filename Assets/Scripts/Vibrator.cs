using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrator : MonoBehaviour
{
    float startVibration = 0;

    IEnumerator waitForVibrate(long duration)
    {
        Handheld.Vibrate();
        yield return new WaitUntil(() => (Time.time >= startVibration + duration));
    }

    public void Vibrate(long duration = 50)
    {
        startVibration = Time.time;
        StartCoroutine(waitForVibrate(duration));
    }
}
