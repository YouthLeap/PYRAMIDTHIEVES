using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BackgroundEntry : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private int id;
    [SerializeField]
    private string path;

    public void Set()
    {
        LevelEditor.instance.background.sprite = image.sprite;
        LevelEditor.instance.backgroundId = id;
        LevelEditor.instance.backgroundPath = path;
    }

    public void Load(Sprite sprite, string path, int id)
    {
        image.sprite = sprite;
        this.id = id;
        this.path = path + sprite.name;
    }
}
