using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Custom/Levels")]
public class LevelList : ScriptableObject
{
    public TextAsset[] levels;
}
