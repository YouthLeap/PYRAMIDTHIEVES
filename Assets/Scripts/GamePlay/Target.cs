using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null && GameManager.GetState() == GameManager.State.PLAYING)
        {
            GameManager.instance.SetState(GameManager.State.GAMEOVER);
            animator.SetBool("Open", true);
        }
    }
}
