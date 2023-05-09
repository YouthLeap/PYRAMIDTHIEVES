using UnityEngine;
using System.Collections;

public class Trap : Item
{
    [SerializeField]
    private Effect effect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (effect != Effect.NONE) other.SendMessage("Dead", effect, SendMessageOptions.DontRequireReceiver);
    }

    public enum Effect
    {
        NONE,
        DEAD,
        BURN
    }
}
