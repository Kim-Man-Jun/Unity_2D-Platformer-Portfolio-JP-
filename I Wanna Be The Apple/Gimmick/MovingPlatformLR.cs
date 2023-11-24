using System.Collections;
using UnityEngine;

public class MovingPlatformLR : MonoBehaviour
{
    public float MovingSpeed = 0.5f;   
    public float MovingTime;           
    public bool MovingLR;           

    void Start()
    {
        StartCoroutine("BlockMoving");
    }

    void Update()
    {
        if (MovingLR == false)
        {
            transform.Translate(-MovingSpeed * Time.deltaTime, 0, 0);
        }
        else if (MovingLR == true)
        {
            transform.Translate(MovingSpeed * Time.deltaTime, 0, 0);
        }
    }

    IEnumerator BlockMoving()
    {
        if (MovingLR == true)
        {
            MovingLR = false;
        }
        else if (MovingLR == false)
        {
            MovingLR = true;
        }
        yield return new WaitForSeconds(MovingTime);
        StartCoroutine("BlockMoving");
    }
}
