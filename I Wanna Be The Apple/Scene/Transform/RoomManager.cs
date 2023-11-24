using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Portal;

public class RoomManager : MonoBehaviour
{
    public static int doorNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] enters = GameObject.FindGameObjectsWithTag("Start");
        for (int i = 0; i < enters.Length; i++)
        {
            GameObject doorObj = enters[i];
            Portal portal = doorObj.GetComponent<Portal>();
            if (doorNumber == portal.doorNumber)
            {
                float x = doorObj.transform.position.x;
                float y = doorObj.transform.position.y;

                if (portal.direction == PortalDirection.up)
                {
                    y += 1;
                }
                else if (portal.direction == PortalDirection.down)
                {
                    y -= 1;
                }
                else if (portal.direction == PortalDirection.left)
                {
                    x -= 1;
                }
                else if (portal.direction == PortalDirection.right)
                {
                    x += 1;
                }
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = new Vector3(x, y, 0);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void ChangeScene(string scenename, int doornum)
    {
        doorNumber = doornum;
        SceneManager.LoadScene(scenename);
    }
}
