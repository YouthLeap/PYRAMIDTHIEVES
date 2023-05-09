using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ObjectControlPanel : MonoBehaviour
{
    public static ObjectControlPanel instance;
    public LevelObjectController activeObject;
    
    [SerializeField]
    private float minScale = 0.1f;
    [SerializeField]
    private float maxScale = 2.0f;
    
    [SerializeField]
    private ScaleBox scaleBox;

    [SerializeField]
    private Text scale;

    [SerializeField]
    private Button movingMode;
    [SerializeField]
    private Image movingButtonState;
    [SerializeField]
    private Sprite circle;
    [SerializeField]
    private Sprite straight;
    
    public void Select(LevelObjectController obj)
    {
        if (obj == activeObject)
        {
            activeObject = null;
            obj.DeSelect();
            gameObject.SetActive(false);
        }
        else
        {
            if (activeObject != null) activeObject.DeSelect();
            activeObject = obj;
            obj.Select();
            gameObject.SetActive(true);
            if (obj.isShowFlyingMode)
            {
                movingMode.gameObject.SetActive(true);
                SetMovingMode(obj, obj.target.movingMode);
            }
            else
            {
                movingMode.gameObject.SetActive(false);
            }
        }
    }

    public void Remove()
    {
        if (activeObject == null || !activeObject.isRemovable) return;
        Destroy(activeObject.gameObject);
        activeObject = null;
        gameObject.SetActive(false);
    }

    public void SwitchMovingMode()
    {
        Debug.Log("Switch moving mode");
        switch (activeObject.target.movingMode)
        {
            case FlyingTrap.MovingMode.STRAIGHT:
                Debug.Log("Switch moving mode " + activeObject.target.movingMode);
                SetMovingMode(activeObject, FlyingTrap.MovingMode.CIRCULAR);
                break;
            case FlyingTrap.MovingMode.CIRCULAR:
                SetMovingMode(activeObject, FlyingTrap.MovingMode.STRAIGHT);
                break;
        }
    }

    public void SetMovingMode(LevelObjectController obj, FlyingTrap.MovingMode mode)
    {
        obj.target.movingMode = mode;
        switch (mode)
        {
            case FlyingTrap.MovingMode.STRAIGHT:
                movingButtonState.sprite = straight;
                break;
            case FlyingTrap.MovingMode.CIRCULAR:
                movingButtonState.sprite = circle;
                break;
        }
    }
    public void IncreaseScale(float value)
    {
        if (activeObject == null) return;
        float scale = activeObject.transform.localScale.x + value;
        if (scale > maxScale) scale = maxScale;
        if (scale < minScale) scale = minScale;
        activeObject.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void Rotate(float angle)
    {
        if (activeObject == null) return;
        Vector3 rot = activeObject.transform.eulerAngles;
        rot.z += angle;
        activeObject.transform.eulerAngles = rot;
    }

    public void Flip()
    {
        if (activeObject == null) return;
        Vector3 rot = activeObject.transform.eulerAngles;
        rot.y += 180;
        activeObject.transform.eulerAngles = rot;
    }

    void Update()
    {
        if (activeObject != null)
        {
            scale.text = string.Format("{0:0.0}x", activeObject.transform.localScale.x);
        }
    }
}
