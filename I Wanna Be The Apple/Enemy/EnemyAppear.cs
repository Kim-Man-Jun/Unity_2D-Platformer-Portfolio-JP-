using UnityEngine;

public class EnemyAppear : MonoBehaviour
{
    public GameObject AppearEnemy;
    public GameObject AppearBlock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && AppearEnemy != null)
        {
            AppearEnemy.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && AppearBlock != null)
        {
            AppearBlock.SetActive(true);
        }
    }
}
