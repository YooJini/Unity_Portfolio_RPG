using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveAttackBtn : MonoBehaviour
{
  public void ActiveAttack2()
  {
        //공격2 버튼 활성화
        transform.Find("Attack2").gameObject.SetActive(true);
        //LOCK 이미지 비활성화
        gameObject.GetComponent<Image>().enabled = false;

  }
  public void ActiveAttack3()
  {
        //공격3 버튼 활성화
        transform.Find("Attack3").gameObject.SetActive(true);
        //LOCK 이미지 비활성화
        gameObject.GetComponent<Image>().enabled = false;
    }
}
