using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Hero : MonoBehaviour 
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private double lives = 5;
    [SerializeField] private float jumpForce = 50f;
    [SerializeField] private ContactFilter2D platform;

    //Создаем массив спрайтов для спрайтов количества жизней
    [SerializeField] private Sprite[] health;

    private bool IsGrounded => rb.IsTouching(platform);

    public bool isAttacking = false;
    public bool isRecharged = true;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemy;
    public GameObject Diescreen;
    public GameObject lvlup;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    public SpriteRenderer hpsprite;

    //Переменная состояний для анимации
    public States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

    //Считываем спрайты, анимации и тд
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        isRecharged = true;

    }
    
    private void Update()
    {
        //каждый кадр спрайт количества сердечек присваивается в зависимости от количества жизней
        hpsprite.sprite = health[(int)lives];

        //Обработка бездействия
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && !Input.GetButton("Fire1")) State = States.idle;

        //Обработка бега
        if (IsGrounded && Input.GetButton("Horizontal") && !Input.GetButtonDown("Jump"))
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            Run(horizontalInput); ;
        }

        // Обработка прыжка
        if (IsGrounded && Input.GetButtonDown("Jump") && !Input.GetButton("Horizontal"))
        {
            Jump();
        }

        //Обработка прыжка в движении
        if (IsGrounded && Input.GetButtonDown("Jump") && Input.GetButton("Horizontal"))
        {
            walkjump();
        }

        //Обработка атаки

        if (Input.GetButtonDown("Fire1") && !Input.GetButton("Horizontal"))
        {
            Attack();
        }

    }
    
    //Бег
    private void Run(float horizontalInput)
    {
        if (IsGrounded) State = States.run;

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        sprite.flipX = dir.x < 0.0f;


    }

    // ПРЫЖКИ
    private void Jump()
    {
        State = States.jump;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    
    private void walkjump()
    {
        State = States.walkjump;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    // Атака
    private void Attack()
    {
        if (isRecharged)
        {
            State = States.attack;
            isAttacking = true;
            isRecharged = false;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCooldown());
        }
    }

    private void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

        for(int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Monsters>().GetDamage();
        }
    }

    //Функция получения урона
    public virtual void GetDamage()
    {
        lives -= 0.5;
        if (lives < 1)
        {
            Die();
        }
    }

    //Функция смерти
    public virtual void Die()
    {
        Diescreen.SetActive(true);
        Time.timeScale = 0f;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Уменьшаем количество жизней игрока при встрече с врагом
            GetDamage();

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Respawn")
        {
            Die();
        }
    }


    //функция, рисующая вспомогательный круг, отображающий текущую дальность атаки
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    //корутины для корректной работы атаки
    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        isRecharged = true;
    }

    public enum States
    {
        idle,
        run,
        attack,
        jump,
        walkjump
    }
}


