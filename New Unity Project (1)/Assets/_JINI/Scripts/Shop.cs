using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Shop : MonoBehaviour
{
    public Text myCoinTxt;
    int myCoin;
    public Text redPotionCountTxt;
    int redPotionCount = 0;
    public Text bluePotionCountTxt;
    int bluePotionCount = 0;

    public Button redPotionBtn;
    public Button bluePotionBtn;

    AudioSource _audio;
    public AudioClip buySound;
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        myCoin = 5000;
        myCoinTxt.text = "My Coin: " + myCoin.ToString();
    }

  public void BuyRedPotion()
    {
        if (myCoin >= 500)
        {
            _audio.PlayOneShot(buySound);
            redPotionCount++;
            redPotionCountTxt.text = redPotionCount.ToString();
            myCoin -= 500;
            myCoinTxt.text = "My Coin: " + myCoin.ToString();
            if (redPotionCount == 1) redPotionBtn.interactable = true;
        }
    }
    public void BuyBluePotion()
    {
        if (myCoin >= 500)
        {
            _audio.PlayOneShot(buySound);
            bluePotionCount++;
            bluePotionCountTxt.text = bluePotionCount.ToString();
            myCoin -= 500;
            myCoinTxt.text = "My Coin: " + myCoin.ToString();
            if (bluePotionCount == 1) bluePotionBtn.interactable = true;
        }
    }
    public void ClickRedPotionBtn()
    {
        redPotionCount--;
        redPotionCountTxt.text = redPotionCount.ToString();
        if (redPotionCount == 0) redPotionBtn.interactable = false;

       
    }
    public void ClickBluePotionBtn()
    {
        bluePotionCount--;
        bluePotionCountTxt.text = bluePotionCount.ToString();
        if (bluePotionCount == 0) bluePotionBtn.interactable = false;
    }
}
