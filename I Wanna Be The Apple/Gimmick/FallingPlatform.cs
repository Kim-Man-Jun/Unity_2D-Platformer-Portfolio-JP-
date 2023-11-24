using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool chartouch = false;
    public float RemainTime;
    float curTime;

    Rigidbody2D rbody;
    BoxCollider2D bCol;

    private float waitTime = 0.3f;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
        bCol = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (chartouch == true)
        {
            curTime += Time.deltaTime;
        }
    }

    private void OnCollisionStay2D(Collision2D Player)
    {
        if (chartouch == false)
        {
            chartouch = true;
        }

        if (RemainTime <= curTime)
        {
            bCol.isTrigger = true;
            rbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            chartouch = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BlockStop")
        {
            waitTime -= Time.deltaTime;

            bCol.isTrigger = false;

            if (waitTime < 0)
            {
                rbody.bodyType = RigidbodyType2D.Static;
                rbody.gravityScale = 0;
            }
        }
    }
}