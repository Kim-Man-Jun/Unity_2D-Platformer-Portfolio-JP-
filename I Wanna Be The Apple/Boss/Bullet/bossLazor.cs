using UnityEngine;

public class bossLazor : MonoBehaviour
{
    void Start()
    {
        //������ �߻� �� ī�޶� ��鸲
        CinemachineShake.Instance.ShakeCamera(5, 5, 1.3f);
        Destroy(gameObject, 1.3f);
    }
}
