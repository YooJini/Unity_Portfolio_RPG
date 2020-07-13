using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    //마우스 움직임을 따라 카메라 회전시키기
    public float speed = 150; //회전속도 (Time.DeltaTime을 통해 1초에 150도 회전)
    //회전각도를 직접 제어하자
    float angleX;


    // Update is called once per frame
    void Update()
    {
        //카메라 회전
        Rotate();
    }

    private void Rotate()
    {
        float h = Input.GetAxis("Mouse X");
        //P=P0+vt;
        //transform.position += dir * speed * Time.deltaTime;
        //회전도 똑같은 방식
        //transform.eulerAngles += dir * speed * Time.deltaTime;
        //카메라 문제 (-90 ~ 90) 고정됐다 풀렸다 하는 문제가 있음
        //직접 회전각도를 제한해서 처리하면 된다.
        //Vector3 angle = transform.eulerAngles;
        //angle += dir * speed * Time.deltaTime;
        //if (angle.x > 60) angle.x = 60;
        //if (angle.x < -60) angle.x = -60;
        //transform.eulerAngles = angle;

        //여기에는 또 문제가 있다.
        //유니티 내부적으로 -각도는 360도를 더해서 처리된다.
        //내가 만든 각도를 가지고 계산처리 해야 한다.

        angleX += h * speed * Time.deltaTime;
        angleX = Mathf.Clamp(angleX, -60, 60);
        transform.eulerAngles = new Vector3(0, angleX, 0);
    }
}
