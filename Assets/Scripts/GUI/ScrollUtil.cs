using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollUtil : MonoBehaviour
{
    [SerializeField]
    private GameObject backButton;
    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private ScrollRect scroll;

    public void Back()
    {
        scroll.horizontalNormalizedPosition = 0;
    }

    public void Next()
    {
        scroll.horizontalNormalizedPosition = 1;
    }

    public void SetNavButtonState()
    {
        Debug.Log(scroll.horizontalNormalizedPosition);
        Debug.Log(scroll.horizontalNormalizedPosition < 0.01f);
        Debug.Log(scroll.horizontalNormalizedPosition > 0.99f);
        backButton.SetActive(scroll.horizontalNormalizedPosition > 0.01f);
        nextButton.SetActive(scroll.horizontalNormalizedPosition < 0.99f);
    }
}
