using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject fx;
    GameObject player;
   
    public Transform target;

    public float velocity = 10.0f;
    int damage = 50;

    Rigidbody rb;

    public List<GameObject> FoundObjects;
    public float shortDistance;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //player = GameObject.Find("Player");
    }
    
    public void FixedUpdate() // 유도탄
    {
        //if(target.gameObject.activeSelf==false)
        //{
        //    gameObject.SetActive(false);
        //    if (GameObject.Find("Player"))
        //    {
        //        PlayerAttack pa = GameObject.Find("Player").GetComponent<PlayerAttack>();
        //        pa.bulletPool.Enqueue(this.gameObject);
        //    }
        //}
            rb.velocity = transform.forward * velocity;
            var targetRotation = Quaternion.LookRotation(target.position + new Vector3(0, 0.8f) - transform.position);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, 10.0f));
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<EnemyFSM>().HitDamage(damage,3);
        GameObject _fx = Instantiate(fx);
        _fx.transform.position = transform.position;
        Destroy(_fx, 0.5f);
        gameObject.SetActive(false);

        if (GameObject.Find("Player"))
        {
            PlayerAttack pa = GameObject.Find("Player").GetComponent<PlayerAttack>();
            pa.bulletPool.Enqueue(this.gameObject);
        }
    }
}
