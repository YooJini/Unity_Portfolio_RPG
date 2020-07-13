
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState
    {
        Idle, Move, Attack, Return, Damaged, Die
    }

    public EnemyState state;

    GameObject target;

    public float findRange = 15;    //플레이어를 찾는 범위
    public float moveRange = 30;    //시작지점에서 최대 이동가능한 범위
    public float attackRange = 2;  //공격가능한 범위

    //애니메이션을 제어하기위한 애니메이터 컴포넌트
    Animator anim;

    //처음 위치
    Vector3 origin;
    Quaternion originRot;

    //유용한 기능
    #region "Idle 관련 변수"

    #endregion
    #region "Move 관련 변수"
    //CharacterController cc;
    float moveSpeed = 3.0f;
    NavMeshAgent meshAgent;

    #endregion
    #region "Attack 관련 변수"
    float attackCount = 2;
    float count = 0;
    float att = 2.5f;    //공격력
    #endregion
    #region "Return 관련 변수"
    #endregion
    #region "Damaged 관련 변수"
    EnemyState preState;    //이전 상태
    int hp = 100;   //체력
    #endregion
    #region "Die 관련 변수"
    #endregion

    void Start()
    {
        //몬스터 상태 초기화
        state = EnemyState.Idle;
        //플레이어 게임오브젝트를 찾아서 타겟에 저장
        target = GameObject.Find("Player");
        //캐릭터컨트롤러 컴포넌트 가져오기
        //cc = GetComponent<CharacterController>();
        //시작지점 저장
        origin = transform.position;
        originRot = transform.rotation;
        //애니메이터 컴포넌트
        anim = GetComponentInChildren<Animator>();

        meshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //if (state != EnemyState.Damaged) preState = state;

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

        if (Vector3.Distance(transform.position, target.transform.position) <= findRange)
        {
            state = EnemyState.Move;
            Debug.Log("Idle -> Move");

            //애니메이션
            anim.SetTrigger("Move");
            anim.SetInteger("MoveType", Random.Range(0, 2));
        }

        //1. 플레이어와 일정범위가 되면 이동 상태로 변경 (탐지범위)
        //- 플레이어 찾기 (GameObject.Find("Player"))
        //- 일정거리 15미터 (거리비교: Distance, Magnitude)
        //- 상태변경
        //State = EnemyState.Move;
        //- 상태전환 출력

    }

    private void Move()
    {

        if (Vector3.Distance(transform.position, origin) > moveRange)
        {
            state = EnemyState.Return;
            Debug.Log("Move -> Return");
            anim.SetTrigger("Return");
        }
        //이동처리
        else if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
        {
            meshAgent.SetDestination(target.transform.position);
            meshAgent.stoppingDistance = attackRange;
            //Vector3 dir = (target.transform.position - transform.position).normalized;
            //좀더 자연스럽게 플레이어를 바라보도록(회전처리) 하고 싶다
            //transform.forward = Vector3.Lerp(transform.forward, dir, 10 * Time.deltaTime);
            //벡터의 러프를 사용할 때 발생하는 문제: 타겟과 일직선상에 있을 경우 백덤블링으로 회전한다. (어느방향으로 돌아야 할지를 몰라서)
            //자연스러운 회전처리를 위해서는
            //결국 쿼터니온을 사용해야 한다.
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);

            //캐릭터 컨트롤러를 이용하여 이동하기
            //cc.Move(dir* moveSpeed * Time.deltaTime);
            //여기에는 중력이 적용 안되는 문제가 있다.
            //중력 문제를 해결하기 위해서 심플무브를 사용한다.
            //심플무브는 최소한의 물리가 적용되어 중력문제를 해결할 수 있다.
            //단 내부적으로 시간처리를 하기 때문에 Time.deltaTime을 사용하지 않는다.
            //cc.SimpleMove(dir * moveSpeed);
        }
        //공격범위 안에 들어옴
        else
        {
            state = EnemyState.Attack;
            Debug.Log("Move -> Attack");

        }
        //1. 플레이어를 향해 이동 후 공격범위 안에 들어가면 공격상태로 변경
        //2. 플레이어를 추격하더라도 처음위치에서 일정범위(30미터)를 넘어가면 리턴상태로 변경
        //- 플레이어처럼 캐릭터컨트롤러 이용
        //- 공격범위 2미터
        //- 상태변경
        //- 상태전환 출력

    }

    private void Attack()
    {
        //공격 범위 안에 들어옴
        if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
        {   //일정 시간마다 플레이어 공격
            count += Time.deltaTime;
            if (count >= attackCount)
            {
                count = 0;
                Debug.Log("Hit");
                anim.SetTrigger("Attack");
                anim.SetInteger("AttackType", Random.Range(0, 2));
                target.GetComponent<Damage>().HitDamage(att);
            }
        }
        else
        {
            count = 0;
            state = EnemyState.Move;
            Debug.Log("Attack -> Move");
            anim.SetTrigger("Move");
            anim.SetInteger("MoveType", Random.Range(0, 2));
        }

        //1. 플레이어가 공격범위 안에 있다면 일정한 시간 간격으로 플레이어 공격
        //2. 플레이어가 공격범위를 벗어나면 이동상태 (재추격)
        //- 공격범위 2미터
        //- 상태변경
        //- 상태전환 출력

    }

    private void Return()
    {

        if (Vector3.Distance(transform.position, origin) > 0.5f)
        {
            meshAgent.SetDestination(origin);

            meshAgent.stoppingDistance = 0.1f;
            //Vector3 dir = (origin - transform.position).normalized;
            //cc.SimpleMove(dir * moveSpeed);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
        else
        {
            //transform.rotation = Quaternion.identity;   //회전값을 0으로 초기화
            transform.rotation = originRot;
            //위치값을 초기값으로
            transform.position = origin;
            

            anim.SetTrigger("Idle");
            state = EnemyState.Idle;
            Debug.Log("Return -> Idle");
           
        }
        //1. 몬스터가 플레이어를 추격하더라도 처음 위치에서 일정범위를 벗어나면 다시 돌아옴
        //- 처음위치에서 일정범위 30미터
        //- 상태변경
        //- 상태전환 출력

    }
    //플레이어쪽에서 충돌감지를 할 수 있으니 퍼블릭으로 만든다.
    public void HitDamage(int value,int attackType)
    {
        //예외처리
        //피격 상태이거나 죽은 상태일 때는 데미지 중첩으로 주지 않는다.
        //예외적으로 플레이어공격3의 경우에는 데미지 중복 가능하다. 
        //Die 상태일 때는 어떤공격이든지 상관없이 데미지 중복 불가능
        if (attackType != 3 && (state == EnemyState.Damaged || state == EnemyState.Die)) return;
        else if (attackType == 3 && state == EnemyState.Die) return;

        hp -= value;
        GetComponent<EnemyDamage>().HpBarDamaged(hp);

        if (hp > 0)
        {
            state = EnemyState.Damaged;
            Debug.Log("Damaged");
            anim.SetTrigger("Damaged");
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
        state = EnemyState.Move;
        anim.SetTrigger("Move");
        anim.SetInteger("MoveType", Random.Range(0, 2));

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

        //처치한 에너미 수 증가
        //에너미가 초록버섯일 경우
        if (gameObject.name.Contains("Mushroom"))
        {
            target.GetComponent<PlayerAttack>().mushroomCount++;
            target.GetComponent<PlayerAttack>().CheckMushroomCount();
        }
        //에너미가 파랭이일 경우
        if(gameObject.name.Contains("Turtle"))
        {
            target.GetComponent<PlayerAttack>().turtleCount++;
            target.GetComponent<PlayerAttack>().CheckTurtleCount();
        }

    }
    IEnumerator DieProc()
    {
        //cc.enabled = false; //굳이 안해줘도 됨
        //2초 후에 자기자신 제거
        yield return new WaitForSeconds(3.0f);
        Debug.Log("죽었다");

        Destroy(gameObject);
        GetComponent<EnemyDamage>().HpBarClear();

        //gameObject.SetActive(false);

    }
    private void OnDrawGizmos()
    {
        //플레이어를 찾을 수 있는 범위
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, findRange);
        //최대 이동가능 범위
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(origin, moveRange);
        //공격가능범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

       

    }

  
}