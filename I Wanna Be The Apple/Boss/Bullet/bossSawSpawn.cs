using UnityEngine;

public class bossSawSpawn : MonoBehaviour
{
    BoxCollider2D bc;

    public GameObject Saw;

    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
    }
    public void spawn()
    {
        float width = bc.bounds.size.x;
        float height = bc.bounds.size.y;

        float x = Random.Range(-width / 2, width / 2);
        float y = Random.Range(-height / 2, height / 2);

        Vector2 spawnVec2 = new Vector2(x, y);

        Instantiate(Saw, new Vector2(transform.position.x + x, transform.position.y + y), Quaternion.identity);
        CinemachineShake.Instance.ShakeCamera(2, 2, 0.1f);
    }
}
