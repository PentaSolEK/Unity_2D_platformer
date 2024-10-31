using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CamMove : MonoBehaviour
{
    public GameObject player;

    void LateUpdate()
    {
        // �������� ������� ������� ������
        Vector3 clampedPosition = transform.position;

        // ������������ ���������� � �������� �������� ��������
        clampedPosition.x = Mathf.Clamp(player.transform.position.x, -11, 1000);
        clampedPosition.y = Mathf.Clamp(player.transform.position.y, 9, 1000);

        // ������������� ����� ������� ������
        transform.position = clampedPosition;
    }

}
