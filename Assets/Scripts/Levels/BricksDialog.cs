using UnityEngine;
using System.Collections.Generic;
public class BricksDialog : Dialog {
    [SerializeField]
    private GameObject entryPrefab;
    [SerializeField]
    private Transform container;

    void Start()
    {
        if (container.childCount == 0) Load(LevelEditor.level);
    }

    void Load(int level = 1)
    {
        foreach (LevelItemData data in LevelEditor.instance.GetBricks(level))
        {
            GameObject obj = Instantiate(entryPrefab) as GameObject;
            obj.transform.SetParent(container);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.GetComponent<BrickEntry>().Load(data);
        }
    }

    public void Reload(int level)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        Load(level);
    }
}
