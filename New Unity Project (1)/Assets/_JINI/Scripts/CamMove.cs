using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Transform target;    //카메라가 따라다닐 타겟
    float offset = 10;               //타겟과의 유지거리

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        
    }

    private void Rotate()
    {
       
    }
}
