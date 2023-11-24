using UnityEngine;

public class GimmickObject : MonoBehaviour
{
    public float length = 0.0f;
    public Vector2 PlayerVector;
    public Vector2 myselfVector;
    public float DestroyTime;

    void Start()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;            //물리 현상 고정, inspector에서 static으로 해야함
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerVector = player.GetComponent<Transform>().position;
        myselfVector = GetComponent<Transform>().position;

        if (player != null)
        {
            float Angle = GetAngle(PlayerVector, myselfVector);

            if (Angle >= 70 && Angle <= 110)
            {
                float d = Vector2.Distance(transform.position, player.transform.position);

                if (length >= d)
                {
                    Rigidbody2D rbody = GetComponent<Rigidbody2D>();

                    if (rbody.bodyType == RigidbodyType2D.Static)
                    {
                        rbody.bodyType = RigidbodyType2D.Dynamic;

                        Destroy(this.gameObject, DestroyTime);
                    }
                }
            }
        }
    }

    public static float GetAngle(Vector2 vStart, Vector2 vEnd)
    {
        Vector2 v = vEnd - vStart;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
}