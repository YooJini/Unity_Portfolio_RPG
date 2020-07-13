using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (transform.position.y > 35)
        {
            rigid.isKinematic = true;
            GameObject.Find("Boss").transform.Find("BossCam").gameObject.SetActive(true);
           
        }
        rigid.AddForce(Vector3.up*5, ForceMode.VelocityChange);
               
    }
}