using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;
using BlowFishCS;
using System.IO;

public class ChangePath : MonoBehaviour
{

    public Transform[] paths;
    public Transform pathHolder;

    public JSONObject userData;
    BlowFish bf = new BlowFish("04B915BA43FEB5B6");

    string filePath = "Assets/Resources/Text/User data.txt";

    void Start()
    {
        StreamReader reader = new StreamReader(filePath);
        userData = JSONObject.Parse(reader.ReadToEnd());
        reader.Close();

        Transform path = Instantiate(paths[int.Parse(bf.Decrypt_CBC(userData.GetString("World")))], pathHolder);
        path.gameObject.AddComponent<BoxCollider>();
        path.gameObject.GetComponent<BoxCollider>().center = new Vector3(0.2393932f, -0.1519677f, 0.06649898f);
        path.gameObject.GetComponent<BoxCollider>().size = new Vector3(5.587857f, 4.204484f, 0.8670009f);
        path.gameObject.tag = "path";
        path.transform.GetChild(path.transform.childCount - 1).GetComponent<SpriteRenderer>().sortingOrder = 20;
    }

    
}
