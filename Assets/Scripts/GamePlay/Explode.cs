using UnityEngine;
using System.Collections;

public class Explode : Trap
{
    [SerializeField]
    private Animator animator;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            other.GetComponent<PlayerController>().Dead(PlayerController.BURNING_DEAD);
            animator.enabled = true;
        }
    }

    public new void AnimationEvent()
    {
        Destroy(gameObject);
    }
}
