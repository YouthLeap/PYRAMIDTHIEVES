using UnityEngine;
using System.Collections;

public class GridSystem : MonoBehaviour
{
    
    //private Vector2 center;
    //private Sprite gridSprite;

    [SerializeField]
    private int width = 14;
    [SerializeField]
    private int height = 8;
    [SerializeField]
    private Vector2 cellSize = new Vector2(0.72f, 0.72f);
    private Vector2 size;

    void Start()
    {
        size.x = cellSize.x * width;
        size.y = cellSize.y * height;
    }

    public Vector2 GetPosition(Vector2 coord, Vector2 size, Vector2 offset)
    {
        Vector2 pos;
        pos.x = ((size.x - width) * 0.5f + coord.x) * cellSize.x + offset.x;
        pos.y = ((size.y - height) * 0.5f + coord.y) * cellSize.y + offset.y;
        pos += (Vector2)transform.position;
        return pos;
    }

    public Vector2 GetCoordinates(Vector2 pos)
    {
        pos -= (Vector2)transform.position;
        if (Mathf.Abs(pos.x) > size.x / 2)
        {
            pos.x = Mathf.Sign(pos.x) * size.x / 2;
        }
        if (Mathf.Abs(pos.y) > size.y / 2)
        {
            pos.y = Mathf.Sign(pos.y) * size.y / 2;
        }
        Vector2 coordPos = pos + size / 2;
        Vector2 coord;
        coord.x = (int)(coordPos.x / cellSize.x);
        coord.y = (int)(coordPos.y / cellSize.y);
        return coord;
    }

    public void CalculateCellSize(Vector2 boundarySize)
    {
        cellSize.x = boundarySize.x / width;
        cellSize.y = boundarySize.y / height;
    }
}
