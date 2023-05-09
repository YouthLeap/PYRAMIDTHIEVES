using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Alert : Dialog {
    [SerializeField]
    private Text content;

    private System.Action action;

    public void Open(string content, System.Action action)
    {
        this.content.text = content;
        this.action = action;
        gameObject.SetActive(true);
    }

    public void Confirm()
    {
        if (action != null) action();
        Close();
    }
}
