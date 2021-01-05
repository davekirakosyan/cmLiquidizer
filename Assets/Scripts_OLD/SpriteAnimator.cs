using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField]
    private Sprite[] frames;
    private int currFrame;
    private float timer;
    private float frameRate = 0.15f;
    private SpriteRenderer spriteRenderer;
    public bool loop = true;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (currFrame >= frames.Length-1 && !loop)
            Destroy(this);

        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer -= frameRate;
            currFrame = (currFrame + 1) % frames.Length;
            spriteRenderer.sprite = frames[currFrame];
        }
        
    }
}
