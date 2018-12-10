using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardContrl : MonoBehaviour
{
    #region   创建单例 
    public static CardContrl _instance;
    void Awake()
    {
        _instance = this;
    }
    public static CardContrl Instance
    {
        get
        {
            // 不需要再检查变量是否为null
            return _instance;
        }
    }
    #endregion

    public int offset;
    public Player player;
    public List<GameObject> Cards = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        Cards = player.cards;
    }
    public void MoveCard()
    {
        Cards = player.newCards;
        int n = Cards.Count;
        //Debug.Log(n);
        for (int i = 0; i < n; i++)
        {
            //Debug.Log(Cards[i].transform.localPosition);
            offset = (i - n/2) * 30  - 250;
            Cards[i].transform.SetSiblingIndex(i);
            Cards[i].transform.DOLocalMove(new Vector3(offset, -82f, 0f), 0.5f);
            //Debug.Log(offset);
            //Cards[i].transform.DOLocalMove(Cards[i].transform.localPosition + new Vector3(offset, 0f, 0f), 2f);

        }
    }

    public void SortCard(int index)
    {
        //Debug.Log("当前层级" + index);
        Cards = player.newCards;
        int n = Cards.Count;
        //Vector3 target = Cards[index].transform.position;
        for (int i = 0; i < n; i++)
        {
            
            offset = (i - n/2) * 30 - 250;
            offset += index - i == 0 ? 0 : index - i > 0 ? -30 : 30;
            //Cards[i].transform.DOLocalMove(target + new Vector3(offset, -82f, 0f), 0.5f);
            Cards[i].transform.DOLocalMove(new Vector3(offset, -82f, 0f), 0.1f);
            //Cards[i].transform.DOLocalMove(Cards[i].transform.localPosition + new Vector3(offset, 0f, 0f), 2f);
            //iTween.MoveTo(Cards[i], iTween.Hash("position", Cards[i].transform.localPosition + new Vector3(offset, 0f, 0f), "time", 2f, "isLocal", true));
        }

    }
    public void ReSortCard(int index)
    {
        int n = Cards.Count;
        for (int i = 0; i < n; i++)
        {
            offset = index - i == 0 ? 0 : index - i < 0 ? -30 : 30;
            Cards[i].transform.DOLocalMove(Cards[i].transform.localPosition + new Vector3(offset, 0f, 0f), 0f);
            //iTween.MoveTo(Cards[i], iTween.Hash("position", Cards[i].transform.localPosition + new Vector3(offset, 0f, 0f), "time", 2f, "isLocal", true));
        }

    }
    #region  itween 移动变换
    //int count = Cards.GetComponentsInChildren().count;
    //for (int i = 0; i < 10; i++)
    //{
    //    offset = (i - 4) * 30 - 15-250;

    //    Debug.Log(offset);
    //    Cardtrans[i].transform.localPosition.x += offset;
    //    Vector3 tagetPos = new Vector3(offset, -82f, 0f) + Cardtrans[i].transform.localPosition;
    //    iTween.MoveTo(Cardtrans[i], tagetPos, 2f);
    //    iTween.MoveTo(Cards[i], iTween.Hash("position", new Vector3(offset, -82f, 0f), "time", 2f, "isLocal", true));
    //    Cardtrans[i].transform.localPosition = new Vector3(offset, -82f, 0f);
    //}
    #endregion
}
