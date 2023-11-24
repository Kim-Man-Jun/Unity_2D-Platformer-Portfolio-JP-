using UnityEngine;

public class bossBullet : MonoBehaviour
{
    Rigidbody2D rb;

    GameObject Target_Player;

    public float Speed = 10f;

    Vector2 dir;
    Vector2 dirNo;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CinemachineShake.Instance.ShakeCamera(3, 2, 0.2f);

        Target_Player = GameObject.FindGameObjectWithTag("Player");

        dir = Target_Player.transform.position - transform.position;
        dirNo = dir.normalized;

        rb.AddForce(dirNo * Speed, ForceMode2D.Impulse);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
