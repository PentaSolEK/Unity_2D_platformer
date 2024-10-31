using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOfTheGym : Monsters
{
    public Transform player; // ������ �� ������ ������
    public float agroRange = 10f; // ����������, �� ������� ������ ������� ������
    public float attackRange = 2f; // ����������, �� ������� ������ ������ ���������
    public float moveSpeed = 3f; // �������� ������������ �������
    public float attackCooldown = 2f; // ����� ����������� �����
    public Transform attackPos;
    public LayerMask hero;

    private Animator animator;
    private float lastAttackTime; // ����� ��������� �����


    public States State
    {
        get { return (States)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        lives = 15;
        lastAttackTime = -attackCooldown; // �������������, ����� ���� ��� ��������� �����
    }


    void Update()
    {
        // ��������� ���������� �� ������
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);


        // ���� ����� � �������� ���� ��������� �������
        if (distanceToPlayer <= agroRange && distanceToPlayer > attackRange)
        {
            ChasePlayer();
        }

        // ���� ���������� �� ������ ������ ��� ����� ���������� �����, ������ �������
        else if (distanceToPlayer <= attackRange)
        {
            // ���������, ������ �� �����������
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time; // ��������� ����� ��������� �����
            }
            else
            {
                // ���� ����������� �� ������, ������������ � idle
                State = States.idle;
            }
        }
        else
        {
            StopChasingPlayer();
        }

        // �������� ���������� � �������� ��� ���������� ����������
    }

    void ChasePlayer()
    {
        State = States.bossrun;
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f; // ������������� y � 0, ����� �������� �������� �� ������������ ���

        // ���������� ������� � ����������� ������
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    void StopChasingPlayer()
    {
        State = States.idle;
    }


    private void Attack()
    {
        int randomAttack = Random.Range(1, 3);
        if (randomAttack == 1) State = States.bossattack1;
        if (randomAttack == 2) State = States.bossattack2;

        // ������� � ��������� idle ����� �����
        Invoke("SetIdleState", 0.5f); // �������� ����� ��� �������� � idle � ���������
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
