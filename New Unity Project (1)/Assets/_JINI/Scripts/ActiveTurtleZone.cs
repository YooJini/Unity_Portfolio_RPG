using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTurtleZone : MonoBehaviour
{
    //버섯존 파괴, 터틀존 생성
    public void ActiveTZ()
    {
        GameObject.Find("FloatingMap").gameObject.transform.Find("TurtleZone_big").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
