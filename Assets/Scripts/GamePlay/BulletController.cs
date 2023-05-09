using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private Rigidbody2D controller;
    [SerializeField]
    private float rotateSpeed = 2.0f;

    void Update()
    {
        Shoot();
    }

    public void Create(Transform bullet)
    {
        transform.position = bullet.position;
        Vector3 rot = bullet.eulerAngles;
        if (Mathf.Abs(rot.y - 180) < 2.0f)
        {
            rot.z = rot.y - rot.z;
            rot.y = 0;
        }
        transform.eulerAngles = rot;
        gameObject.SetActive(true);
    }

    public void Shoot()
    {
        controller.AddForce(transform.right * speed);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            gameObject.SetActive(false);
        }
        else if ((GameManager.GetPlayer().gameObject == other.gameObject))
        {
            other.SendMessage("Dead", Trap.Effect.DEAD);
        }
    }
}
