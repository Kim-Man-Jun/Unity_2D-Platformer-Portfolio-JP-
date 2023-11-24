using UnityEngine;

public class BigCoin : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public EdgeCollider2D ec;
    public bool coinMoving = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ec = GetComponent<EdgeCollider2D>();
    }

    void Update()
    {
        if (coinMoving == true)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        else if (coinMoving == false)
        {
            rb.velocity = Vector3.zero * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stop"))
        {
            coinMoving = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
            ec.isTrigger = false;
        }
    }
}