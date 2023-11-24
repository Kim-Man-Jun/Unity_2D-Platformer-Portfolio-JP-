using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int bulletDamage = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Dead")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<bossController>().Damaged(bulletDamage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
