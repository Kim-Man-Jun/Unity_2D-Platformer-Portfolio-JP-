using UnityEngine;

public class bossLazorPre : MonoBehaviour
{
    SpriteRenderer sr;
    public GameObject bossLazorFire;

    float cooltime;

    float colora;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        colora = sr.color.a;
    }

    void Start()
    {
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        sr.color = new Color(1, 1, 1, colora);

        cooltime += Time.deltaTime;

        if (cooltime < 1f)
        {
            colora -= 0.002f;
        }
    }

    private void OnDestroy()
    {
        GameObject razor = Instantiate(bossLazorFire);
        razor.transform.position = transform.position;
    }
}
