using UnityEngine;
using System.Collections;

public class Treasure : Item
{
    [SerializeField]
    private Vector3 offsetPosition;
    void Start()
    {
        // Override start function
        sound.mute = !Attributes.soundOn;
        if (haveSound && SoundManager.instance != null)
        {
            SoundManager.instance.AddSource(sound);
        }
    }

    [SerializeField]
    private Animator animator;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.GetState() == GameManager.State.PLAYING &&
            GameManager.GetPlayer().playerState == PlayerController.State.PLAY &&
            GameManager.IsPlayerTag(other.gameObject.tag))
        {
            GameManager.instance.isComplete = true;
            GameManager.instance.SetState(GameManager.State.GAMEOVER);
            //GameManager.GetPlayer().transform.position = transform.position + offsetPosition;
            animator.enabled = true;
        }
    }

    public override void AnimationEvent()
    {
        GameManager.instance.FinishLevel(true);
        haveSound = false;
    }
}
