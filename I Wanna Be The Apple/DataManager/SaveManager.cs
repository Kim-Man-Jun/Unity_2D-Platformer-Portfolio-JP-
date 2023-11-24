using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip saveSound;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            DataManager dataManager = FindObjectOfType<DataManager>();
            dataManager.Saveprocedure();

            audio.PlayOneShot(saveSound);
        }
    }
}