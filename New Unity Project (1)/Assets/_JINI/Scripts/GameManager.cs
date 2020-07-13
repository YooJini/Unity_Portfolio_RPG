using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  // //일시정지 여부를 판단하는 변수
  // private bool isPaused;
  //
  // public void OnPauseClick()
  // {
  //     //일시정지 값을 토글시킴
  //     isPaused = !isPaused;
  //     //Time Scale이 0이면 정지, 1이면 정상 속도
  //     Time.timeScale = (isPaused) ? 0.0f : 1.0f;
  //
  //     var playerObj = GameObject.FindGameObjectWithTag("PLAYER");
  //     var scripts = playerObj.GetComponents<MonoBehaviour>();
  //    
  //     foreach (var script in scripts)
  //     {
  //         script.enabled = !isPaused;
  //     }
  //
  //  
  //     //따라서 일시 정지가 됐을 때 일부 UI(무기교체버튼)가 클릭되지 않도록 구현
  //     //보통 일시정지됐을 때 비활성화해야 하는 각종 버튼은 하나의 빈 게임오브젝트 하위에 넣어두고 
  //     //부모 게임오브젝트 하나의 Canvas Group만 추가해 구현한다.
  //     
  //     var canvasGroup = GameObject.Find("Panel - Weapon").GetComponent<CanvasGroup>();
  //     canvasGroup.blocksRaycasts = !isPaused;
  // }
}
