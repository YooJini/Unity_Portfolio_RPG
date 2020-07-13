using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public float coolTime = 5.0f;
    Button button;
    Image image;

    private void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    //0.02초 주기로 업데이트
    private void FixedUpdate()
    {
        if (image.fillAmount == 1)
        {
            button.interactable = true;
        }
        else
            image.fillAmount += 0.02f/coolTime;   

    }
    public void CoolTimeFunc()
    {
        button.interactable = false;
        image.fillAmount = 0.0f;
    }
   
}
