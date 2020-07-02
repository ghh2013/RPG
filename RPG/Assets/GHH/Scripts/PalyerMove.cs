using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalyerMove : MonoBehaviour
{
    public float speed = 5.0f;
    CharacterController cc;

    public float gravity = -20;
    float velocityY;
    float jumpPower = 10;
    int jumpcount = 0;

    float h, v;

    Animator anim;

    int hp = 100;
    int att = 5;
    
    float attTime = 2f;
    float timer = 0f;

    bool move = false;
    bool attack = false;
    bool die = false;
    bool damaged = false;
    bool grenade = false;
    bool jump = false;
    
    // Start is called before the first frame update
    void Start()
    {       
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (v != 0)
        {
            move = true;
        }
        else
        {
            move = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            grenade = true;
        }
        else
        {
            grenade = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
        AnimationUpdate();
        Move();
    }


    private void AnimationUpdate()
    {
        if (move == true)
        {
            anim.SetBool("move", true);
        }
        else
        {
            anim.SetBool("move", false);
        }
        if (attack == true)
        {
            anim.SetBool("attack", true);
        }
        else
        {
            anim.SetBool("attack", false);
        }
        if (damaged == true)
        {
            anim.SetBool("damaged", true);
        }
        else
        {
            anim.SetBool("damaged", false);
        }
        if (die == true)
        {
            anim.SetBool("die", true);
        }
        else
        {
            anim.SetBool("die", false);
        }
        if (grenade == true)
        {
            anim.SetBool("grenade", true);
        }
        else
        {
            anim.SetBool("grenade", false);
        }
        if (jump == true)
        {
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }
    }

    private void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);

        dir = Camera.main.transform.TransformDirection(dir);

        if(cc.collisionFlags==CollisionFlags.Below)
        {
            velocityY = 0;
            jumpcount = 0;
        }
        else
        {
            velocityY += gravity * Time.deltaTime;
            dir.y = velocityY;
        }
        if(Input.GetButtonDown("Jump")&&jumpcount<2)
        {
            jumpcount++;
            velocityY = jumpPower;
        }
        cc.Move(dir * speed * Time.deltaTime);
    }
}
