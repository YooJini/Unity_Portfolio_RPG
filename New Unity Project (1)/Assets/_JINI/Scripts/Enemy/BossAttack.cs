using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public Transform firePos;
    public GameObject Attack2_Fx;

    public Queue<GameObject> bulletPool;
    public GameObject bulletFactory;
    int poolSize = 3;

    private void Start()
    {
        InitObjectPooling();
    }
    public void Fire1()
    {

        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            bullet.transform.position = firePos.transform.position;
            bullet.transform.forward = firePos.transform.forward;
        }
        else
        {
            //총알오브젝트 생성
            GameObject bullet = Instantiate(bulletFactory);
            bullet.SetActive(false);
            //생성된 총알 오브젝트를 풀에 담는다
            bulletPool.Enqueue(bullet);
        }

    }
    public void Fire2()
    {
       GameObject fx = Instantiate(Attack2_Fx);
        fx.transform.position = firePos.position;
        fx.transform.rotation = firePos.rotation;

    }
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

}
