using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private State state = State.START;
    [SerializeField]
    private PlayerController player;
    private Dictionary<string, System.Action> onGameStartListener = new Dictionary<string, System.Action>();
    [SerializeField]
    private HUDController hud;
    [SerializeField]
    private GameObject[] players;
    [SerializeField]
    private GameObject tutorialPrefab;
    private static bool isShowTutorial;

    public List<Item> items = new List<Item>(); // Don't call any method of any item at start or before !!!

    public bool isComplete = false;
    public static bool isPaidLives = false;
    void Awake()
    {
        instance = this;
        AdmobController.CreateInstance();
        SwitchPlayer();
        isShowTutorial = PlayerPrefs.GetInt(Strings.SHOW_GAME_TUTORIAL, Strings.ON) == Strings.ON;
        PlayerPrefs.SetInt(Strings.SHOW_GAME_TUTORIAL, Strings.OFF);
    }

    void Start()
    {
        items.ForEach((Item item) => { item.Pause(); });
        if (GoogleAnalyticsV4.instance != null) GoogleAnalyticsV4.instance.LogScreen(Scenes.GAME_PLAY + " Level " + LevelManager.LevelToString(LevelManager.level));
    }

    void Update()
    {
        bool isButtonClicked = (EventSystem.current.IsPointerOverGameObject());
        if (Input.GetMouseButtonDown(0) && isButtonClicked)
        {
            hud.PlayButtonSFX();
        }
        switch (state)
        {
            case State.START:
                if (Input.GetMouseButtonDown(0) && !isButtonClicked)
                {
                    StartGame();
                }
                break;
        }
    }

    public void CloseInfoPanel()
    {
        if (!isShowTutorial)
        {
            StartGame();
        }
        else
        {
            SetState(State.TUTORIAL);
            isShowTutorial = false;
        }
    }

    public void StartGame()
    {
        SetState(State.PLAYING);
        hud.SetLifeText();
    }

    public void Pause(bool isPause = true)
    {
        if (isPause)
        {
            SetState(State.PAUSE);
            hud.pauseDialog.Open();
        }
        else
        {
            SetState(State.PLAYING);
            hud.pauseDialog.Close();
        }
        foreach (Item item in items)
        {
            item.Pause(isPause);
        }
    }

    public void FinishLevel(bool isComplete)
    {
        if (isComplete)
        {
            hud.levelCompleteDialog.GetComponent<AudioSource>().mute = !Attributes.soundOn;
            hud.levelCompleteDialog.Open();
            hud.levelCompleteDialog.SetStars(Attributes.lives);
            Attributes.SetStars(LevelManager.level, Attributes.lives);
            if (GoogleAnalyticsV4.instance != null) GoogleAnalyticsV4.instance.LogEvent(new EventHitBuilder().SetEventCategory("GamePlay")
                .SetEventAction("Complete level " + LevelManager.LevelToString(LevelManager.level))
                .SetEventLabel("Finish level " + LevelManager.level + " with " + Attributes.lives + " stars"));
            Attributes.highest_level = LevelManager.level + 1;
            Attributes.lives = 3; // Reset live to 3
        }
        else
        {
            Attributes.lives--;
            if (Attributes.lives > 0)
            {
                Scenes.Reload();
            }
            else
            {
                hud.SetLifeText();
                hud.gameOverDialog.GetComponent<AudioSource>().mute = !Attributes.soundOn;
                hud.gameOverDialog.Open();
                if (GoogleAnalyticsV4.instance != null)
                    GoogleAnalyticsV4.instance.LogEvent(new EventHitBuilder().SetEventCategory("GamePlay")
                    .SetEventAction("GameOver at level " + LevelManager.LevelToString(LevelManager.level)));
                AdmobController.instance.ShowInterstitial();
            }
        }
    }

    public void PlayNextLevel()
    {
        int nextLevel = LevelManager.level + 1;
        if (LevelManager.IsShowUnlockNewLevel())
        {
            LevelManager.UnlockLevel(nextLevel);
        }
        else
        {
            LevelManager.PlayLevel(LevelManager.level + 1);
        }
    }

    public void ReloadGameScene()
    {
        if (Attributes.lives > 0)
        {
            Scenes.Reload();
        }
        else
        {
            if (Attributes.gems > 0)
                hud.livesPurchaseDialog.Open();
            else
                hud.ShowToast("Not enough gems!");
        }
    }

    public void PayAndReplay()
    {
        Attributes.PayLives();
        if (Attributes.lives > 0) Scenes.Reload();
    }

    public void BackToLevelEditor()
    {
        Scenes.Load(Scenes.LEVEL_EDITOR);
        AdmobController.instance.HideBanner();
    }

    public void SetState(State state)
    {
        this.state = state;
        switch (state)
        {
            case State.PAUSE:
                player.SetState(PlayerController.State.PAUSE);
                break;
            case State.PLAYING:
                player.SetState(PlayerController.State.PLAY);
                items.ForEach((Item item) => { item.Pause(false); });
                foreach (KeyValuePair<string, System.Action> action in onGameStartListener)
                {
                    action.Value();
                }
                break;
            case State.GAMEOVER:
                state = State.GAMEOVER;
                player.SetState(PlayerController.State.GAME_OVER);
                break;
            case State.TUTORIAL:
                Instantiate(tutorialPrefab);
                break;
        }
    }

    public static PlayerController GetPlayer()
    {
        return instance.player;
    }

    public static State GetState()
    {
        if (instance == null) return State.NOT_AVAILABLE;
        return instance.state;
    }

    public void AddStartGameEvent(string name, System.Action action)
    {
        if (onGameStartListener.ContainsKey(name))
        {
            onGameStartListener[name] = action;
        }
        else
        {
            onGameStartListener.Add(name, action);
        }
    }

    public void RemoveOnStartGameEvent(string name)
    {
        if (onGameStartListener.ContainsKey(name))
        {
            onGameStartListener.Remove(name);
        }
    }

    public enum State
    {
        START,
        PLAYING,
        PAUSE,
        GAMEOVER,
        TUTORIAL,
        NOT_AVAILABLE
    }

    private void SwitchPlayer()
    {
        int id = PlayerPrefs.GetInt(Strings.SELECTED_PLAYER, 0);
        if (id == 0) return;
        if (id > players.Length) return;
        Destroy(player.gameObject);
        GameObject newPlayer = Instantiate(players[id - 1]) as GameObject;
        player = newPlayer.GetComponent<PlayerController>();
    }

    public static bool IsPlayerTag(string tag)
    {
        return (instance.player.tag == tag);
    }

    public void BackToMainMenu()
    {
        Scenes.Load(Scenes.MAIN_MENU);
    }

    public void ShowAdmobBanner(bool isShow)
    {
        if (isShow)
            AdmobController.instance.ShowBanner();
        else
            AdmobController.instance.HideBanner();
    }

}