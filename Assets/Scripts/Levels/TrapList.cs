using UnityEngine;
using System.Collections;
[CreateAssetMenu(menuName = "Custom/Items")]
public class TrapList : ScriptableObject
{
    public LevelItemData[] traps;
    public LevelItemData[] items;
}
