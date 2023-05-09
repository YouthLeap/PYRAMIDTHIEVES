using UnityEngine;
using System.Collections;

public class TouchSelector : MonoBehaviour
{
    [SerializeField]
    private TouchPoint touchPoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPoint.Show();
        }
        if (Input.GetMouseButtonUp(0))
        {
            touchPoint.Hide();
        }
        if (Input.GetMouseButton(0))
        {
            touchPoint.SetPosition();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
