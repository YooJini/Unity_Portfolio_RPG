using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    Animator mAvatar;
    public bool isAttack=false;

    //처치한 에너미 수 (퀘스트 완료했는지 판별하기 위함)
    //초록버섯 (퀘스트1)
    public int mushroomCount = 0;
    //파랭이 (퀘스트2)
    public int turtleCount = 0;

    //공격1
    GameObject collisionCubePos;
    CollisionCube collisionCube;

    //공격2
    GameObject stonePoint;
    StoneCollision stoneCollision;

    //공격3
    public GameObject FirePos;
    public Queue<GameObject> bulletPool;
    public GameObject bulletFactory;
    int poolSize = 5;
    


    // Start is called before the first frame update
    void Start()
    {
        mAvatar = GetComponent<Animator>();
        stonePoint = GameObject.FindGameObjectWithTag("HammerColPoint");
        stoneCollision = stonePoint.GetComponent<StoneCollision>();
        collisionCubePos = GameObject.FindGameObjectWithTag("HammerCube");
        collisionCube = collisionCubePos.GetComponent<CollisionCube>();
        InitObjectPooling();
       
    }
    //공격3
    //총알 오브젝트 풀링 초기화
    void InitObjectPooling()
    {
        bulletPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }
    //총알 생성
    public void CreateBullet()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 8.0f, 1 << 9);
        if (hitColliders.Length > 0)
        {
            for (int i = 0; i < poolSize;)
            {
                GameObject bullet = bulletPool.Dequeue();
                bullet.SetActive(true);
                bullet.transform.position = FirePos.transform.position;
                bullet.transform.forward = FirePos.transform.forward;
                int index = hitColliders.Length - i - 1;
                if (index < 0) index = 0;
                bullet.GetComponent<Bullet>().target = hitColliders[index].transform;

            }
        }
        else return;
            
    }
    public void Attack1()
    {
        
        if (!isAttack)
        {
            collisionCube.EnableCollider();
            //ActiveStoneCollision();
            //StartCoroutine(ActiveStoneCollision());
            isAttack = true;
            mAvatar.SetTrigger("Attack1");
            mAvatar.SetInteger("AttackType",Random.Range(0, 2));
            
        }
 
    }
    public void Attack2()
    {
        if (!isAttack)
        {
            ActiveStoneCollision();
            isAttack = true;
            mAvatar.SetTrigger("Attack2");
        }
    }
    public void Attack3()
    {
        if (!isAttack)
        {
            isAttack = true;
            mAvatar.SetTrigger("Attack3");            
        }
    }

    //공격종료
    //공격 도중 다른 공격의 중복 사용을 막기 위해
    public void AttackEnd1_1()
    {
        isAttack = false;
        collisionCube.DisableCollider();
    }
    public void AttackEnd1_2()
    {
        isAttack = false;
        collisionCube.DisableCollider();
    }
    public void AttackEnd2()
    {
        isAttack = false;
    }
    public void AttackEnd3()
    {
        isAttack = false;
    }

   
    void ActiveStoneCollision()
    {
        //if (mAvatar.GetInteger("AttackType") == 1) return;

        stoneCollision.EnableCollider();
    }

    //죽은 버섯 수 카운트
  public void CheckMushroomCount()
   {
       if(mushroomCount==5)
       {
           GameObject.Find("FirstQuest").GetComponent<QuestComplete>().ActiveCompleteBtn();
      
       }
   }
    //죽은 거북이 수 카운트
    public void CheckTurtleCount()
    {
        if (turtleCount == 5)
        {
            GameObject.Find("SecondQuest").GetComponent<QuestComplete>().ActiveCompleteBtn();

        }
    }

    //IEnumerator ActiveStoneCollision()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    
    //    stoneCollision.EnableCollider();
    //}

}
