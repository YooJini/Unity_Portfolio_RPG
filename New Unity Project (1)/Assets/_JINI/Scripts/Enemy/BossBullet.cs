using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
 
    public GameObject fx;

    Transform target;

    public float velocity = 10.0f;
    int damage = 20;

    Rigidbody rb;


    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>();

    }
    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }
    public void FixedUpdate()
    { 
        rb.velocity = transform.forward * velocity;
        if(gameObject.transform.position.z<target.position.z)
        {
            BossAttack ba = GameObject.Find("Boss").GetComponent<BossAttack>();
            gameObject.SetActive(false);
            ba.bulletPool.Enqueue(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Damage>().HitDamage(damage);
        GameObject _fx = Instantiate(fx);
        _fx.transform.position = transform.position;
        Destroy(_fx, 0.5f);
        gameObject.SetActive(false);

        if (GameObject.Find("Boss"))
        {
            BossAttack ba = GameObject.Find("Boss").GetComponent<BossAttack>();
            ba.bulletPool.Enqueue(this.gameObject);
        }
    }
}

