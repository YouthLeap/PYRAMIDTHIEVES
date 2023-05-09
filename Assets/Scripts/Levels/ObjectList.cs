using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Custom/Object List")]
public class ObjectList : ScriptableObject
{
    public Sprite[] backgrounds;
    public LevelItemData[] bricks;
    public LevelItemData[] traps;
    public LevelItemData[] items;
}
