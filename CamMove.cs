using UnityEngine;

public class CamMove : MonoBehaviour
{
    public GameObject player;

    void LateUpdate()
    {
        // Получаем текущую позицию камеры
        Vector3 clampedPosition = transform.position;

        // Ограничиваем координаты в пределах заданных значений
        clampedPosition.x = Mathf.Clamp(player.transform.position.x, -11, 1000);
        clampedPosition.y = Mathf.Clamp(player.transform.position.y, -10, 1000) + 3;

        // Устанавливаем новую позицию камеры
        transform.position = clampedPosition;
    }

}
