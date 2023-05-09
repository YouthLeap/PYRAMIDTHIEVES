using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LevelObject
{
    public GameObject prefab;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;

    public static LevelObject Create(GameObject prefab, Transform data)
    {
        LevelObject obj = new LevelObject();
        obj.prefab = prefab;
        obj.position = data.position;
        obj.rotation = data.eulerAngles;
        obj.scale = data.localScale;
        return obj;
    }

    public void CreateInstance()
    {
        GameObject obj = GameObject.Instantiate(prefab) as GameObject;
        obj.transform.position = position;
        obj.transform.eulerAngles = rotation;
        obj.transform.localScale = scale;  
    }
}
