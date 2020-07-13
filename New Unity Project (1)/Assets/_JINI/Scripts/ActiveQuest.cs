using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveQuest : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //퀘스트 창 활성화
        GameObject questWindow = GameObject.FindGameObjectWithTag("Quest").gameObject;
        questWindow.GetComponent<CanvasGroup>().alpha = 1;
        questWindow.GetComponent<CanvasGroup>().interactable = true;

        ////버튼으로 활용할 오브젝트 활성화
        //if (other.CompareTag("Player"))
        //{
        //    transform.GetChild(2).gameObject.SetActive(true);          
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject questWindow = GameObject.FindGameObjectWithTag("Quest").gameObject;
        questWindow.GetComponent<CanvasGroup>().alpha = 0;
        questWindow.GetComponent<CanvasGroup>().interactable = false;
    }
}
