using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LCDialog : Dialog
{
    [SerializeField]
    private Selectable[] stars; 
    [SerializeField]
    private Text title;

    void Start()
    {
        title.text = string.Format("Level {0} completed!", LevelManager.LevelToString(LevelManager.level));
    }

    public void SetStars(int star)
    {
        for (int i = 0; i < 3; i++)
        {
            stars[i].interactable = (i < star);
        }
    }
}
