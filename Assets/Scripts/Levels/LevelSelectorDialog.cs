using UnityEngine;
using System.Collections;

public class LevelSelectorDialog : Dialog
{
    [SerializeField]
    BackgroundDialog backgroundDialog;
    [SerializeField]
    private BricksDialog brickDialog;
    public void SelectLevel(int level)
    {
        if (LevelEditor.level == level) return;
        LevelEditor.level = level;
        Debug.Log("Select level " + level);
        brickDialog.Reload(level);
        backgroundDialog.Reload(level);
    }
}
