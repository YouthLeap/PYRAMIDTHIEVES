using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;

public class LevelEditorWindow : EditorWindow
{
    public static LevelEditorWindow instance;

    static string dataPath = "Assets/Data/Levels/";

    static string levelName = EditorPrefs.GetString("level_name", "Level_1");
    static Transform levelContainer;
    static AssetLevelData levelData;
    static Sprite background;

    public static void OpenWindow()
    {
        instance = (LevelEditorWindow)EditorWindow.GetWindow(typeof(LevelEditorWindow));
    }

    void OnGUI()
    {
        if (EditorSceneManager.GetActiveScene() != EditorSceneManager.GetSceneByName("LevelEditor"))
        {
            if (GUILayout.Button("Open Level Editor"))
            {
                EditorSceneManager.OpenScene("Assets/Scenes/LevelEditScene.unity");
            }
            return;
        }
        levelData = (AssetLevelData)EditorGUILayout.ObjectField("Data Object", levelData, typeof(AssetLevelData), false);
        if (levelContainer == null) levelContainer = GameObject.Find("Level").transform;
        if (background == null) background = GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite;
        if (levelData == null) levelData = AssetDatabase.LoadAssetAtPath(GetPath(), typeof(AssetLevelData)) as AssetLevelData;
        levelName = EditorGUILayout.TextField("Name", levelName);
        levelContainer = (Transform)EditorGUILayout.ObjectField("Container", levelContainer, typeof(Transform), true);
        background = (Sprite)EditorGUILayout.ObjectField("Background", background, typeof(Sprite), true);

        EditorGUILayout.Separator();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear"))
        {
            Clear();
        }
        if (GUILayout.Button("Load"))
        {
            Load();
        }
        if (GUILayout.Button("Save"))
        {
            Save();
        }
        EditorGUILayout.EndHorizontal();
    }

    void Save()
    {
        AssetLevelData level = ScriptableObject.CreateInstance<AssetLevelData>();
        for (int i = 0; i < levelContainer.childCount; i++)
        {
            Transform obj = levelContainer.GetChild(i);
            LevelObject levelObject = LevelObject.Create(PrefabUtility.GetPrefabParent(obj.gameObject) as GameObject, obj);
            level.Add(levelObject);
            Debug.Log(obj.name);
            if (obj.name == "Door")
            {
                level.doorPosision = obj.transform.position;
            }
        }
        level.background = background;
        AssetDatabase.CreateAsset(level, GetPath());
        AssetDatabase.SaveAssets();
        levelData = level;
    }

    void Load()
    {
        if (levelData == null) return;
        levelName = levelData.name;
        EditorPrefs.SetString("level_name", levelName);
        Clear();
        LevelObject[] objects = levelData.GetAllObjects().ToArray();
        foreach (LevelObject obj in objects)
        {
            GameObject levelObj = PrefabUtility.InstantiatePrefab(obj.prefab) as GameObject;
            Transform objTransform = levelObj.transform;
            objTransform.position = obj.position;
            objTransform.eulerAngles = obj.rotation;
            objTransform.localScale = obj.scale;
            objTransform.SetParent(levelContainer);
        }
    }

    void Clear()
    {
        if (levelContainer == null) return;
        for (int i = levelContainer.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(levelContainer.GetChild(i).gameObject);
        }
    }

    string GetPath()
    {
        return dataPath + levelName + ".asset";
    }
}
