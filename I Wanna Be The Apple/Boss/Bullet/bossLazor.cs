using UnityEngine;

public class bossLazor : MonoBehaviour
{
    void Start()
    {
        //레이저 발사 시 카메라 흔들림
        CinemachineShake.Instance.ShakeCamera(5, 5, 1.3f);
        Destroy(gameObject, 1.3f);
    }
}
