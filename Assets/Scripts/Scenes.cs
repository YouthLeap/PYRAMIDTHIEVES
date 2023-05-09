using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;
public class Scenes
{
    public const string LEVEL_EDITOR = "LevelEditScene";
    public const string GAME_PLAY = "GamePlay";
    public const string MAP_SCENE = "MapSelector";
    public const string MAIN_MENU = "MainMenu";

    public static void Load(string name)
    {
        if (AdmobController.instance != null) AdmobController.instance.HideBanner();
        SceneManager.LoadScene(name);
    }

    public static void Reload()
    {
        if (AdmobController.instance != null) AdmobController.instance.HideBanner();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static bool IsScene(string scene)
    {
        return SceneManager.GetActiveScene().name == scene;
    }
}
