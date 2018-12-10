﻿using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Defense : MonoBehaviour, IDragHandler, IPointerDownHandler,
    IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string cardID;  //卡牌ID
    public string cardName; //卡牌图片名
    public string cardType; //牌的类型
    public string cardMessage;  //卡牌信息

    public int defense = 5;

    public int cardCost;    //卡牌花费

    public int viewIndex;  //显示层级


    public float isDisCard;

    public Player player;
    public Enemy enemy;


    public void OnPointerEnter(PointerEventData eventDate)
    {
        //鼠标移动到UI上
        viewIndex = this.transform.GetSiblingIndex();
        CardContrl.Instance.SortCard(viewIndex);
        this.transform.SetAsLastSibling();
        this.transform.localScale += new Vector3(0.05f, 0.05f, 0f);
    }

    public void OnPointerExit(PointerEventData eventDate)
    {
        //鼠标离开UI
        this.transform.SetSiblingIndex(viewIndex);
        CardContrl.Instance.MoveCard();
        this.transform.localScale -= new Vector3(0.05f, 0.05f, 0f);
    }

    public void OnPointerDown(PointerEventData eventDate)
    {
        //鼠标点击
        isDisCard = this.transform.position.y;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //鼠标拖动
        GameObject pointerDrag = eventData.pointerDrag;
        Vector3 globalMousePosition;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(pointerDrag.GetComponent<RectTransform>(), eventData.position, Camera.main, out globalMousePosition))
        {
            pointerDrag.transform.position = globalMousePosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //鼠标松开
        float s = this.transform.position.y - isDisCard;
        if (s > 3f)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
            enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
            if (player.OnCost(cardCost))
            {
                this.transform.DOLocalMove(new Vector3(-510f, -97f, 0f), 0.5f);
                player.DisCard(gameObject);
                player.OnShield(defense);
            }
           
        }

    }

}
