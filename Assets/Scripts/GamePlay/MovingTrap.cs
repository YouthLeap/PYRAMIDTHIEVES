using UnityEngine;
using System.Collections;

public class MovingTrap : Trap
{
    [SerializeField]
    private float distance;
    [SerializeField]
    protected Rigidbody2D rb;
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    protected int isFaceRight = 1;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    private LayerMask grounds;
    [SerializeField]
    bool isTurning = false;
    [SerializeField]
    private float startPoint;
    [SerializeField]
    private float endPoint;
    [SerializeField]
    private float frontDistance = 0.5f;
    [SerializeField]
    private float groundDistance = 1.7f;
    float isMove = 1;
    [SerializeField]
    private bool freeMove = false;
    void Start()
    {
        if (Mathf.Abs(transform.eulerAngles.y - 180) < 0.5f)
        {
            transform.eulerAngles = Vector3.zero;
            Flip();
        }
        distance = distance * Mathf.Sign(transform.localScale.x);
        startPoint = Mathf.Min(transform.position.x, transform.position.x + distance);
        endPoint = Mathf.Max(transform.position.x, transform.position.x + distance);
    }

    void Update()
    {
        if (GameManager.GetState() == GameManager.State.PLAYING) Move();
        HitCheck();
        if (!freeMove) Check();
#if UNITY_EDITOR
        Debug.DrawRay(new Vector3(startPoint, transform.position.y), Vector2.right * distance, Color.red);
#endif
    }
    public void LoadAditionalData(LevelObjectData data)
    {
        distance = data.target.x;
    }

    public void Flip()
    {
        isFaceRight = -isFaceRight;
        Vector2 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
        isTurning = false;
        animator.SetBool("Flip", false);
    }

    protected void Move()
    {
        Vector2 vel = rb.velocity;
        vel.x = Time.deltaTime * speed * isMove * isFaceRight;
        rb.velocity = vel;
    }
    float time = 0;
    protected void HitCheck()
    {
        Debug.DrawRay(transform.position, Vector2.right * isFaceRight * frontDistance, Color.green);
        Debug.DrawRay(transform.position, Vector2.down * groundDistance, Color.yellow);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * isFaceRight * frontDistance, 1.0f, grounds);
        if (hit.collider != null)
        {
            if (time == 0) Turn();
        }
        else
        {
            if (time > 0) time = 0;
        }
    }

    protected void Turn()
    {
        isTurning = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("Flip", true);
    }

    private void Check()
    {
        if (!isTurning &&
            ((transform.position.x < startPoint && isFaceRight == -1) ||
            (transform.position.x > endPoint && isFaceRight == 1)))
        {
            Turn();
        }
    }
}
