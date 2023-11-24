using System.Collections;
using UnityEngine;

public class MovingPlatformUD : MonoBehaviour
{
    public float MovingSpeed = 0.5f;
    public float MovingTime;
    public bool MovingUD;

    void Start()
    {
        StartCoroutine("BlockMoving");
    }

    void Update()
    {
        if (MovingUD == false)
        {
            transform.Translate(0, MovingSpeed * Time.deltaTime, 0);
        }
        else if (MovingUD == true)
        {
            transform.Translate(0, -MovingSpeed * Time.deltaTime, 0);
        }
    }

    IEnumerator BlockMoving()
    {
        if (MovingUD == true)
        {
            MovingUD = false;
        }
        else if (MovingUD == false)
        {
            MovingUD = true;
        }
        yield return new WaitForSeconds(MovingTime);
        StartCoroutine("BlockMoving");
    }
}
