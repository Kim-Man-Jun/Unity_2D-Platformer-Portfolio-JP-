using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public Rigidbody2D BulletPrefab;
    public Transform firePos;

    public float cooltime = 0.25f;
    public float curtime = 0;
    public float BulletSpeed = 30.0f;

    public Vector2 PlayerVector;
    public Vector2 FireposVector;

    private AudioSource AudioSource;
    public AudioClip Gun;

    GameObject Player;
    PlayerController playerController;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerController = Player.GetComponent<PlayerController>();
    }

    void Update()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerVector = player.GetComponent<Transform>().position;
        FireposVector = GetComponent<Transform>().position;

        float angle = GetAngle(PlayerVector, FireposVector);

        if (curtime <= 0)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                if (playerController.isDead == true)
                {
                    return;
                }

                if (angle <= -150 && angle >= -170)
                {
                    Rigidbody2D BulletPre = Instantiate(BulletPrefab, firePos.position, Quaternion.identity);
                    BulletPre.velocity = BulletSpeed * firePos.transform.right * -1;
                    AudioSource.PlayOneShot(Gun);
                }
                else if (angle <= 0 && angle >= -20)
                {
                    Rigidbody2D BulletPre = Instantiate(BulletPrefab, firePos.position, Quaternion.identity);
                    BulletPre.velocity = BulletSpeed * firePos.transform.right;
                    AudioSource.PlayOneShot(Gun);
                }
            }
            curtime = cooltime;
        }
        curtime -= Time.deltaTime;
    }

    public static float GetAngle(Vector2 vStart, Vector2 vEnd)
    {
        Vector2 v = vEnd - vStart;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
}