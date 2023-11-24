using UnityEngine;

public class FlashBlock : MonoBehaviour
{
    public bool FBOnOff;
    public float FBOnOffDeleyTime;
    public float FBOnOffStartTime;
    public float FBLeftTime;
    public float FBLeftTimeBase;

    void Start()
    {
        if (FBOnOff == false)
        {
            InvokeRepeating("Respawn", FBOnOffStartTime, FBOnOffDeleyTime);
        }
    }

    void FixedUpdate()
    {
        if (FBOnOff == true)
        {
            FBLeftTime -= Time.deltaTime;
            if (FBLeftTime <= 0)
            {
                this.gameObject.SetActive(false);
                FBOnOff = false;
                FBLeftTime = FBLeftTimeBase;
            }
        }
    }

    void Respawn()
    {
        this.gameObject.SetActive(true);
        FBOnOff = true;
    }
}