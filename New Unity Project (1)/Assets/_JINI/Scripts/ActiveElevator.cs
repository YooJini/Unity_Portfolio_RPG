using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveElevator : MonoBehaviour
{
    public void ActiveElevatorZone()
    {
        gameObject.transform.Find("Elevator").gameObject.SetActive(true);
    }
}
