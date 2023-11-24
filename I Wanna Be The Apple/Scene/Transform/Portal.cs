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
            //static ������ �������� ù �������� �� false�� ��� ���� ������ ��� ����
            //true(save�� ���� ��)�� ��� �ش� ����� �������ѹ���
            DataManager.StageFirst = false;

            RoomManager.ChangeScene(sceneName, doorNumber);
        }
    }
}