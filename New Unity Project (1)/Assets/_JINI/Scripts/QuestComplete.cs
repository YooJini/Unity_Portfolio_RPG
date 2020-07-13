using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestComplete : MonoBehaviour
{
    public void ActiveCompleteBtn()
    {
        transform.Find("CompleteBtn").gameObject.SetActive(true);
    }
}
