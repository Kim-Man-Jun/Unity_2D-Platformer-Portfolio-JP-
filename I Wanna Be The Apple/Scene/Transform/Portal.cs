using UnityEngine;

public class Portal : MonoBehaviour
{
    PlayerStart playerstart;
    int playerstartnum = 0;
    public enum PortalDirection
    {
        right,
        left,
        down,
        up,
    }

    public string sceneName = "";
    public int doorNumber = 0;
    public PortalDirection direction = PortalDirection.down;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //static 변수로 스테이지 첫 진입했을 때 false일 경우 시작 지점에 계속 오게
            //true(save를 했을 때)일 경우 해당 기능을 오프시켜버릶
            DataManager.StageFirst = false;

            RoomManager.ChangeScene(sceneName, doorNumber);
        }
    }
}