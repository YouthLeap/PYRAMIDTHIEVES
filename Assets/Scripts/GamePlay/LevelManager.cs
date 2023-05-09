using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{

    public static AssetLevelData levelData; // Use this to assign level by level selector only

    // Use on game scene only
    public static LevelManager instance;
    private static readonly int[] finalLevels = { 10, 20, 24, 29, 30, 40, 50, 54, 59, 60, 70, 75, 80, 85, 89, 90 };

    //public AssetLevelData levelD;

    public static LevelData data;
    public static int level;
    public static int unlockLevel = -1;

    [SerializeField]
    private SpriteRenderer background;

    public static int maxLevel = 90;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Load(data);
    }

    public void Load(LevelData data)
    {
        if (data == null)
        {
            Debug.Log("Level data not found!!!");
            return;
        }
        Clear();
        foreach (LevelObjectData obj in data.objects)
        {
            GameObject o = Instantiate(Resources.Load(obj.path) as GameObject) as GameObject;
            Item item = o.GetComponent<Item>();
            if (item.GetType() == typeof(FlyingTrap))
            {
                ((FlyingTrap)item).LoadAditionalData(obj);
            }
            if (item.GetType() == typeof(MovingTrap))
            {
                ((MovingTrap)item).LoadAditionalData(obj);
            }
            item.LoadData(obj);
        }
        if (data.background != "")
        {
            Sprite bg = Resources.Load<Sprite>(data.background);
            background.sprite = bg;
        }
    }

    public void Load(AssetLevelData data)
    {
        if (data == null)
        {
            Debug.Log("Level data not found!!!");
            return;
        }
        Clear();
        foreach (LevelObject levelObject in data.GetAllObjects())
        {
            levelObject.CreateInstance();
        }
    }

    void Clear()
    {
        if (transform.childCount > 0)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }

    public static void PlayLevel(int level, System.Action fallback = null)
    {
        LevelManager.level = level;
        if (level > 0)
        {
            if (Attributes.lives < 3) Attributes.PayLives();
            if (Attributes.lives > 0)
            {
                LevelList levels = Resources.Load("LevelList") as LevelList;
                data = LevelData.Create(levels.levels[level - 1].text);
                Scenes.Load(Scenes.GAME_PLAY);
            }
            else
            {
                if (fallback != null) fallback();
            }
        }
    }

    public static bool IsShowUnlockNewLevel()
    {
        if (level + 1 < Attributes.highest_level) return false;
        for (int i = 0; i < finalLevels.Length; i++)
        {
            if (finalLevels[i] == level) return true;
        }
        return false;
    }

    public static void UnlockLevel(int level)
    {
        unlockLevel = level;
        Scenes.Load(Scenes.MAP_SCENE);
    }

    public static string LevelToString(int level)
    {
        return string.Format("{0} - {1}", level / 30 + 1 - ((level % 30 == 0) ? 1 : 0), level % 30 + (level % 30 == 0 ? 30 : 0));
    }
}
