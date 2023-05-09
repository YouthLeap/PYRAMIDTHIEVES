using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{
    [SerializeField]
    private ObjectList objectList;
    [SerializeField]
    private LevelObjectList[] levelObjects;
    [SerializeField]
    private TrapList items;
    [SerializeField]
    private GameObject trash;
    public ObjectControlPanel objectControl;

    public GameObject targetPrefab;
    public static LevelEditor instance;
    public Dialog currentDialog;

    [SerializeField]
    private BackgroundDialog backgroundDialog;
    [SerializeField]
    private BricksDialog brickDialog;
    [SerializeField]
    private TrapDialog trapDialog;
    [SerializeField]
    private Alert alert;

    public int backgroundId;
    public string backgroundPath = "Background/Level1/lv1";

    public SpriteRenderer background;
    public LevelObjectController selectedObject;
    public ScaleBox scaleBox;
    
    private static string tempData;
    public static int level = 1;

    void Awake()
    {
        instance = this;
        ObjectControlPanel.instance = objectControl;
        InitOnSceneItems();
    }

    void Start()
    {
        if (tempData != null)
        {
            LevelData data = LevelData.Create(tempData);
            LoadLevel(data);
        }
        //backgroundDialog.Load(objectList.backgrounds);

        Input.simulateMouseWithTouches = true;
        //backgroundDialog.Open();
        GetFiles();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (alert.gameObject.activeSelf)
            {
                alert.Close();
            }
            else
            {
                alert.Open("Quit ?", () => { Application.Quit(); });
            }
        }
    }

    public void OpenLevelEditor()
    {

    }

    private void InitOnSceneItems()
    {
        Item[] items = FindObjectsOfType<Item>();
        foreach (Item item in items)
        {
            InitLevelObject(item);
        }
    }

    public void InitLevelObject(Item item)
    {
        GameObject obj = item.gameObject;
        //GridObject grid = obj.AddComponent<GridObject>();
        //grid.size = item.size;
        //grid.offset = item.offset;
        LevelObjectController controller = obj.AddComponent<LevelObjectController>();
        controller.Load(item);
        Destroy(item);
        if (obj.GetComponent<Animator>() != null) obj.GetComponent<Animator>().enabled = false;
    }

    private LevelData GetLevelData()
    {
        LevelObjectController[] objects = FindObjectsOfType<LevelObjectController>();
        LevelData data = new LevelData();
        data.backgroundId = backgroundId;
        data.background = backgroundPath;
        foreach (LevelObjectController levelObject in objects)
        {
            data.objects.Add(levelObject.GetObjectData());
        }
        tempData = data.ToJsonString();
        Debug.Log("data: " + tempData);
        return data;
    }

    public void Play()
    {
        LevelManager.data = GetLevelData();
        Scenes.Load(Scenes.GAME_PLAY);
    }

    public static void Play(string path)
    {
        instance.GetLevelData();
        StreamReader sr = new StreamReader(path);
        LevelData data = LevelData.Create(sr.ReadToEnd());
        LevelManager.data = data;
        Scenes.Load(Scenes.GAME_PLAY);
    }

    private string GetPath()
    {
        string path = Application.persistentDataPath;
        path = Path.Combine(path, "Data");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        return path;
    }

    public string GetPath(string fileName)
    {
        return Path.Combine(GetPath(), fileName);
    }

    public void Save(string fileName)
    {
        LevelObjectController[] levelObjects = FindObjectsOfType<LevelObjectController>();
        LevelData data = new LevelData();
        data.backgroundId = backgroundId;
        data.background = backgroundPath;
        foreach (LevelObjectController levelObject in levelObjects)
        {
            data.objects.Add(levelObject.GetObjectData());
        }
        Debug.Log(data.ToJsonString());
        if (fileName == "") fileName = "level";
        fileName += ".json";
        string path = Path.Combine(GetPath(), fileName);
        StreamWriter sw = new StreamWriter(path);
        sw.WriteLine(data.ToJsonString());
        sw.Close();
    }

    public string[] GetFiles()
    {
        return Directory.GetFiles(GetPath());
    }

    public static void Remove(string fileName)
    {
        if (File.Exists(fileName)) File.Delete(fileName);
    }

    public LevelItemData[] GetBricks(int level)
    {
        return levelObjects[level - 1].bricks;
    }

    public Sprite[] GetBackgrounds(int level)
    {
        return levelObjects[level - 1].backgrounds;
    }

    public string GetBackgroundPath(int level)
    {
        return levelObjects[level - 1].backgroundPath;
    }
    public LevelItemData[] GetTraps()
    {
        return items.traps;
    }

    public LevelItemData[] GetItems()
    {
        return items.items;
    }

    public void ShowTrash(bool isShow)
    {
        trash.SetActive(isShow);
    }

    public static void LoadLevel(string path)
    {
        StreamReader sr = new StreamReader(path);
        LevelData data = LevelData.Create(sr.ReadToEnd());
        LoadLevel(data);
        sr.Close();
    }

    public static void LoadLevel(LevelData data)
    {
        if (data != null)
        {
            LevelObjectController[] objects = FindObjectsOfType<LevelObjectController>();
            foreach (LevelObjectController o in objects)
            {
                Destroy(o.gameObject);
            }
            if (data.background != "")
            {
                instance.backgroundPath = data.background;
                Sprite background = Resources.Load<Sprite>(data.background);
                instance.background.sprite = background;
            }
            foreach (LevelObjectData objectData in data.objects)
            {
                GameObject obj = Instantiate(Resources.Load(objectData.path) as GameObject) as GameObject;
                LevelObjectController controller = obj.AddComponent<LevelObjectController>();
                Item item = obj.GetComponent<Item>();
                controller.Load(item);
                controller.Load(objectData);
                Destroy(item);
                BoxCollider2D col = obj.GetComponent<BoxCollider2D>();
                if (col == null)
                {
                    col = obj.AddComponent<BoxCollider2D>();
                    col.isTrigger = true;
                }
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                if (rb != null) Destroy(rb);
                if (obj.GetComponent<Animator>() != null) obj.GetComponent<Animator>().enabled = false;
            }
        }
    }

    public void Clear()
    {
        LevelObjectController[] objects = FindObjectsOfType<LevelObjectController>();
        foreach (LevelObjectController o in objects)
        {
            if (o.isRemovable) Destroy(o.gameObject);
        }
    }

    public void Select(LevelObjectController levelObject)
    {
        if (selectedObject != null) selectedObject.DeSelect();
        levelObject.Select();
        selectedObject = levelObject;
    }
    public static void SendMail(string file)
    {
        string email = "dunglt@arrowhitech.com";
        string subject = MyEscapeURL("Pyramid Thieves");
        StreamReader sr = new StreamReader(file);
        string body = MyEscapeURL(sr.ReadToEnd());
        sr.Close();
        string url = "mailto:" + email + "?subject=" + subject + "&body=" + body;
        Application.OpenURL(url);

        Debug.Log("Send email " + url);
    }
    static string MyEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }

    public void UnlockLevel(InputField levelText)
    {
        if (levelText.text == "") return;
        int level = int.Parse(levelText.text);
        if (level > Attributes.highest_level)
        {
            Attributes.highest_level = level;
        }
        else
        {
            PlayerPrefs.SetInt(Strings.CURRENT_LEVEL, level);
        }
    }
}
