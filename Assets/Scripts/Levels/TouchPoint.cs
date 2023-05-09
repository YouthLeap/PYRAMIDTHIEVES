using UnityEngine;
using System.Collections;

public class TouchPoint : MonoBehaviour {
    bool isShow = false;
    float time = 0;
    bool isTouchObject = false;
    string selectedObject;
    void Update()
    {
        //
    }
    public void Show()
    {
        isShow = true;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        isShow = false;
        gameObject.SetActive(false);
    }

    public void SetPosition()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 10;
        transform.position = Camera.main.ScreenToWorldPoint(pos);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTouchObject && other.GetComponent<LevelObjectController>() != null)
        {
            isTouchObject = true;
            selectedObject = other.name;
            Debug.Log(other.name);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (selectedObject == other.name)
        {
            isTouchObject = false;
        }
    }

    void OnDisable()
    {
        Debug.Log("Dis");
    }
}
