using UnityEngine;


public class platform3 : MonoBehaviour
{
    private float speed = 5f;
    private Vector3 dir;
    public float currentX;
    public float currentY;
    private Rigidbody2D rb;
    private float moveDirectionX = 1f;
    private float moveDirectionY = -1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentX = transform.position.x;
        currentY = transform.position.y;
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
        transform.Translate(new Vector3(moveDirectionX * speed * Time.deltaTime, moveDirectionY * speed * Time.deltaTime, 0));

        // Проверяем, достиг ли монстр предельной дистанции по X
        if (Mathf.Abs(currentPos) >= 20f)
        {
            // Меняем направление движения
            moveDirectionX *= -1f;
            moveDirectionY *= -1f;
            // Обновляем текущую позицию
            currentX = transform.position.x; 
            currentY = transform.position.y; 
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
