using UnityEngine;


public class platform1 : MonoBehaviour
{
    private float speed = 5f;
    private Vector3 dir;
    public float currentX;
    private Rigidbody2D rb;
    private float moveDirection = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentX = transform.position.x;
    }
    private void Start()
    {
        dir = transform.up;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float currentPos = transform.position.x - currentX;

        // Перемещаем монстра
        transform.Translate(new Vector3(moveDirection * speed * Time.deltaTime, 0, 0));

        // Проверяем, достиг ли монстр предельной дистанции по X
        if (Mathf.Abs(currentPos) >= 20f)
        {
            // Меняем направление движения
            moveDirection *= -1f;
            currentX = transform.position.x; // Обновляем текущую позицию
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, что объект имеет тег "Player"
        if (collision.CompareTag("Player"))
        {
            // Делаем игрока дочерним объектом платформы
            collision.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Убираем родительскую связь, когда игрок покидает платформу
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
