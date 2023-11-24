using UnityEngine;

public class TouchDisappear : MonoBehaviour
{
    public GameObject DisappearObject;
    public GameObject DisappearObject2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            DisappearObject.SetActive(false);
            DisappearObject2.SetActive(false);
        }
    }
}
