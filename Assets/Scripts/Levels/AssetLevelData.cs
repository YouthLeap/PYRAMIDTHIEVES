using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Custom/Level")]
public class AssetLevelData : ScriptableObject
{
    public Sprite background;
    public Vector3 doorPosision;

    [SerializeField]
    private List<LevelObject> levelObjects;

    public void Add(LevelObject levelObject)
    {
        if (levelObjects == null) levelObjects = new List<LevelObject>();
        levelObjects.Add(levelObject);
    }

    public List<LevelObject> GetAllObjects()
    {
        return levelObjects;
    }
}
