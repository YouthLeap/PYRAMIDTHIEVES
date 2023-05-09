using UnityEngine;

public class FlyingTrap : Trap
{
    public Vector3 targetDistance = Vector3.right;
    public MovingMode mode = MovingMode.STRAIGHT;
    private Vector3 startPosition;
    [SerializeField]
    private float speed = 1.0f;
    float time = 0;
    float currentPosX;

    float startAngle;
    float radius;
    Vector3 center;
    //float previousHorizontalPosition;
    bool isMoveClockwise = true;

    public void LoadAditionalData(LevelObjectData data)
    {
        targetDistance = data.target;
        mode = data.movingMode;
    }
    void Start()
    {
        startPosition = transform.position;
        sound.mute = !Attributes.soundOn;
        if (Mathf.Abs(transform.eulerAngles.y - 180) < 0.5f)
        {
            transform.eulerAngles = Vector3.zero;
            Vector3 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;
        }
        
        if (targetDistance.x != 0 && mode == MovingMode.STRAIGHT) targetDistance = targetDistance * Mathf.Sign(transform.localScale.x);
        if (mode == MovingMode.CIRCULAR)
        {
            targetDistance.x *= Mathf.Sign(transform.localScale.x);
            startAngle = Vector2.Angle(Vector2.right, -targetDistance) * Mathf.Deg2Rad;
            if (targetDistance.y > 0.5f) startAngle = -startAngle;
            center = targetDistance / 2;
            radius = center.magnitude;
            isMoveClockwise = (transform.localScale.x > 0) ^ targetDistance.y > 0;
        }
        currentPosX = startPosition.x;
        if (haveSound && SoundManager.instance != null)
        {
            SoundManager.instance.AddSource(GetComponent<AudioSource>());
        }
    }
    void Update()
    {
        if (GameManager.GetState() == GameManager.State.PLAYING)
        {
            time += Time.deltaTime;
            switch (mode)
            {
                case MovingMode.STRAIGHT:
                    transform.position = startPosition + targetDistance * Mathf.PingPong(time * speed / targetDistance.magnitude, 1);
                    //if (Mathf.Abs(targetDistance.x) > 0)
                    //{
                    //    float dir = transform.position.x - currentPosX;
                    //    currentPosX = transform.position.x;
                    //    Vector3 scale = transform.localScale;
                    //    scale.x = Mathf.Sign(dir);
                    //    transform.localScale = scale;
                    //}
                    break;
                case MovingMode.CIRCULAR:
                    float angle = isMoveClockwise ? Mathf.PI * 2 - time + startAngle : time + startAngle;
                    Vector3 offset = center + radius * (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0));
                    transform.position = startPosition + offset;
                    //Vector3 scale2 = transform.localScale;
                    //scale2.x = previousHorizontalPosition < transform.position.x ? 1 : -1;
                    //transform.localScale = scale2;
                    //previousHorizontalPosition = transform.position.x;
                    break;
            }
            if (mode == MovingMode.CIRCULAR || Mathf.Abs(targetDistance.x) > 0)
            {
                float dir = transform.position.x - currentPosX;
                currentPosX = transform.position.x;
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Sign(dir);
                transform.localScale = scale;
            }
        }
        CheckSFXPlay();
    }

    public enum MovingMode
    {
        STRAIGHT,
        CIRCULAR
    }
}
