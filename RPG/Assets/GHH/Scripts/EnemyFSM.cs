using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    enum EnemyState
    {
        Idle, Move, Attack, Return, Damaged, Die
    }
    EnemyState state;

    public float FindRange = 15f;
    public float moveRange = 30f;
    public float attackRange = 2f;
    Vector3 startPoint;
    Transform player;
    CharacterController cc;
    Animator anim;
    NavMeshAgent agent;

    int hp = 100;
    int att=5;
    float speed = 5.0f;
    float attTime = 2f;
    float timer=0f;
    // Start is called before the first frame update
    void Start()
    {
        state = EnemyState.Idle;
        startPoint = transform.position;
        player = GameObject.Find("Player").transform;
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                break;
            case EnemyState.Die:
                break;
        }
    }

    private void Idle()
    {
        if(Vector3.Distance(transform.position,player.position)<FindRange)
        {
            state = EnemyState.Move;
            print("상태전환:Idle->Move");
            anim.SetTrigger("Move");
        }
    }

    private void Move()
    {
        if (!agent.enabled) agent.enabled = true;
        if(Vector3.Distance (transform.position, startPoint)>moveRange)
        {
            state = EnemyState.Return;
            print("상태전환:Move->Return");
            anim.SetTrigger("Return");
        }
        else if(Vector3.Distance(transform.position,player.position)>attackRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            state = EnemyState.Attack;
            print("상태전환:Move->Attack");
            anim.SetTrigger("Attack");
        }
    }

    private void Attack()
    {
        agent.enabled = false;
        if(Vector3.Distance(transform.position,player.position)<attackRange)
        {
            transform.LookAt(player.position);
            timer += Time.deltaTime;
            if(timer>attTime)
            {
                print("공격");
                timer = 0f;
                anim.SetTrigger("Attack");
            }
        }
        else
        {
            state = EnemyState.Move;
            print("상태전환:Attack->Move");
            timer = 0f;
            anim.SetTrigger("Move");
        }
    }

    private void Return()
    {
        if(Vector3.Distance(transform.position,startPoint)>0.1)
        {
            agent.SetDestination(startPoint);
        }
        else
        {
            transform.position = startPoint;
            transform.rotation = Quaternion.identity;
            state = EnemyState.Idle;
            print("상태전환:Return->Idle");
            anim.SetTrigger("Idle");
            agent.enabled = false;
        }
    }
    public void hitDamage(int value)
    {
        if (state == EnemyState.Damaged || state == EnemyState.Die) return;
        hp -= value;
        if(hp>0)
        {
            state = EnemyState.Damaged;
            print("상태전환:AnyState->Damaged");
            print("HP:" + hp);
            anim.SetTrigger("Damaged");
            Damaged();
        }
        else
        {
            state = EnemyState.Die;
            print("상태전환:AnyState->Die");
            anim.SetTrigger("Die");
            Die();
        }
    }

    private void Damaged()
    {
        StartCoroutine(DamageProc());
    }
    IEnumerator DamageProc()
    {
        yield return new WaitForSeconds(1.0f);
        state = EnemyState.Move;
        print("상태전환:Damaged->Move");
        anim.SetTrigger("Move");
    }

    private void Die()
    {
        StopAllCoroutines();
        StartCoroutine(DieProc());
    }
    IEnumerator DieProc()
    {
        cc.enabled = false;
        agent.enabled = false;
        yield return new WaitForSeconds(2.0f);
        print("죽었다");
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, FindRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(startPoint, moveRange);
    }
}