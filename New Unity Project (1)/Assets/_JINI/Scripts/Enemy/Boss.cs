using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading;
using UnityEditor.PackageManager;
using UnityEditorInternal;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,Attack1,Attack2, Stun, Die
    }

    public EnemyState state;

    GameObject target;

   
    float attackRange = 6;  //공격가능한 범위

    //애니메이션을 제어하기위한 애니메이터 컴포넌트
    Animator anim;

   
    int att = 5;    //공격력
   
    int hp = 500;   //체력



    void Start()
    {
        //보스 상태 초기화
        state = EnemyState.Idle;
        //플레이어 게임오브젝트를 찾아서 타겟에 저장
        target = GameObject.Find("Player");
        //애니메이터 컴포넌트
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {

        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Attack1:
                Attack1();
                break;
            case EnemyState.Attack2:
 
                break;
            case EnemyState.Stun:

                break;
            case EnemyState.Die:
                
                break;
        }
    }

   

    private void Idle()
    {

        if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
        {
            state = EnemyState.Attack1;
            //애니메이션
            anim.SetTrigger("Attack1");
          
        }

    }


    private void Attack1()
    {
        //공격 범위 안에 들어옴
        if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
        {
            state = EnemyState.Idle;
            anim.SetTrigger("Idle");
        }

    }
  
    //플레이어쪽에서 충돌감지를 할 수 있으니 퍼블릭으로 만든다.
    public void HitDamage(int value)
    {
        //예외처리
        //피격 상태이거나 죽은 상태일 때는 데미지 중첩으로 주지 않는다.
        if (state == EnemyState.Stun || state == EnemyState.Die) return;

        hp -= value;

        if (hp > 0)
        {
            state = EnemyState.Stun;
            anim.SetTrigger("Stun");
            Damaged();

        }
        else
        {
            state = EnemyState.Die;
            anim.SetTrigger("Die");
            Die();
        }
    }
    //피격상태 (Any State) 
    void Damaged()
    {

        //코루틴을 사용하자
        //1. 몬스터 체력이 1이상
        //2. 다시 이전 상태로 변경
        //- 상태변경
        //- 상태전환 출력

        //피격상태를 처리하기 위한 코루틴 실행
        StartCoroutine(DamageProc());

    }
    //피격상태 처리용 코루틴
    IEnumerator DamageProc()
    {
        //피격모션 시간만큼 기다리기
        yield return new WaitForSeconds(1.0f);
        //현재상태를 이동으로 전환
        state = EnemyState.Attack2;
        anim.SetTrigger("Attack2");

    }
    //죽음상태 (Any State)
    void Die()
    {

        //코루틴을 사용하자
        //1. 체력이 0이하
        //2. 몬스터 오브젝트 삭제
        //- 상태변경
        //- 상태전환 출력 (죽었다)

        //진행중인 모든 코루틴 정지
        StopAllCoroutines();
        StartCoroutine(DieProc());
    }
    IEnumerator DieProc()
    {
        //2초 후에 자기자신 제거
        yield return new WaitForSeconds(2.0f);
        Debug.Log("죽었다");
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        

        //공격가능범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
}