using UnityEngine;
using System.Collections;

public class GridObject : MonoBehaviour
{
    public GridSystem gridSystem;

    private Vector3 positionOffset;
    public Vector2 size = Vector2.one;
    public Vector2 offset = Vector2.zero;

    void Start()
    {
        if (gridSystem == null) gridSystem = GameObject.Find("GridSystem").GetComponent<GridSystem>();
    }
    void OnMouseDown()
    {
        positionOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionOffset.z = transform.position.z;
    }

    void OnMouseDrag()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 coord = gridSystem.GetCoordinates(pos);
        pos = gridSystem.GetPosition(coord, size, offset);
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
