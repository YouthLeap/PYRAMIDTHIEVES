using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class LevelObjectController : MonoBehaviour
{
    public float scale = 1;
    [SerializeField]
    private string path;

    public bool isRemovable = true;

    public TargetController target;
    private Vector3 targetPos;

    private Vector3 positionOffset;

    public bool isShowFlyingMode = false;
    
    void Start()
    {
        if (GetComponent<Animator>() != null) GetComponent<Animator>().enabled = false;
    }

    public void Load(LevelObjectData data)
    {
        path = data.path;
        transform.position = data.pos;
        transform.localScale = data.scale;
        transform.eulerAngles = data.rot;
        //targetPos = data.target;
        if (target != null)
        {
            //target.transform.localPosition = data.target;
            //Debug.Log(data.target);
            target.SetPosition(data.target);
            target.movingMode = data.movingMode;
        }
    }

    public void Load(Item data)
    {
        path = data.path;
        if (data.GetType() == typeof(Treasure) || data.GetType() == typeof(Door))
        {
            isRemovable = false;
        }
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (col == null)
        {
            col = gameObject.AddComponent<BoxCollider2D>();
            col.isTrigger = true;
        }
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) Destroy(rb);
        if (data.GetType() == typeof(FlyingTrap) || data.GetType() == typeof(MovingTrap))
        {
            GameObject targetObj = Instantiate(LevelEditor.instance.targetPrefab) as GameObject;
            target = targetObj.GetComponent<TargetController>();
            target.SetOriginTransform(transform);
            targetObj.transform.SetParent(transform);
            
            if (data.GetType() == typeof(FlyingTrap))
            {
                target.isFlying = true;
                isShowFlyingMode = true;
            }
        }
    }
    
    void OnMouseDrag()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = transform.position.z;
        transform.position = pos + positionOffset;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        positionOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionOffset.z = transform.position.z;
        ObjectControlPanel.instance.Select(this);
    }

    public void Select()
    {
        foreach (SpriteRenderer sr in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = Color.cyan;
        }
    }

    public void DeSelect()
    {
        LevelEditor.instance.selectedObject = null;
        foreach (SpriteRenderer sr in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = Color.white;
        }
    }

    public LevelObjectData GetObjectData()
    {
        LevelObjectData data = new LevelObjectData();
        data.path = path;
        data.pos = transform.position;
        data.rot = transform.eulerAngles;
        data.scale = transform.localScale;
        if (target != null)
        {
            data.target = target.transform.localPosition * transform.localScale.y;
            data.movingMode = target.movingMode;
        }
        return data;
    }
}
