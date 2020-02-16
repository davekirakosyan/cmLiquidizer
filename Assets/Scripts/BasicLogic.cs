using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLogic : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.Contains("White"))
        {
           // Debug.Log(collision.collider.name);
            Time.timeScale = 0;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
