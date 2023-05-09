using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BackgroundDialog : Dialog
{
    [SerializeField]
    private GameObject entryPrefab;
    [SerializeField]
    private Transform container;

    public string backgroundPath = "Background/Level1";

    void Start()
    {
        if (container.childCount == 0) Load(LevelEditor.level);
    }
    public void Load(int level = 1)
    {
        Sprite[] sprites = LevelEditor.instance.GetBackgrounds(level);
        string path = LevelEditor.instance.GetBackgroundPath(level);
        for (int i = 0; i < sprites.Length; i++)
        {
            GameObject obj = Instantiate(entryPrefab) as GameObject;
            obj.transform.SetParent(container);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.GetComponent<BackgroundEntry>().Load(sprites[i], path, i);
            if (i == 0) obj.GetComponent<BackgroundEntry>().Set();
        }
    }
    public void Reload(int level)
    {
        Debug.Log("Reload level " + level);
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        Load(level);
    }
}
