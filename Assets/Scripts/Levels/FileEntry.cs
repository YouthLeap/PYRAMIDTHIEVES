using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class FileEntry : MonoBehaviour
{
    public string levelName;
    public string path;
    [SerializeField]
    private Text fileName;
    [SerializeField]
    private Color selectColor = Color.yellow;
    private Color defaultColor = Color.cyan;
    public void Load(string entry)
    {
        path = entry;
        //string[] s = entry.Split('/');
        //s = s[s.Length -1].Split('\\');
        ////s = s[s.Length - 1].Split('.');
        //levelName = s[s.Length - 1];
        levelName = Path.GetFileNameWithoutExtension(path);
        fileName.text = levelName;
    }

    public void Select()
    {
        SavePanel.instance.Select(this);
    }

    public void Select(bool isSelect)
    {
        fileName.color = (isSelect) ? selectColor : defaultColor;
    }

    public void LoadLevel()
    {
        LevelEditor.LoadLevel(path);
    }
}
