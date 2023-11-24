using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    PlayerController player;

    static PlayerStart Instance;

    private void Awake()
    {
        Vector3 pos = this.gameObject.transform.position;
        player = FindObjectOfType<PlayerController>();

        if (player != null)
        {
            if (DataManager.StageFirst == false)
            {
                player.transform.position = pos;
            }
        }
    }
}

