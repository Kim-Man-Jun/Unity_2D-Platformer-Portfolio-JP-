using UnityEngine;

public class bossSaw : MonoBehaviour
{
    Rigidbody2D rb;

    float coolTime;
    public float coolTimeMax;

    float randomDir;
    float jumpPower = 1.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 7f);
    }

    void Update()
    {
        coolTime += Time.deltaTime;

        if (coolTime >= coolTimeMax)
        {
            randomDir = Random.Range(-1.5f, 1.5f);
            rb.AddForce(new Vector2(randomDir, jumpPower), ForceMode2D.Impulse);
            
            coolTime = 0;
        }
    }
}
