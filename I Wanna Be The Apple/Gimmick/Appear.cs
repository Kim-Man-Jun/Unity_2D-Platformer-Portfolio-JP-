using UnityEngine;

public class Appear : MonoBehaviour
{
    public GameObject BigCoin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BigCoin.SetActive(true);
        }
    }
}
