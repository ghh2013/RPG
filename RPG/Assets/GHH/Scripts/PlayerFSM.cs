using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerFSM : MonoBehaviour
{
    enum PlayerState
    {
        Idle, Move, Attack, Damaged, Die
    }
    PlayerState state;

    CharacterController cc;
    Animator anim;
    
    int hp = 100;
    int att = 5;
    float speed = 5.0f;
    float attTime = 2f;
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.Idle;
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case PlayerState.Idle:
                Idle();
                break;
            case PlayerState.Move:
                Move();
                break;
            case PlayerState.Attack:
                Attack();
                break;            
            case PlayerState.Damaged:
                Damaged();
                break;
            case PlayerState.Die:
                Die();
                break;
        }
    }

    private void Idle()
    {
       
    }

    private void Move()
    {
        throw new NotImplementedException();
    }

    private void Attack()
    {
        throw new NotImplementedException();
    }

    private void Damaged()
    {
        throw new NotImplementedException();
    }

    private void Die()
    {
        throw new NotImplementedException();
    }
}