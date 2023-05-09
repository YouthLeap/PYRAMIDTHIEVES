using UnityEngine;
public class ShootingTrap : Item
{
    [SerializeField]
    private Transform bulletPosition;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    float speed = 1;
    [SerializeField]
    private Transform leftCheck;
    [SerializeField]
    private Transform rightCheck;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private float touchRadius = 0.01f;

    bool isTouchLeft {
        get
        {
            bool isTouch = false;
            Collider2D[] touchs = Physics2D.OverlapCircleAll(leftCheck.position, touchRadius, whatIsGround);
            for (int i = 0; i < touchs.Length; i++)
            {
                if (touchs[i].gameObject != gameObject)
                    isTouch = true;
            }
            return isTouch;
        }
    }

    bool isToucgRight
    {
        get
        {
            bool isTouch = false;
            Collider2D[] touchs = Physics2D.OverlapCircleAll(rightCheck.position, touchRadius, whatIsGround);
            for (int i = 0; i < touchs.Length; i++)
            {
                if (touchs[i].gameObject != gameObject)
                    isTouch = true;
            }
            return isTouch;
        }
    }


    private bool isFaceRight
    {
        get
        {
            return transform.localScale.x < 0 ^ Mathf.Abs(transform.eulerAngles.y) > 179;
        }
    }
    ObjectPool<BulletController> bulletPool;
    int poolSize = 3;
    [SerializeField]
    float angleOffset = -24;

    void Awake()
    {
        //isFaceRight = transform.localScale.x < 0 ^ Mathf.Abs(transform.eulerAngles.y) > 179;
    }

    void Update()
    {
        if (GameManager.GetState() == GameManager.State.PLAYING) LockOnTarget();
    }

    protected override void Init()
    {
        base.Init();
        bulletPool = new ObjectPool<BulletController>(poolSize, bulletPrefab);
    }

    public override void AnimationEvent()
    {
        BulletController bullet = bulletPool.Get();
        bullet.Create(bulletPosition);
    }

    private void LockOnTarget()
    {
        Vector3 target = GameManager.GetPlayer().transform.position;
        if (transform.position.y < target.y) return;
        Vector3 direction = target - transform.position;
        Vector3 rotation = transform.eulerAngles;
        //float angle;
        float angle = 0;
        if (isFaceRight)
        {
            angle = -Mathf.Atan(-direction.x / direction.y) * Mathf.Rad2Deg - 90;
        }
        else
        {
            angle = Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg + 180 * (direction.x > 0 ? 0 : 1) - transform.eulerAngles.y / 2;
        }
        float targetAngle = Mathf.LerpAngle(rotation.z, angle - angleOffset, Time.deltaTime * speed);
        bool moveLeft = targetAngle < rotation.z;
        if (moveLeft && isTouchLeft) targetAngle = rotation.z;
        if (!moveLeft && isToucgRight) targetAngle = rotation.z;
        rotation.z = targetAngle;
        transform.eulerAngles = rotation;
    }
}