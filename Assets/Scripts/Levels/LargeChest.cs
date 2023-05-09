using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LargeChest : MonoBehaviour
{

    [SerializeField]
    private int level;
    [SerializeField]
    protected Image[] stars;
    [SerializeField]
    private Sprite disableStar;
    [SerializeField]
    private Text levelText;

    void Start()
    {
        levelText.text = string.Format("{0:00}", level % 30);
        if (level > Attributes.highest_level)
        {
            SetStars(0);
            GetComponent<Button>().interactable = false;
        }
        else
        {
            SetStars(Attributes.GetStars(level));
            //SetStars(3);
        }
    }
    public void SetStars(int star)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i >= star) stars[i].sprite = disableStar;
        }
    }

    public void Play()
    {
        if (Attributes.lives > 0 || Attributes.gems > 0) LevelManager.PlayLevel(level);
    }
}
