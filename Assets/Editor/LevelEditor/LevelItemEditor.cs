using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Item), true)]
[CanEditMultipleObjects]
public class LevelItemEditor : Editor
{
    const float cell = 0.72f;

    Vector2 rootPos
    {
        get
        {
            var item = target as Item;
            Vector2 pos;
            pos.x = item.size.x * cell * 0.5f;
            pos.y = item.size.y * cell * 0.5f;
            return pos;
        }
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Autosize"))
        {
            SetAutoSize();
        }
        if (GUILayout.Button("Auto Offset"))
        {
            SetAutoOffset();
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Default Position"))
        {
            SetDefaultPosition();
        }
        if (GUILayout.Button("Set Position"))
        {
            SetPosition();
        }
        GUILayout.EndHorizontal();
    }

    void SetAutoSize()
    {
        var item = target as Item;
        Vector2 size = item.GetComponent<SpriteRenderer>().sprite.bounds.size;
        item.size.x = Mathf.RoundToInt(size.x / cell);
        item.size.y = Mathf.RoundToInt(size.y / cell);
    }

    void SetAutoOffset()
    {
        var item = target as Item;
        Vector2 pos = item.transform.position;
        item.offset = pos - rootPos;
    }

    void SetDefaultPosition()
    {
        var item = target as Item;
        item.transform.position = rootPos;
    }

    void SetPosition()
    {
        var item = target as Item;
        item.transform.position = rootPos + item.offset;
    }
}
