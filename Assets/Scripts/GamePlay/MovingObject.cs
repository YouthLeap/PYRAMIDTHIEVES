using UnityEngine;
using System.Collections;

public class MovingObject : Trap
{
    public Transform target;
    public Vector3 startPosition;
    public float speed = 0.2f;
    private float time = 0;

    public Rigidbody2D rb2d;
    public bool isFlying;

    public Type movingMode = Type.STRAIGHT;

    private float targetLat;

    void Start()
    {
        targetLat = target.transform.position.x;
        if (Mathf.Abs(transform.eulerAngles.y - 180) < 0.2f)
        {
            Vector3 rot = transform.eulerAngles;
            rot.y = 0;
            transform.eulerAngles = rot;
            SetFacingRight(false);
        }
    }

    void Update()
    {
        if (GameManager.GetState() == GameManager.State.PLAYING)
        {
            if (isFlying)
            {
                time += Time.deltaTime;
                switch (movingMode)
                {
                    case Type.STRAIGHT:
                        transform.position = startPosition + Mathf.Sign(transform.localScale.x) * target.localPosition * Mathf.PingPong(time * speed, 1);
                        break;
                    case Type.CIRCULAR:
                        Vector3 radius = target.localPosition / 2;
                        Vector3 offset = radius + radius.magnitude * (new Vector3(Mathf.Cos(time + Mathf.PI), Mathf.Sin(time + Mathf.PI), 0));
                        transform.position = startPosition + Mathf.Sign(transform.localScale.x) * offset;
                        break;
                }
            }
            else
            {
                Move(1.0f);
                float left = Mathf.Min(startPosition.x, targetLat);
                float right = Mathf.Max(startPosition.x, targetLat);
                if (transform.position.x < left) SetFacingRight(true);
                if (transform.position.x > right) SetFacingRight(false);
            }
        }
    }

    public void Move(float move)
    {
        rb2d.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * move * speed, rb2d.velocity.y);
    }

    public void SetFacingRight(bool isRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * ((isRight) ? 1.0f : -1.0f);
        transform.localScale = scale;
        //if ((Mathf.Abs(transform.localScale.x) == -1) == isRight)
        //{
        //    GetComponent<Animator>().SetTrigger("Flip");
        //}
    }
    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
    }

    public enum Type
    {
        NONE,
        STRAIGHT,
        CIRCULAR
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //GetComponent<Animator>().SetTrigger("Flip");
            Flip();
        }

    }
}
