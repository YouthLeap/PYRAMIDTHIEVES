using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Tab : MonoBehaviour {
    [SerializeField]
    private TabGroup group;
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private Image tabImage;
    [SerializeField]
    private Image tabIcon;
    [SerializeField]
    private Sprite enableSprite;
    [SerializeField]
    private Sprite disableSprite;
    //[SerializeField]
    //private Text title;

    public void Active()
    {
        Debug.Log("Active tab " + name);
        group.Active(this);
    }

    public void Enable()
    {
        content.SetActive(true);
        tabImage.enabled = true;
        //title.color = group.enableColor;
        tabIcon.sprite = enableSprite;
    }

    public void Disable()
    {
        content.SetActive(false);
        tabImage.enabled = false;
        //title.color = group.disableColor;
        tabIcon.sprite = disableSprite;
    }
}
