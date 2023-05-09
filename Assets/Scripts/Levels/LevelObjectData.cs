using UnityEngine;
[System.Serializable]
public class LevelObjectData
{
    public string path;
    public Vector3 pos;
    public Vector3 rot;
    public Vector3 scale;

    public Vector3 target;
    public FlyingTrap.MovingMode movingMode;
}
