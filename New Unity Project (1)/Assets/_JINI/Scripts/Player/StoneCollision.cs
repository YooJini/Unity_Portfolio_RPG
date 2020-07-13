using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StoneCollision : MonoBehaviour
{
    public GameObject att2_collision_effect;
    BoxCollider boxCollider;

    int attack2_att = 40;

    Shake shake;

    private void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        shake = GameObject.Find("CameraRig").GetComponent<Shake>();
    }

    private void OnTriggerEnter(Collider other)
     {
        if (other.gameObject.name.Contains("big"))
         {
            DisableCollider();
            StartCoroutine(shake.ShakeCamera());
            StartCoroutine(Sphere());
            GameObject effect = Instantiate(att2_collision_effect);
            effect.transform.position = gameObject.transform.position + new Vector3(0,0.3f,0);
            Destroy(effect,4.0f);         
         }
     }
    IEnumerator Sphere()
    {
        yield return new WaitForSeconds(0.1f);
        print("Start Sphere");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3.0f,1<<9);
        for(int i=0;i<hitColliders.Length;i++)
        {
            hitColliders[i].gameObject.GetComponent<EnemyFSM>().HitDamage(attack2_att,2);
        }
       
    }
    public void EnableCollider()
    {
        boxCollider.enabled = true;
    }
    public void DisableCollider()
    {
        boxCollider.enabled = false;
    }
    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.layer == 1 << 8)
    //      {
    //         GameObject effect = Instantiate(att1_collision_effect);
    //         effect.transform.position = this.gameObject.transform.position;
    //      }
    // }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3.0f);
    }
}
