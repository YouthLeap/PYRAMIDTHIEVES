using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private DialogGroup group;

    public void Open()
    {
        if (group == null)
        {
            gameObject.SetActive(true);
            return;
        }
        if (group.activeDialog == this) return;
        if (group.activeDialog != null) group.activeDialog.Close();
        gameObject.SetActive(true);
        group.activeDialog = this;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Toogle()
    {
        bool isActive = !gameObject.activeSelf;
        bool isClose = (group.activeDialog != null && group.activeDialog != this);
        if (isClose) group.activeDialog.Close();
        if (isActive) group.activeDialog = this;
        gameObject.SetActive(isActive);
    }
}
