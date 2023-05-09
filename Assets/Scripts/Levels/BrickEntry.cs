using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BrickEntry : LevelObjectEntry
{
    public void CreateBrick()
    {
        GameObject brick = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        LevelObjectController controller = brick.AddComponent<LevelObjectController>();
        LevelObjectData data = new LevelObjectData();
        data.scale = new Vector3(1, 1, 1);
        controller.Load(data);
    }
}
