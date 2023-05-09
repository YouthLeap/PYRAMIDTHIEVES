using UnityEngine;
using System.Collections;

public class ItemDialog : Dialog
{

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Transform container;

    void Start()
    {
        Load();
    }
    void Load()
    {
        LevelItemData[] items = LevelEditor.instance.GetItems();
        foreach (LevelItemData item in items)
        {
            GameObject obj = Instantiate(prefab) as GameObject;
            obj.transform.SetParent(container);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.GetComponent<LevelObjectEntry>().Load(item);
        }
    }
}
