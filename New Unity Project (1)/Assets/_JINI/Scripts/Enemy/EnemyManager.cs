using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    
    //에너미 스폰타임
    //에너미 스폰위치

    public GameObject enemyFactory;     //에너미 프리팹
    //public GameObject spawnPoint;     //스폰 위치
    public GameObject[] spawnPoints;    //스폰 위치 배열
    //float respawnTime = 0.2f;           //리스폰타임 
    //float curTime = 0.0f;               //누적타임

    //오브젝트 풀링
    //오브젝트 풀링에 사용할 최대 에너미 수
    //int poolSize = 10;

    //int enemySize = 10;

    //public Queue<GameObject> enemyPool;

    private void Start()
    {
        //오브젝트 풀링 초기화
        //InitObjectPooling();
        SpawnEnemy();
    }

   // private void InitObjectPooling()
   // {
   //     enemyPool = new Queue<GameObject>();
   //     for (int i = 0; i < poolSize; i++)
   //     {
   //         GameObject enemy = Instantiate(enemyFactory);
   //         enemy.SetActive(false);
   //         enemyPool.Enqueue(enemy);
   //     }
   // }
    private void SpawnEnemy()
    {
        //for (int i = 0; i < spawnPoints.Length; i++)
        //{
        //    GameObject enemy = enemyPool.Dequeue();
        //    enemy.SetActive(true);
        //    enemy.transform.position = spawnPoints[i].transform.position;
        //}
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject enemy = Instantiate(enemyFactory);
            enemy.transform.position = spawnPoints[i].transform.position;
        }
    }


   // private void RespawnEnemy()
   // {
   //     
   //
   //     curTime += Time.deltaTime;
   //     if (curTime > respawnTime)
   //     {
   //         //누적된 현재시간을 0.0초로 초기화
   //         curTime = 0.0f;
   //         //리스폰타임을 랜덤으로
   //         respawnTime = UnityEngine.Random.Range(0.2f, 0.5f);
   //
   //         if (enemyPool.Count > 0)
   //         {   //오브젝트풀링
   //             GameObject enemy = enemyPool.Dequeue();
   //             enemy.SetActive(true);
   //             int index = UnityEngine.Random.Range(0, spawnPoints.Length);
   //             enemy.transform.position = spawnPoints[index].transform.position;
   //         }
   //        // else
   //        // {
   //        //     //에너미 오브젝트 생성
   //        //     GameObject enemy = Instantiate(enemyFactory);
   //        //     enemy.SetActive(false);
   //        //     //생성된 에너미 오브젝트를 풀에 담는다
   //        //     enemyPool.Enqueue(enemy);
   //        // }
   //     }
   // }
}
