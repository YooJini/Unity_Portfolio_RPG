using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    float initHp = 100.0f;
    public float currHp = 100.0f;

    public Image hpBar;

    Animator mAvatar;

    private void Start()
    {
        currHp = initHp;
        mAvatar = GetComponent<Animator>();
    }
    public void HitDamage(float value)
    {
        currHp -= value;
        hpBar.fillAmount = currHp / initHp;
        print(currHp);

        if (currHp < 0) mAvatar.SetTrigger("Death");
    }
    public void FullHp()
    {
        currHp = initHp;
        hpBar.fillAmount = currHp / initHp;
    }
   
}
