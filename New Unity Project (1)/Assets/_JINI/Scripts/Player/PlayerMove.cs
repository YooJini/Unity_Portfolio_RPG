using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 3.0f;      //이동 속도
  
    CharacterController cc;

    Animator mAvatar;

    public VariableJoystick joyStick;

    #region 점프 관련 변수
    float jumpSpeed = 1.0f;     //점프 스피드
    public int jumpCount = 0;   
    public float gr = -3;       //중력
    public float velocityY;            //낙하속도 (velocity는 방향과 힘을 가짐)
    #endregion

    Vector3 Reposition;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        mAvatar = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      if(!GetComponent<PlayerAttack>().isAttack)
        Move();

     
    }

    private void FixedUpdate()
    {
        if (cc.collisionFlags == CollisionFlags.Below && velocityY < jumpSpeed)
        {
            Reposition = transform.position;
        }
            if (transform.position.y < -10)
        {
            transform.position = Reposition;
        }
    }
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        //조이스틱
        if (h == 0 && v == 0)   
        {
            mAvatar.SetFloat("Speed", joyStick.Horizontal * joyStick.Horizontal + joyStick.Vertical * joyStick.Vertical);
      
            if (joyStick.Vertical != 0 || joyStick.Horizontal != 0)
            {
                cc.Move(transform.forward * speed * Time.deltaTime);
                this.transform.rotation = Quaternion.Euler(0, Mathf.Atan2(joyStick.Horizontal, joyStick.Vertical) * Mathf.Rad2Deg, 0);
      
            }
        }
        //키보드
        else
        {
           cc.Move(transform.forward * speed * Time.deltaTime);
           this.transform.rotation=Quaternion.Euler(0, Mathf.Atan2(h, v) * Mathf.Rad2Deg, 0);
       
           mAvatar.SetFloat("Speed", h * h + v * v);
        }

       //땅에 닿았냐
        if(cc.collisionFlags==CollisionFlags.Below &&velocityY<jumpSpeed)
        {
            
            jumpCount = 0;
            velocityY = 0;
        }
       else
       {
           velocityY += gr * Time.deltaTime;
           cc.Move(transform.up*velocityY);
       }
       if(Input.GetButtonDown("Jump")&&jumpCount==0)
       {
            jumpCount++;
            velocityY = jumpSpeed;
            mAvatar.SetTrigger("Jump");
         
       }




    }

    public void Jump()
    {
        if(jumpCount==0)
        {
            jumpCount++;
            velocityY = jumpSpeed;
            mAvatar.SetTrigger("Jump");
        }
    }
}
