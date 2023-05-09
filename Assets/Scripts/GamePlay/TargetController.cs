using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour
{
    private Transform originTransform;
    public FlyingTrap.MovingMode movingMode;
    private float minDistance = 0.8f;
    public bool isFlying;
    [SerializeField]
    Mode mode = Mode.Horizontal;
    float minSwitchValue = 0.01f;
    void Start()
    {
        if (transform.localPosition.magnitude < minDistance)
        {
            Vector3 pos = transform.localPosition;
            if (pos.x < pos.y)
                pos.y = minDistance;
            else
                pos.x = minDistance;
            transform.localPosition = pos;
        }
    }
    void OnMouseDrag()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CheckModeSwitch(pos);
        pos.z = transform.position.z;
        if (mode == Mode.Horizontal)
        {
            pos.y = originTransform.position.y;
            //pos.x = originTransform.position.x + minDistance * Mathf.Sign(pos.x);
            if (Mathf.Abs(originTransform.transform.position.x - pos.x) < minDistance)
                pos.x = originTransform.position.x + minDistance * Mathf.Sign(pos.x);
        }
        else
        {
            pos.x = originTransform.position.x;
            if (Mathf.Abs(originTransform.transform.position.y - pos.y) < minDistance)
                pos.y = originTransform.position.y + minDistance * Mathf.Sign(pos.y);
        }
        transform.position = pos;
    }
    public void SetOriginTransform(Transform origin)
    {
        originTransform = origin;
    }

    void SetMode(Mode mode)
    {
        this.mode = mode;
    }

    void CheckModeSwitch(Vector3 mousePos)
    {
        if (!isFlying)
        {
            mode = Mode.Horizontal;
            return;
        }
        Vector3 pos = mousePos - originTransform.position;
        mode = (Mathf.Abs(pos.x) > Mathf.Abs(pos.y)) ? Mode.Horizontal : Mode.Vertical;
        //if (mode == Mode.Vertical)
        //{
        //    if (Mathf.Abs(pos.x) > minDistance && Mathf.Abs(pos.y) > minDistance
        //        && (Mathf.Abs(pos.x) - Mathf.Abs(pos.y)) > 0)
        //    {
        //        mode = Mode.Horizontal;
        //    }
        //}
        //else
        //{
        //    if (Mathf.Abs(pos.x) > minDistance && Mathf.Abs(pos.y) > minDistance
        //        && (Mathf.Abs(pos.x) - Mathf.Abs(pos.y)) < 0)
        //    {
        //        mode = Mode.Vertical;
        //    }
        //}
    }
    public void SetPosition(Vector3 position)
    {
        Debug.Log("Set target position" + position);
        mode = (Mathf.Abs(position.x) > Mathf.Abs(position.y)) ? Mode.Horizontal : Mode.Vertical;
        transform.localPosition = position;
    }

    enum Mode
    {
        Horizontal,
        Vertical
    }
}
