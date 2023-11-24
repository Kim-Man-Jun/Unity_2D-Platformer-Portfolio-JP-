using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

public class bossController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    float colora;
    BoxCollider2D bc;
    AudioSource aS;
    public AudioClip bossBoomSound;

    GameObject Player;
    PlayerController playerController;

    public GameObject bossSpawner;
    public GameObject goal;

    [Header("Boss Appear")]
    bool coolTimeOnOff = false;
    public float coolTimeMax = 2f;
    float coolTime;
    public bool bossDie;
    float bossDieTime;
    public ParticleSystem bossBoom;

    [Header("Boss State")]
    public Slider bossHpBar;
    public int bossHpMax = 200;
    private int bossHpMin;

    [Header("Boss Attack")]
    public GameObject bossBullet;
    public GameObject bossLazorPrecursor;
    public GameObject bossSaw;
    public Transform bossPos;

    [Header("Boss Pattern")]
    //보스 첫 등장 이후 패턴 시작
    bool bossPatternStart = false;
    //보스 패턴 변경
    bool bossPatternChange = false;
    public float patternTime = 8f;
    public int patternNum;

    #region boss pattern Time
    float pattern1_Cooltime = 0;
    float pattern1_Maxtime = 1.7f;

    float pattern2_Cooltime = 0;
    float pattern2_Maxtime = 2f;

    float pattern3_Cooltime = 0;
    float pattern3_Maxtime = 0.14f;

    float pattern4_Cooltime = 0;
    float pattern4_Maxtime = 0.15f;
    #endregion

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        colora = sr.color.a;

        aS = GetComponent<AudioSource>();

        bossHpMin = bossHpMax;
        Player = GameObject.FindGameObjectWithTag("Player");
        playerController = Player.GetComponent<PlayerController>();
    }

    void Update()
    {
        bossHpBar.value = bossHpMin;

        if (playerController.isDead == true)
        {
            return;
        }

        if (bossDie == true)
        {
            bossDieTime += Time.deltaTime;
            bossDestroy();
            GetComponent<bossMoving>().enabled = false;
            bossPatternStart = false;
        }

        //보스 첫 등장 관련 쿨타임 조절
        if (coolTimeOnOff == false)
        {
            coolTime += Time.deltaTime;

            if (coolTime > coolTimeMax)
            {
                firstStart();
            }
        }

        if (bossPatternStart == true)
        {
            //시간 경과별 패턴 변경용 코루틴
            if (bossPatternChange == false)
            {
                StartCoroutine(PatterChange());
            }

            if (patternNum == 0)
            {
                pattern1_Cooltime += Time.deltaTime;

                if (pattern1_Cooltime > pattern1_Maxtime)
                {
                    pattern1_Cooltime = 0;
                    GameObject bul1 = Instantiate(bossBullet);
                    bul1.transform.position = bossPos.position;
                    pattern1_Maxtime -= 0.1f;
                }
            }

            else if (patternNum == 1)
            {
                pattern2_Cooltime += Time.deltaTime;

                if (pattern2_Cooltime > pattern2_Maxtime)
                {
                    pattern2_Cooltime = 0;
                    GameObject bul2 = Instantiate(bossLazorPrecursor);
                    bul2.transform.position = bossPos.position;
                    pattern1_Maxtime -= 0.1f;
                }
            }

            else if (patternNum == 2)
            {
                pattern3_Cooltime += Time.deltaTime;

                if (pattern3_Cooltime > pattern3_Maxtime)
                {
                    pattern3_Cooltime = 0;
                    bossSpawner.GetComponent<bossSawSpawn>().spawn();
                }
            }

            else if (patternNum == 3)
            {
                pattern4_Cooltime += Time.deltaTime;

                if (pattern4_Cooltime > pattern4_Maxtime)
                {
                    return;
                }
            }
        }
    }

    IEnumerator PatterChange()
    {
        bossPatternChange = true;

        yield return new WaitForSeconds(patternTime);

        patternNum++;

        if (patternNum > 2)
        {
            patternNum = 0;
            pattern1_Maxtime = 1.7f;
            pattern2_Maxtime = 2f;
        }

        bossPatternChange = false;
    }

    public void Damaged(int dmg)
    {
        bossHpMin -= dmg;

        if (bossHpMin <= 0)
        {
            bossHpBar.value = 0;
            bossHpBar.enabled = false;
            goal.SetActive(true);
            bossDie = true;
        }
    }

    float waitTime = 0;

    private void bossDestroy()
    {
        if (bossDieTime <= 5f)
        {
            waitTime += Time.deltaTime;

            colora -= 0.001f;

            sr.color = new Color(1, 1, 1, colora);

            transform.Translate(new Vector3(0, -0.005f, 0));

            if (waitTime > 0.2f)
            {
                float width = bc.bounds.size.x;
                float height = bc.bounds.size.y;

                float x = UnityEngine.Random.Range(-width / 1.5f, width / 1.5f);
                float y = UnityEngine.Random.Range(-height / 1.5f, height / 1.5f);

                Vector2 particlePos = new Vector2(x + transform.position.x, y + transform.position.y);

                ParticleSystem go = Instantiate(bossBoom, particlePos, Quaternion.identity);
                aS.PlayOneShot(bossBoomSound);
                Destroy(go.gameObject, 0.3f);

                waitTime = 0;
            }
        }
        else if (bossDieTime > 5)
        {
            Destroy(gameObject);

            AudioSource audio = GameManager.instance.GetComponent<AudioSource>();

            audio.Stop();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && bossDie == false)
        {
            StartCoroutine(Damaged());
        }
    }

    IEnumerator Damaged()
    {
        sr.color = new Color(1, 0.5f, 0.5f, 0.6f);
        yield return new WaitForSeconds(0.2f);
        sr.color = new Color(1, 1, 1, 1);
    }

    private void firstStart()
    {
        if (coolTime > coolTimeMax)
        {
            gameObject.transform.position = new Vector2(44, 4f);
            rb.bodyType = RigidbodyType2D.Static;
            GetComponent<bossMoving>().enabled = true;
            coolTimeOnOff = true;
            coolTime = 0f;
            bossPatternStart = true;
        }
    }
}
