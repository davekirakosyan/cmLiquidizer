using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardOnePosiitionGetter : MonoBehaviour
{
    public Transform cardHolder;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        this.transform.position = cardHolder.GetChild(0).position;
    }

    
}
