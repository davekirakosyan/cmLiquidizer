using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyAnimator : MonoBehaviour
{

    public float[] values;
    private int currFrame;
    private float timer;
    private float frameRate = 0.1f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer -= frameRate;
            currFrame = (currFrame + 1) % values.Length;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, values[currFrame]);
        }

    }
}
