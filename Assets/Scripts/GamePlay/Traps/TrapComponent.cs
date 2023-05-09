using UnityEngine;
using System.Collections;

public class TrapComponent : MonoBehaviour {
    [SerializeField]
    Trap.Effect effect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (effect != Trap.Effect.NONE) other.SendMessage("Dead", effect);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
