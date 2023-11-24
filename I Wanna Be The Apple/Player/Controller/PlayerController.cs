using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move Info")]
    float axisH = 0.0f;
    public float speed = 5.0f;

    [Header("Jump Info")]
    public float jumpForce = 700f;
    public static int jumpCount = 0;
    public float playerDeadAddForce = 0.0f;

    [Header("ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundLayer;

    [Header("treadable Check")]
    [SerializeField] private Transform treadableCheck;
    [SerializeField] private float treadableDistance;
    [SerializeField] private LayerMask treadableLayer;

    public bool isDead = false;

    string nowAnima;
    string oldAnima;

    Rigidbody2D rb;
    Animator anima;
    SpriteRenderer sr;

    private AudioSource sd;
    public AudioClip playerjump;
    public AudioClip playerdoublejump;

    static PlayerController Instance;

    public int PlayerNowRoom = 0;

    public float secondJump;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        sd = GetComponent<AudioSource>();

        nowAnima = "Idle";
        oldAnima = "Idle";
    }

    private void Update()
    {
        if (isDead) return;

        PlayerMovement();
        AnimationController();
    }

    public void PlayerMovement()
    {
        axisH = Input.GetAxisRaw("Horizontal");

        if (axisH != 0)
        {
            if (axisH > 0.0f)
            {
                transform.localScale = new Vector2(1, 1);
                rb.velocity = new Vector2(speed * axisH, rb.velocity.y);
            }
            else if (axisH < 0.0f)
            {
                transform.localScale = new Vector2(-1, 1);
                rb.velocity = new Vector2(speed * axisH, rb.velocity.y);
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (jumpCount == 0 && (isGroundDetected() == true || istreadableDetected() == true))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(0, jumpForce));
                sd.PlayOneShot(playerjump);
            }
            else if (jumpCount == 1)
            {
                rb.velocity = Vector2.zero;
                sd.PlayOneShot(playerdoublejump);
                rb.velocity = new Vector2(0, jumpForce * secondJump);
                jumpCount = 0;
            }
            else if (jumpCount == 2)
            {
                rb.velocity = Vector2.zero;
                sd.PlayOneShot(playerdoublejump);
                rb.velocity = new Vector2(0, jumpForce * secondJump);
                jumpCount = 0;
            }
        }
    }

    public void AnimationController()
    {
        if (isGroundDetected() || istreadableDetected())
        {
            if (axisH == 0)
            {
                nowAnima = "Idle";
            }

            else if (axisH != 0)
            {
                nowAnima = "Run";
            }
        }

        else if (isGroundDetected() == false || istreadableDetected() == false)
        {
            nowAnima = "Jump";
            anima.SetFloat("yVelocity", rb.velocity.y);

            if (Input.GetKeyDown(KeyCode.X))
            {
                nowAnima = "DoubleJump";
                anima.SetFloat("DouyVelocity", rb.velocity.y);
            }
        }

        if (nowAnima != oldAnima)
        {
            anima.SetBool(oldAnima, false);
            oldAnima = nowAnima;
            anima.SetBool(nowAnima, true);
        }
    }

    public void Die()
    {
        if (isDead == true)
        {
            nowAnima = "Jump";
            anima.SetBool(nowAnima, true);

            rb.velocity = Vector2.zero;
            GetComponent<CapsuleCollider2D>().enabled = false;

            rb.AddForce(new Vector2(0, playerDeadAddForce), ForceMode2D.Impulse);

            GameManager.instance.OnPlayerDead();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Dead" || other.tag == "Boss" || other.tag == "BossBullet")
            && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Treadable")
        {
            jumpCount = 1;
        }
    }

    public bool isGroundDetected() => Physics2D.Raycast(groundCheck.position,
        Vector2.down, groundDistance, groundLayer);
    public bool istreadableDetected() => Physics2D.Raycast(treadableCheck.position,
        Vector2.down, treadableDistance, treadableLayer);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x,
            groundCheck.position.y - groundDistance));
        Gizmos.color = Color.red;

        Gizmos.DrawLine(treadableCheck.position, new Vector3(treadableCheck.position.x,
            treadableCheck.position.y - treadableDistance));
        Gizmos.color = Color.yellow;
    }
}