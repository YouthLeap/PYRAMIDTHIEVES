using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public bool isFacingRight = true;
    [SerializeField]
    public float speed = 10f;
    [SerializeField]
    private float jumpForce = 400f;
    [SerializeField]
    private float wallJumpForce = 250f;
    [SerializeField]
    private float wallFallSpeed = 2;
    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    bool isGround = true;
    [SerializeField]
    bool isTouchWall = false;

    Vector2 wallJumpDirection = new Vector2(-0.5f, 1.2f);
    Vector2 jumpDirection = new Vector2(0.0f, 1.0f);

    public const int NORMAL_DEAD = 0;
    public const int BURNING_DEAD = 1;

    [SerializeField]
    private Vector3 velocity;
    
    public State playerState;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform frontCheck;

    private float groundRadius = 0.12f;

    [SerializeField]
    private float slideSpeed = 1.25f;

    [SerializeField]
    private AudioSource sfx;
    [SerializeField]
    private AudioClip jumpSFX;

    private int sign
    {
        get
        {
            return isFacingRight ? 1 : -1;
        }
    }

    void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.AddStartGameEvent(name, OnGameStart);
        }
        CheckTouchGround();
    }
    float move = 1.0f;
    void Update()
    {
        CheckState();
        //TestFunction();
    }

    void FixedUpdate()
    {
        //CheckTouchGround();
        anim.SetFloat(AnimationState.Up, rb2d.velocity.y);
    }

    private void CheckState()
    {
        switch (playerState)
        {
            case State.GAME_OVER:
                if (isGround) SetState(State.PAUSE);
                break;
            case State.PLAY:
                Move(1.0f);
                CheckControl();
                CheckTouchGround();
                if (isTouchWall && !isGround && rb2d.velocity.y < 0)
                {
                    Vector3 velocity = rb2d.velocity;
                    velocity.y = -slideSpeed;
                    rb2d.velocity = velocity;
                }
                break;
        }
    }

    private void Pause(bool isPause = true)
    {
        if (isPause)
        {
            velocity = rb2d.velocity;
            velocity = Vector3.zero;
        }
        else
        {
            rb2d.velocity = velocity;
        }
        rb2d.isKinematic = isPause;
        anim.enabled = !isPause;
    }

    public void CheckControl()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
            if (GameManager.GetState() == GameManager.State.PLAYING) Jump();
        }
#else
        if (Input.touchCount > 0)
        {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Jump();
            }
        }
#endif
    }

    private void OnGameStart()
    {
        playerState = State.PLAY;
    }

    public void Move(float move)
    {
        rb2d.velocity = new Vector2(sign * move * speed, rb2d.velocity.y);
    }

    public void Jump()
    {
        if (isGround)
        {
            if (Attributes.soundOn) sfx.PlayOneShot(jumpSFX);
            if (!isTouchWall) anim.SetTrigger(AnimationState.JumpTrigger);
            float force = isTouchWall ? wallJumpForce : jumpForce;
            if (rb2d.velocity.y < 0) rb2d.velocity = Vector3.zero;
            rb2d.AddForce(force * Vector2.up);
        }
        else if (isTouchWall)
        {
            if (Attributes.soundOn) sfx.PlayOneShot(jumpSFX);
            anim.SetTrigger(AnimationState.JumpTrigger);
            Flip();
            rb2d.velocity = Vector2.zero;
            anim.SetBool(AnimationState.WallSlide, false);
            rb2d.AddForce(jumpForce * wallJumpDirection);
        }
        //if (isTouchWall && isGround) move = 0;
    }
    private void CheckTouchGround()
    {
        isTouchWall = false;
        Collider2D[] frontCols = Physics2D.OverlapCircleAll(frontCheck.position, groundRadius, whatIsGround);
        for (int i = 0; i < frontCols.Length; i++)
        {
            if (frontCols[i].gameObject != gameObject)
                isTouchWall = true;
        }

        anim.SetBool(AnimationState.IsTouchWall, isTouchWall);
        //RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, whatIsGround);
        //isGround = hitGround.collider != null;
        isGround = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                isGround = true;
        }
        anim.SetBool(AnimationState.IsGround, isGround);

        if (isTouchWall && !isGround && rb2d.velocity.y == 0)
        {
            rb2d.velocity = new Vector2(-0.02f, 0.0f);
        }
    }

    public void Flip()
    {
        move = 1;
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x = sign;
        transform.localScale = scale;
    }

    public class AnimationState
    {
        public const string JumpTrigger = "Jump";
        public const string WallSlide = "WallSlide"; // Bool
        public const string IsGround = "IsGround";
        public const string Dead = "Dead";
        public const string Burn = "Burn";
        public const string IsTouchWall = "IsTouchWall";
        public const string Up = "Up"; // Float
        public const string DistanceToWall = "DistanceToWall"; // Float
    }

    public void Dead(int cause = NORMAL_DEAD)
    {
        //rb2d.isKinematic = true;
        rb2d.velocity = Vector3.zero;
        playerState = State.DEAD;
        switch (cause)
        {
            case NORMAL_DEAD:
                anim.SetBool(AnimationState.Dead, true);
                break;
            case BURNING_DEAD:
                anim.SetBool(AnimationState.Burn, true);
                break;
        }
    }

    public void OnAnimationEnd(string state)
    {
        switch (state)
        {
            case AnimationState.Dead:
            case AnimationState.Burn:
                gameObject.SetActive(false);
                GameManager.instance.SetState(GameManager.State.GAMEOVER);
                GameManager.instance.FinishLevel(false);
                break;
        }
    }

    public void Dead(Trap.Effect effect)
    {
        if (playerState == State.DEAD) return;
        rb2d.velocity = Vector3.zero;
        playerState = State.DEAD;
        switch (effect)
        {
            case Trap.Effect.DEAD:
                anim.SetTrigger(AnimationState.Dead);
                break;
            case Trap.Effect.BURN:
                anim.SetTrigger(AnimationState.Burn);
                break;
        }
    }

    public enum State
    {
        PLAY,
        PAUSE,
        DEAD,
        GAME_OVER
    }

    public void SetState(State state)
    {
        playerState = state;
        switch (state)
        {
            case State.PAUSE:
                Pause();
                break;
            case State.DEAD:
                break;
            case State.GAME_OVER:
                Pause();
                break;
            case State.PLAY:
                Pause(false);
                break;
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
