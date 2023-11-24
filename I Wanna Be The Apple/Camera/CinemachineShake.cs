using Cinemachine;
using UnityEngine;

//cinemachine 카메라 흔들림 관련
public class CinemachineShake : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private float shakeTimer;
    private float startingamplitude;
    private float startingfrequency;

    public static CinemachineShake Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float amplitude, float frequency, float time)
    {
        CinemachineBasicMultiChannelPerlin perlinCamera =
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        perlinCamera.m_AmplitudeGain = amplitude;
        perlinCamera.m_FrequencyGain = frequency;
        shakeTimer = time;

        startingamplitude = amplitude;
        startingfrequency = frequency;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
        }

        if (shakeTimer <= 0)
        {
            CinemachineBasicMultiChannelPerlin perlinCamera =
    virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            perlinCamera.m_AmplitudeGain = 0;
            perlinCamera.m_FrequencyGain = 0;
        }
    }
}
