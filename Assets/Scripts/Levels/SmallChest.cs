using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SmallChest : MonoBehaviour
{
    [SerializeField]
    private int level;
    [SerializeField]
    private Image[] stars;
    [SerializeField]
    private Sprite disableChest;

    void Start()
    {
        if (level > Attributes.highest_level)
        {
            SetStars(0);
            GetComponent<Image>().sprite = disableChest;
        }
        else
        {
            //SetStars(PlayerPrefs.GetInt(ConstantKey.STAR_AT_LEVEL_KEY + level.ToString()));
            SetStars(Attributes.GetStars(level));
        }
    }
    public void SetStars(int star)
    {
        for (int i = 0; i < 3; i++)
        {
            stars[i].enabled = (i < star);
        }
    }

}
