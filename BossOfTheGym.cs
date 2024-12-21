using UnityEngine;

public class BossOfTheGym : Monsters
{
    public Transform player; // Ссылка на объект игрока
    public float agroRange = 10f; // Расстояние, на котором монстр заметит игрока
    public float attackRange = 5f; // Расстояние, на котором монстр начнет атаковать
    public float moveSpeed = 3f; // Скорость передвижения монстра
    public float attackCooldown = 0.5f; // Время перезарядки атаки
    public Transform attackPos;
    public LayerMask hero;

    private Animator animator;
    private float lastAttackTime; // Время последней атаки


    public States State
    {
        get { return (States)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        lives = 15;
        lastAttackTime = -attackCooldown; // Инициализация, чтобы босс мог атаковать сразу
    }


    void Update()
    {
        // Проверяем расстояние до игрока
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Если игрок в пределах зоны видимости монстра
        if (distanceToPlayer <= agroRange && distanceToPlayer > attackRange)
        {
            ChasePlayer();
        }

        // Если расстояние до игрока меньше или равно расстоянию атаки, монстр атакует
        else if (distanceToPlayer <= attackRange)
        {
            // Проверяем, прошла ли перезарядка
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time; // Обновляем время последней атаки
            }
            else
            {
                // Если перезарядка не прошла, возвращаемся в idle
                State = States.idle;
            }
        }
        else
        {
            StopChasingPlayer();
        }

        // Передаем информацию в аниматор для управления анимациями
    }

    void ChasePlayer()
    {
        State = States.bossrun;
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f; // Устанавливаем y в 0, чтобы избежать вращения по вертикальной оси

        // Перемещаем монстра в направлении игрока
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    void StopChasingPlayer()
    {
        State = States.idle;
    }


    private void Attack()
    {
        int randomAttack = Random.Range(1, 3);
        switch (randomAttack)
        {
            case 1:
                State = States.bossattack1;
                break;
            case 2:
                State = States.bossattack2;
                break;
        }
        // Переход в состояние idle после атаки
        Invoke("SetIdleState", 0.5f); // Вызываем метод для перехода в idle с задержкой // Вызываем метод для перехода в idle с задержкой
    }

    private void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, hero);

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Hero>().GetDamage();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public enum States
    {
        idle,
        bossrun,
        bossattack1,
        bossattack2
    }
}
