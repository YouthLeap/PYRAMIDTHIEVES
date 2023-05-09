using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImportDialog : Dialog
{
    [SerializeField]
    private InputField input;

    public void Import()
    {
        LevelData data = LevelData.Create(input.text);
        LevelEditor.LoadLevel(data);
        Close();
    }
}
