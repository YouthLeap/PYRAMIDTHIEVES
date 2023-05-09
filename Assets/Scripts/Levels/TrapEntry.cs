using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TrapEntry : LevelObjectEntry {
    
    private Vector2 size;
    //private Vector2 pivot;
    
    public void CreateTrap()
    {
        GameObject trap = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        trap.transform.localScale = new Vector3(1, 1, 1);
        LevelObjectController controller = trap.AddComponent<LevelObjectController>();
        LevelObjectData data = new LevelObjectData();
        data.path = "Traps/" + prefab.name;
        data.pos = Vector3.zero;
        data.scale = new Vector3(1, 1, 1);
        controller.Load(data);
        if (size.magnitude > 0.2f)
        {
            BoxCollider2D col = trap.AddComponent<BoxCollider2D>();
            col.size = size;
            //col.offset = pivot;
            col.isTrigger = true;
        }
    }
}
