using UnityEngine;

public class MoreJump : MonoBehaviour
{
    public GameObject MoreJumpObject;
    public bool MJOnOff;
    public float MJOnOffDeleyTime;

    void Start()
    {
        if (MJOnOff == false)
        {
            InvokeRepeating("Respawn", 0.0f, MJOnOffDeleyTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (PlayerController.jumpCount == 0)
            {
                PlayerController.jumpCount = 2;
                MoreJumpObject.SetActive(false);
                MJOnOff = false;
            }

            else if (PlayerController.jumpCount == 1)
            {
                PlayerController.jumpCount = 2;
                MoreJumpObject.SetActive(false);
                MJOnOff = false;
            }

            else if (PlayerController.jumpCount == 2)
            {
                PlayerController.jumpCount = 2;
                MoreJumpObject.SetActive(false);
                MJOnOff = false;
            }
        }
    }

    void Respawn()
    {
        MoreJumpObject.SetActive(true);
        MJOnOff = true;
    }
}