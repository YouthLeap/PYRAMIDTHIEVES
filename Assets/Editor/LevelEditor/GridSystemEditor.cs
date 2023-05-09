using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(GridSystem))]
public class GridSystemEditor : Editor {
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Calculate Cell Size"))
        {
            RecalculateCellSize();
        }
        GUILayout.EndHorizontal();
    }

    void RecalculateCellSize()
    {
        var grid = target as GridSystem;
        grid.CalculateCellSize(grid.GetComponent<SpriteRenderer>().bounds.size);
        Debug.Log(grid.GetComponent<SpriteRenderer>().bounds.size);
    }
}
