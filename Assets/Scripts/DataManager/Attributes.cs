using UnityEngine;
using System.Collections;

public class Attributes
{
    private const int DEFAULT_DIAMONDS = 10;
    private const int START_LEVEL = 1;
    private const int DEFAULT_LIVES = 3;
    public static int gems
    {
        get
        {
            if (PlayerPrefs.GetInt(Strings.DIAMONDS, DEFAULT_DIAMONDS) < 0)
                PlayerPrefs.SetInt(Strings.DIAMONDS, 0);
            return PlayerPrefs.GetInt(Strings.DIAMONDS, DEFAULT_DIAMONDS);
        }
        set { PlayerPrefs.SetInt(Strings.DIAMONDS, value); }
    }
    
    public static int lives
    {
        get { return PlayerPrefs.GetInt(Strings.LIVES, DEFAULT_LIVES); }
        set { PlayerPrefs.SetInt(Strings.LIVES, Mathf.Clamp(value, 0, DEFAULT_LIVES)); }
    }

    public static int highest_level
    {
        get {
            if (PlayerPrefs.GetInt(Strings.CURRENT_LEVEL, START_LEVEL) > LevelManager.maxLevel)
                return LevelManager.maxLevel;
            return PlayerPrefs.GetInt(Strings.CURRENT_LEVEL, START_LEVEL);
        }
        set
        {

            if (value > PlayerPrefs.GetInt(Strings.CURRENT_LEVEL))
                PlayerPrefs.SetInt(Strings.CURRENT_LEVEL, value);
        }
    }

    public static System.DateTime diamonMachineTime
    {
        get
        {
            string time = PlayerPrefs.GetString(Strings.TARGET_TIME, System.DateTime.Now.ToString());
            return System.DateTime.Parse(time);
        }
        set
        {
            PlayerPrefs.SetString(Strings.TARGET_TIME, value.ToString());
        }
    }

    public static bool settingShowInfo
    {
        get { return (PlayerPrefs.GetInt(Strings.SETTING_SHOW_INFO, Strings.ON) == Strings.ON); }
        set { PlayerPrefs.SetInt(Strings.SETTING_SHOW_INFO, value ? Strings.ON : Strings.OFF); }
    }

    public static int GetStars(int level)
    {
        return PlayerPrefs.GetInt(Strings.STARS_LEVEL + level);
    }

    public static void SetStars(int level, int stars)
    {
        if (stars > GetStars(level))
            PlayerPrefs.SetInt(Strings.STARS_LEVEL + level, stars);
    }

    public static int selectedPlayer
    {
        get { return PlayerPrefs.GetInt(Strings.SELECTED_PLAYER); }
        set { PlayerPrefs.SetInt(Strings.SELECTED_PLAYER, value); }
    }

    public static bool GetPlayer(int id)
    {
        if (id == 0) return true;
        return (PlayerPrefs.GetInt(Strings.PURCHASED_PLAYER + id, Strings.OFF) == Strings.ON);
    }

    public static void PurchasedPlayer(int id)
    {
        PlayerPrefs.SetInt(Strings.PURCHASED_PLAYER + id, Strings.ON);
    }

    public static void PayLives(int ammount = 3)
    {
        if (gems > 0)
        {
            GameManager.isPaidLives = true;
            gems--;
            lives = ammount;
        }
    }

    public static bool soundOn
    {
        get
        {
            return (PlayerPrefs.GetInt(Strings.SETTING_SOUNDS, Strings.ON) == Strings.ON);
        }
        set
        {
            PlayerPrefs.SetInt(Strings.SETTING_SOUNDS, value ? Strings.ON : Strings.OFF);
        }
    }
}