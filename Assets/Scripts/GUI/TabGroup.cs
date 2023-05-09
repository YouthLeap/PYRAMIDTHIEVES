using UnityEngine;
using System.Collections;

public class TabGroup : MonoBehaviour
{
    [SerializeField]
    private Tab activeTab;

    public void Active(Tab tab)
    {
        Debug.Log("Active tab " + tab.name);
        if (activeTab == tab) return;
        if (activeTab != null) activeTab.Disable();
        activeTab = tab;
        tab.Enable();
    }
}
