using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCam : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 10.0f;

    private void Start()
    {
        gameObject.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }
    void FollowTarget()
    {
        //타겟 방향 구하기 (벡터의 뺄셈)
        //방향 = 타겟 - 자기자신
        Vector3 dir = target.position - transform.position;
        dir.Normalize();    //방향만 쓰는 거라서 반드시 정규화를 시켜줘야 함
        transform.Translate(dir * followSpeed * Time.deltaTime);
        //
        ////카메라가 타겟 위치에 도착했을 때 덜덜덜 떨리는 문제점
        if(Vector3.Distance(transform.position,target.position)<1.0f)
        {
            transform.position = target.position;
            StartCoroutine(OffTheCam());
            
        }
    }
    IEnumerator OffTheCam()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
       
    }
}
