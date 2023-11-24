using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMoving : MonoBehaviour
{
    [Header("Moving State")]
    public float bossUpMax = 0;
    public float bossDownMax = 0;
    public float bossMovingSpeed = 10f;
    float currentPos;
    int movingdir;

    public bool isMoving = true;

    bossController bossController;
    GameObject Player;
    PlayerController playerController;

    void Start()
    {
        currentPos = transform.position.y;
        bossController = GetComponent<bossController>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerController = Player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (bossController.patternNum == 1 && playerController.isDead == false)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        if (isMoving)
        {
            currentPos += movingdir * bossMovingSpeed * Time.deltaTime;

            if (currentPos >= bossUpMax)
            {
                movingdir = -1;
                currentPos = bossUpMax;
            }
            else if (currentPos <= bossDownMax)
            {
                movingdir = 1;
                currentPos = bossDownMax;
            }

            transform.position = new Vector3(44, currentPos, 0);
        }
        else if (isMoving == false)
        {
            transform.position = new Vector3(44, Player.transform.position.y, 0);
        }
    }
}
