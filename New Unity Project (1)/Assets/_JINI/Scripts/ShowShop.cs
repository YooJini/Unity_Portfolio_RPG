using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWindow : MonoBehaviour
{
    CanvasGroup cg;

    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
    }
    public void ShowShopWindow()
    {
        cg.alpha = 1;
        cg.interactable = true;
    }
    public void HideShopWindow()
    {
        cg.alpha = 0;
        cg.interactable = false;
    }
}
