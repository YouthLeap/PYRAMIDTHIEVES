using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Level Object List")]
public class LevelObjectList : ScriptableObject
{
    public string backgroundPath;
    public Sprite[] backgrounds;
    public LevelItemData[] bricks;
}
