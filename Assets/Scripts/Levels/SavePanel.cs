using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class SavePanel : Dialog {
    public static SavePanel instance;
    [SerializeField]
    private Transform container;
    [SerializeField]
    private GameObject entryPrefab;
    [SerializeField]
    private Text fileName;
    [SerializeField]
    private Text status;

    public List<string> fileList = new List<string>();
    public FileEntry current;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        LoadFileList();
    }

    void LoadFileList()
    {
        string[] entries = LevelEditor.instance.GetFiles();

        foreach (string entry in entries)
        {
            if (!fileList.Contains(entry))
            {
                fileList.Add(entry);
                GameObject obj = Instantiate(entryPrefab) as GameObject;
                obj.GetComponent<FileEntry>().Load(entry);
                obj.transform.SetParent(container);
                obj.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void OnEnable()
    {
        LoadFileList();
    }
    
    public void Save()
    {
        LevelEditor.instance.Save(fileName.text);
        Close();
    }
    public void Play()
    {
        if (current == null)
        {
            ShowStatus("No File selected!!!");
            return;
        }
        LevelEditor.Play(current.path);
        Close();
    }
    public void Load()
    {
        if (current == null)
        {
            ShowStatus("No File selected!!!");
            return;
        }
        LevelEditor.LoadLevel(current.path);
        Close();
    }
    public void Select(FileEntry entry)
    {
        if (entry == current) return;
        if (current != null) current.Select(false);
        fileName.text = entry.levelName;
        entry.Select(true);
        current = entry;
    }

    public void SendMail()
    {
        if (current == null)
        {
            ShowStatus("No File selected!!!");
            return;
        }
        LevelEditor.SendMail(current.path);
    }

    public void Delete()
    {
        if (current == null)
        {
            ShowStatus("No File selected!!!");
            return;
        }
        LevelEditor.Remove(current.path);
        Destroy(current.gameObject);
    }

    private void ShowStatus (string content)
    {
        status.text = content;
    }
}
