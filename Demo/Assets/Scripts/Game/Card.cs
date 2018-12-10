using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    private Image image;    //牌的图片
    private CardInfo cardInfo;  //卡牌信息
    private bool isSelected;    //是否选中
    
    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void InitImage(CardInfo cardInfo)
    {
        this.cardInfo = cardInfo;
        image.sprite = Resources.Load("Images/Cards/" + cardInfo.cardName, typeof(Sprite)) as Sprite;
    }

    public void SetSelectState()
    {

        isSelected = !isSelected;
    }
}
