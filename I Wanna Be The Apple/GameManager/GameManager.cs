using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameover = false;
    public GameObject gameoverUI;
    public GameObject gamerestartUI;

    public AudioClip GameOver;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()        
    {
        if (isGameover && Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            DataManager.instance.LoadData();
        }
        else if (Input.GetKey(KeyCode.R))       
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            DataManager.instance.LoadData();
        }
    }

    public void OnPlayerDead()
    {
        isGameover = true;

        AudioSource soundPlayer = GetComponent<AudioSource>();

        if (soundPlayer != null)
        {
            soundPlayer.Stop();
            soundPlayer.PlayOneShot(GameOver);
        }

        gameoverUI.SetActive(true);
        gamerestartUI.SetActive(true);
    }
}