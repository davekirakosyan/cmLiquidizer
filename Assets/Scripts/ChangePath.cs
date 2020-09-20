using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePath : MonoBehaviour
{

    public Transform[] paths;
    public Transform table;

    void Start()
    {
        Transform path = Instantiate(paths[PlayerPrefs.GetInt("World")], table);
        path.transform.localPosition = new Vector3(-2.5f, -0.35f, -2.89f);
        path.transform.eulerAngles = new Vector3(60.312f, 0, 0);
        path.transform.localScale = new Vector3(0.77864f, 0.77864f, 0.77864f);
        path.gameObject.AddComponent<BoxCollider>();
        path.gameObject.GetComponent<BoxCollider>().center = new Vector3(0.2393932f, -0.1519677f, 0.06649898f);
        path.gameObject.GetComponent<BoxCollider>().size = new Vector3(5.587857f, 4.204484f, 0.8670009f);
        path.gameObject.tag = "path";
    }

    
}
