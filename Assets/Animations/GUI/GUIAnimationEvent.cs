using UnityEngine;
using System.Collections;

public class GUIAnimationEvent : MonoBehaviour
{
    public System.Action onOpenEvt;
    public System.Action onCloseEvt;

    public void OnOpen()
    {
        if (onOpenEvt != null) onOpenEvt();
    }

    public void OnClose()
    {
        if (onCloseEvt != null) onCloseEvt();
    }
}
