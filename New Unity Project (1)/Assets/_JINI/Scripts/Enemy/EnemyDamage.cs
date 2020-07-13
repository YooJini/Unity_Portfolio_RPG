using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    private const string bulletTag = "BULLET";
    //생명 게이지
    private float hp = 100.0f;
    //초기 생명 수치
    private float initHp = 100.0f;

 
    //생명 게이지 프리팹을 저장할 변수
    public GameObject hpBarPrefab;
    //생명 게이지의 위치를 보정할 오프셋
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    //부모가 될 canvas 객체
    private Canvas uiCanvas;
    //생명 수치에 따라 fillamount 속성을 변경할 image
    private Image hpBarImage;

    GameObject hpBar;
    // Start is called before the first frame update
    void Start()
    {
        //혈흔 효과 프리팹을 로드
        //bloodEffect = Resources.Load<GameObject>("BulletImpactFleshBigEffect");

        //생명 게이지의 생성 및 초기화
     SetHpBar();
    }

    private void SetHpBar()
    {
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        //UI 캔버스 하위로 생명 게이지 생성
        hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
        //fillAmount 속성을 변경할 이미지 추출
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];
        hpBarImage.fillAmount = 1;
        //생명 게이지가 따라가야할 대상과 오프셋 값 설정
        var _hpBar = hpBar.GetComponent<EnemyHpBar>();
        _hpBar.targetTr = this.gameObject.transform;
        _hpBar.offset = hpBarOffset;

        hpBar.GetComponent<CanvasGroup>().alpha = 0;
        
    }


    public void HpBarDamaged(int hp)
    {
        if (hpBar.GetComponent<CanvasGroup>().alpha == 0) hpBar.GetComponent<CanvasGroup>().alpha = 1;
        hpBarImage.fillAmount = hp / initHp;
    }
    
    public void HpBarClear()
    {
        
        hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
        var _hpBar = hpBar.GetComponent<EnemyHpBar>();
        _hpBar.targetTr = null;

    }
   
}
