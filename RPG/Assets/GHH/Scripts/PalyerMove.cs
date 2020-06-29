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
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
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
