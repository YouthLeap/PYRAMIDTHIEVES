using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
    [SerializeField]
    private Text message;
    public void Show(string message)
    {
        this.message.text = message;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        message.text = "";
        gameObject.SetActive(false);
    }
}
