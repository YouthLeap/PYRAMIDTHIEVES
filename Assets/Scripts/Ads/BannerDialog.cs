using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BannerDialog : Dialog {
    [SerializeField]
    private Image image;

    public string url;

    public void Load(string url, Sprite sprite)
    {
        this.url = url;
        image.sprite = sprite;
        Open();
    }

    public void OpenUrl()
    {
        Application.OpenURL(url);
    }
}
