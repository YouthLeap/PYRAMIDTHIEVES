using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    public string path;
    public Vector2 size = Vector2.one;
    public Vector2 offset = Vector2.zero;

    [SerializeField]
    private bool isDisableAnimatorOnPause = true;
    [SerializeField]
    protected bool haveSound = false;
    [SerializeField]
    protected AudioSource sound;
    [SerializeField]
    protected float customSoundLength = 3.0f;
    [SerializeField]
    protected GameManager.State stateToPlaySFX = GameManager.State.PLAYING;
    private float fxTimer = 0;
    void Start()
    {
        Init();
        CheckGameState();
    }

    void Update()
    {
        CheckSFXPlay();
    }

    protected void CheckSFXPlay()
    {
        if (GameManager.GetState() == stateToPlaySFX && customSoundLength > 0 && haveSound)
        {
            if (fxTimer > customSoundLength)
            {
                fxTimer = 0;
                sound.Play();
            }
            else
            {
                fxTimer += Time.deltaTime;
            }
        }
    }

    protected virtual void Init() { }
    protected virtual void CheckGameState()
    {
        if (GameManager.GetState() != GameManager.State.PLAYING) Pause();
        if (GameManager.GetState() != GameManager.State.NOT_AVAILABLE)
        {
            GameManager.instance.items.Add(this);
        }
        if (haveSound && SoundManager.instance != null)
        {
            SoundManager.instance.AddSource(GetComponent<AudioSource>());
        }
    }
    public virtual void AnimationEvent() { }

    public void LoadData(LevelObjectData data)
    {
        transform.position = data.pos;
        transform.localScale = data.scale;
        transform.eulerAngles = data.rot;
    }

    public void Pause(bool isPause = true)
    {
        if (isDisableAnimatorOnPause)
        {
            Animator anim = GetComponent<Animator>();
            if (anim != null)
            {
                anim.enabled = !isPause;
            }
        }
    }
}
