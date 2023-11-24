using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisappear : MonoBehaviour
{
    public GameObject AppearEnemy;
    public GameObject AppearBlock;
    Rigidbody2D rb;

    void Start()
    {
        rb = AppearEnemy.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "Player" && AppearBlock == true)
        {
            AppearBlock.SetActive(false);
        }

        else if(collision.gameObject.tag == "Dead")
        {
            rb.gravityScale = 4f;
        }
    }
}
