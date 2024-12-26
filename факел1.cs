using UnityEngine;


public class факел1 : Monsters
{
    private float speed = 5f;
    private Vector3 dir;
    private SpriteRenderer  sprite;
    public float currentX;
    private Rigidbody2D rb;
    private float moveDirection = -1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentX = transform.position.x;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {       
        dir = transform.right;
        lives = 3;
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
}   
