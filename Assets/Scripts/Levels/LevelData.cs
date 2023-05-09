using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public int backgroundId;
    public string background;
    public List<LevelObjectData> objects = new List<LevelObjectData>();

    public string ToJsonString()
    {
        return JsonUtility.ToJson(this);
    }

    public static LevelData Create(string source)
    {
        return JsonUtility.FromJson<LevelData>(source);
    }
}
