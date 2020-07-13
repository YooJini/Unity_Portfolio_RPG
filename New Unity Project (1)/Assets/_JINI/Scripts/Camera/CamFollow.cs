using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class CamFollow : MonoBehaviour
{
    public Transform target;            //추적 대상
    //public float moveDamping = 15.0f;   //이동 속도 계수
    public float rotateDamping = 10.0f; //회전속도 계수
    public float distance = 5.0f;       //추적 대상과의 거리
    public float height = 4.0f;         //추적 대상과의 높이
    //public float targetOffset = 2.0f;   //추적 좌표의 오프셋

    //Camera의 Transform 컴포넌트
    private Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    //주인공 캐릭터의 이동 로직이 완료된 후 처리하기 위해 LateUpdate에서 구현
    private void LateUpdate()
    {
        //부드러운 회전
        float currAngle = Mathf.LerpAngle(tr.eulerAngles.y, target.eulerAngles.y, rotateDamping * Time.deltaTime);

        Quaternion rot = Quaternion.Euler(0, currAngle, 0);

        //Vector3 vec = new Vector3(target.position.x, tr.position.y, target.position.z);
        //이동할 때의 속도 계수를 적용
        tr.position = target.position - (rotateDamping * Vector3.forward * distance) + Vector3.up * height;

        tr.LookAt(target);
    }

}
