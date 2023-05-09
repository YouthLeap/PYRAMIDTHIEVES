using UnityEngine;
using System.Collections;

public class MapSelector : MonoBehaviour
{
    public static MapSelector instance;
    [SerializeField]
    private ScrollController scrollController;
    public bool isUnlocking = false;
    [SerializeField]
    private AudioSource sfx;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GoogleAnalyticsV4.instance.LogScreen(Scenes.MAP_SCENE);
        sfx.mute = !Attributes.soundOn;
    }

    public void PlayDoorSFX()
    {
        if (Attributes.soundOn) sfx.Play();
    }

    public void StopDoorSFX()
    {
        sfx.Stop();
    }

    public void BackToMainMenu()
    {
        if (!isUnlocking) Scenes.Load(Scenes.MAIN_MENU);
    }

    public void UnlockLevel(PyramidDoor door, int level)
    {
        if (level <= 1) return;
        isUnlocking = true;
        scrollController.ScrollTo(() => { door.Unlock(); });
    }
}
