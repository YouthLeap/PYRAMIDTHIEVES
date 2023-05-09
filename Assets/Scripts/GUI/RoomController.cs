using UnityEngine;
using System.Collections;

public class RoomController : MonoBehaviour
{
    [SerializeField]
    private GUIAnimationEvent guiEvent;
    [SerializeField]
    private Animator animator;
    private const string SLIDE_OUT = "SlideOut";

    void Start()
    {
        guiEvent.onCloseEvt = () => { gameObject.SetActive(false); };
    }

    public void Open()
    {
        gameObject.SetActive(true);
        animator.SetBool(SLIDE_OUT, true);
    }

    public void Close()
    {
        animator.SetBool(SLIDE_OUT, false);
    }
}
