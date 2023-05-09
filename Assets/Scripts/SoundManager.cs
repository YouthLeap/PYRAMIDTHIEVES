using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {
    [SerializeField]
    private AudioSource bgm;
    [SerializeField]
    private AudioClip[] bgmClips;

    public static SoundManager instance;

    private List<AudioSource> sources = new List<AudioSource>();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (bgmClips.Length > 0)
        {
            bgm.clip = bgmClips[Random.Range(0, bgmClips.Length)];
            bgm.Play();
        }
        bgm.mute = !Attributes.soundOn;
        //GameManager.instance.AddStartGameEvent(name, PlayAllSFX);
    }

    void PlayAllSFX()
    {
        foreach(AudioSource src in sources)
        {
            src.Play();
        }
    }

    public void AddSource(AudioSource source)
    {
        sources.Add(source);
    }

    public void ToggleSound(bool isOn)
    {
        bgm.mute = !isOn;
        foreach(AudioSource src in sources)
        {
            src.mute = !isOn;
        }
    }
}
