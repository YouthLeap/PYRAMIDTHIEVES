using UnityEngine;
using System.Collections;

public class TrapDialog : Dialog
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Transform container;

    // Use this for initialization
    void Start()
    {
        Load();
    }
    void Load()
    {
        LevelItemData[] traps = LevelEditor.instance.GetTraps();
        foreach (LevelItemData trap in traps)
        {
            GameObject obj = Instantiate(prefab) as GameObject;
            obj.transform.SetParent(container);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.GetComponent<TrapEntry>().Load(trap);
        }
    }
}
