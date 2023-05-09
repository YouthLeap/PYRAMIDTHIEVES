using UnityEngine;
using System.Collections;

public class Strings
{
    public const string SHOW_GAME_TUTORIAL = "GameplayTutorial";
    public const string SHOW_MENU_TUTORIAL = "MenuTutorial";

    public const string ANIMATION_JUMP = "Jump";
    public const string ANIMATION_WALK = "Walk";
    public const string ANIMATION_WALLJUMP = "WallJump";
    public const string ANIMATION_WALLFALL = "WallFall";

    public const string LIVES = "Lives";
    public const string DIAMONDS = "Diamonds";
    public const string CURRENT_LEVEL = "canPlayLevel";
    public const string STARS_LEVEL = "star_at_level_";
    public const string TARGET_TIME = "TargetTime";

    public const string MAP_POSITION = "MapPosition";
    public const string SELECTED_PLAYER = "SelectedPlayer";
    public const string PURCHASED_PLAYER = "PurchasedPlayer";
    public const string SETTING_SHOW_INFO = "SettingShowInfo";
    public const string SETTING_SOUNDS = "SettingSound";
    
    public const int ON = 0;
    public const int OFF = 1;

#if UNITY_ANDROID
    public const string BANNER_URL = "http://www.iphone-vietnam.com/ahtservices?method=getAdsApp&device=google_play&name_app=pyramidthieve";
#elif UNITY_IOS
    public const string BANNER_URL = "http://www.iphone-vietnam.com/ahtservices?method=getAdsApp&device=ios&name_app=pyramidthieve";
#endif
    public const string BANNER_SUCCESS = "SUCCESS";
}
