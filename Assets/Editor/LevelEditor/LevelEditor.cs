using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SceneManagement;

public class LevelEditor : Editor
{
    const string OBJECT_NAME = "LevelEditor";
    const string SCENE_NAME = "MainMenu";
    const string SCENE_PATH = "Assets/Scenes/MainMenu";

    [MenuItem("Level Editor/Enable")]
    public static void Enable()
    {
        GameObject obj = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/LevelEditor/LevelEditor.prefab", typeof(GameObject)) as GameObject;
        PrefabUtility.InstantiatePrefab(obj);
        EditorSceneManager.SaveScene(EditorSceneManager.GetSceneByName(SCENE_NAME));
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        for (int i = 0; i < scenes.Length; i++)
        {
            if (scenes[i].path.Contains("LevelEditScene"))
            {
                scenes[i].enabled = true;
            }
        }
        EditorBuildSettings.scenes = scenes;
    }

    [MenuItem("Level Editor/Disable")]
    public static void Disable()
    {
        //EditorSceneManager.OpenScene(SCENE_PATH);
        if (EditorSceneManager.GetActiveScene().name != SCENE_NAME) return;
        GameObject obj = GameObject.Find(OBJECT_NAME);
        DestroyImmediate(obj);
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        for (int i = 0; i < scenes.Length; i++)
        {
            if (scenes[i].path.Contains("LevelEditScene"))
            {
                scenes[i].enabled = false;
            }
        }
        EditorBuildSettings.scenes = scenes;
    }

    [MenuItem("Level Editor/Enable", true)]
    public static bool EnableValidate()
    {
        return !Validate();
    }

    [MenuItem("Level Editor/Disable", true)]
    public static bool DisableValidate()
    {
        return Validate();
    }

    private static bool Validate()
    {
        GameObject obj = GetGameObject();
        if (obj != null && obj.activeSelf) return true;
        return false;
    }

    private static GameObject GetGameObject()
    {
        UnityEngine.SceneManagement.Scene scene = EditorSceneManager.GetSceneByName(SCENE_NAME);
        GameObject[] objs = scene.GetRootGameObjects();
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].name == OBJECT_NAME)
            {
                return objs[i];
            }
        }
        return null;
    }
}
