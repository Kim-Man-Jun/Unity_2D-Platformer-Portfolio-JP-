using UnityEngine;
using System.IO;

public class PlayerData
{
    public Vector3 PlayerSavePos;
    public Vector3 PlayerSaveRot;
    public int PlayernowRoomSave;
}

public class DataManager : MonoBehaviour
{
    public PlayerData nowPlayer = new PlayerData();

    string Path;
    string FileName = "/SaveFile.txt";

    public static DataManager instance;

    private PlayerController thePlayer;
    private MapTransform MapTransform;

    public static bool StageFirst = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
    }
    void Start()
    {
        Path = Application.dataPath + "/Save/";

        if (!Directory.Exists(Path))
        {
            Directory.CreateDirectory(Path);
        }
    }

    public void SaveData()
    {
        StageFirst = true;
        string Data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(Path + FileName, Data);
        print(Data);
        print("저장 완료");
    }

    public void LoadData()
    {
        if (StageFirst == true)
        {
            if (File.Exists(Path + FileName))
            {
                string LoadData = File.ReadAllText(Path + FileName);
                nowPlayer = JsonUtility.FromJson<PlayerData>(LoadData);

                thePlayer = FindObjectOfType<PlayerController>();

                thePlayer.transform.position = nowPlayer.PlayerSavePos;
                thePlayer.transform.eulerAngles = nowPlayer.PlayerSaveRot;
                thePlayer.PlayerNowRoom = nowPlayer.PlayernowRoomSave;

                thePlayer.isDead = false;
                thePlayer.GetComponent<CapsuleCollider2D>().enabled = true;
                PlayerController.jumpCount = 1;

                if (MapTransform != null)
                {
                    MapTransform = FindObjectOfType<MapTransform>();
                    MapTransform.ChangeRoom();
                }
            }
        }
        else
        {
            thePlayer = FindObjectOfType<PlayerController>();

            thePlayer.isDead = false;
            thePlayer.GetComponent<CapsuleCollider2D>().enabled = true;
            PlayerController.jumpCount = 1;

            if (MapTransform != null)
            {
                MapTransform = FindObjectOfType<MapTransform>();
                MapTransform.ChangeRoom();
            }
        }
    }

    public void Saveprocedure()
    {
        thePlayer = FindObjectOfType<PlayerController>();

        nowPlayer.PlayerSavePos = thePlayer.transform.position;
        nowPlayer.PlayerSaveRot = thePlayer.transform.rotation.eulerAngles;
        nowPlayer.PlayernowRoomSave = thePlayer.PlayerNowRoom;
        SaveData();
    }
}