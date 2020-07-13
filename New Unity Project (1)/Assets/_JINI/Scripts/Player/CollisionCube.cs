using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CollisionCube : MonoBehaviour
{
    int att = 30;
    BoxCollider boxCollider;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Boss") other.GetComponent<Boss>().HitDamage(att);
        else other.GetComponent<EnemyFSM>().HitDamage(att, 1);
    }

    public void EnableCollider()
    {
        boxCollider.enabled = true;
    }
    public void DisableCollider()
    {
        boxCollider.enabled = false;
    }
}
