using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CamMove : MonoBehaviour
{
    public GameObject player;

    void LateUpdate()
    {
        // Получаем текущую позицию камеры
        Vector3 clampedPosition = transform.position;

        // Ограничиваем координаты в пределах заданных значений
        clampedPosition.x = Mathf.Clamp(player.transform.position.x, -11, 1000);
        clampedPosition.y = Mathf.Clamp(player.transform.position.y, 9, 1000);

        // Устанавливаем новую позицию камеры
        transform.position = clampedPosition;
    }

}
