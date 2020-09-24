using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePath : MonoBehaviour
{

    public Transform[] paths;
    public Transform pathHolder;

    void Start()
    {
        Transform path = Instantiate(paths[PlayerPrefs.GetInt("World")], pathHolder);
        path.gameObject.AddComponent<BoxCollider>();
        path.gameObject.GetComponent<BoxCollider>().center = new Vector3(0.2393932f, -0.1519677f, 0.06649898f);
        path.gameObject.GetComponent<BoxCollider>().size = new Vector3(5.587857f, 4.204484f, 0.8670009f);
        path.gameObject.tag = "path";
        path.transform.GetChild(path.transform.childCount - 1).GetComponent<SpriteRenderer>().sortingOrder = 20;
    }

    
}
