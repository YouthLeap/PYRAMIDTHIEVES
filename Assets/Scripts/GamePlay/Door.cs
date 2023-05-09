using UnityEngine;
using System.Collections;

public class Door : Item
{
    [SerializeField]
    private Animator animator;
    
    void Start()
    {
        GameManager.GetPlayer().gameObject.SetActive(false);
        GameManager.GetPlayer().SetState(PlayerController.State.PAUSE);
        if (GameManager.GetState() == GameManager.State.START || GameManager.GetState() == GameManager.State.PAUSE)
        {
            animator.enabled = false;
            GameManager.instance.AddStartGameEvent(name, () =>
            {
                animator.enabled = true;
            });
        }
    }

    public void OnOpen()
    {
        GameManager.GetPlayer().transform.position = transform.position;
        GameManager.GetPlayer().gameObject.SetActive(true);
        GameManager.GetPlayer().SetState(PlayerController.State.PLAY);
    }
}
