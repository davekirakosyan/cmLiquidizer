using System;
using System.IO;
using BlowFishCS;
using UnityEngine;
using Boomlagoon.JSON;
using System.Collections;
using System.Collections.Generic;

public static class JSON_API
{
    public static bool globalSecurity = true;
    public static string BlowFishKey = "04B915BA43FEB5B6";
    public static string path = "Assets/Resources/Text/User data.txt";
    
    private static JSONObject jsonData;
    private static BlowFish bf = new BlowFish(BlowFishKey);

    public static void ReadJSONFromMemory()
    {
        StreamReader reader = new StreamReader(path);
        jsonData = JSONObject.Parse(reader.ReadToEnd());
        reader.Close();
    }

    public static void UpdateJSONInMemory()
    {
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(jsonData.ToString());
        writer.Close();
    }

    private static genRType genParse<genRType>(string obj)
    {
        return (genRType)Convert.ChangeType(obj, typeof(genRType));
    }

    private static genRType genCast<genRType, genPType>(genPType data)
    {
        return (genRType)Convert.ChangeType(data, typeof(genRType));
    }

    public static genRType GetJSONData<genRType> (string fieldName)
    {
        return genParse<genRType>(globalSecurity ? bf.Decrypt_CBC(jsonData.GetString(fieldName)) : jsonData.GetString(fieldName));
    }

    public static bool Contains(string fieldName)
    {
        return jsonData.ContainsKey(fieldName);
    }

    public static void Add<genPType>(string fieldName, genPType value)
    {
        jsonData.Add(fieldName, globalSecurity ? bf.Encrypt_CBC(genCast<string, genPType>(value)) : jsonData.GetString(fieldName));
    }

    public static void Update<genPType>(string fieldName, genPType value)
    {
        jsonData.Remove(fieldName);
        Add<genPType>(fieldName, value);
    }

    public static void Remove(string fieldName)
    {
        jsonData.Remove(fieldName);
    }
}
