using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Camera curCamera;

    int shakeCount = 0;
    Vector3 mCurPos;//相机的初始位置
    float radio = 0.2f;

    public void ShakeCameraWithCount()
    {
        mCurPos = curCamera.transform.position;
        shakeCount = Random.Range(5, 7);
        StartCoroutine("ShakeWithCount");
        //ShakeWithCount();
    }
    IEnumerator ShakeWithCount()
    {
        while (shakeCount > 0)
        {
            yield return new WaitForSeconds(0.05f);

            shakeCount--;
            float r = Random.Range(-radio, radio);//随机的震动幅度
            Debug.Log(r);
            if (shakeCount == 0)
            {
                //保证最终回归到原始位置
                //curCamera.transform.position = mCurPos;
            }
            else
            {
                curCamera.transform.position = mCurPos + Vector3.one * r;
                Debug.Log(curCamera.transform.position);
            }

        }
    }

    public float HP = 60;
    public float shield = 0;//盾
    public int Cost = 3;
    public float HPtemp;

    public int isWeak = 0;
    public int isVulnerability = 0;

    public GameObject bufWeak;
    public GameObject bufVulnerability;

    //等待玩家操作  
    public bool isWaitPlayer = true;
    public bool ifUIshow = true;
    public bool isDealCard = true;

    public GameObject popupDamageGo;

    public Slider health;
    public Text text;

    public Slider shieldSlider;
    public Text shieldText;

    public Text costText;

    public int cardCount;  //0
    public int cardNumb;  //抽卡数

    public List<GameObject> cards = new List<GameObject>();
    public List<GameObject> begCards = new List<GameObject>();
    public List<GameObject> endCards = new List<GameObject>();
    public List<GameObject> newCards = new List<GameObject>();


    public void GameStart()
    {
        HPtemp = HP;

        curCamera = Camera.main;

        begCards = cards;
        Shuffle(begCards);

        health.value = HP;
        text.text = HP.ToString();
    }
    /// <summary>
    /// 伤害
    /// </summary>
    /// <param name="mValue"></param>
    public void OnDamage(int mValue)
    {
        ShakeCameraWithCount();
        if (isVulnerability > 0)
            mValue = (int)(mValue * 1.5f);
        if (mValue - shield >= 0)
        {
            mValue -= (int)shield;
            shield = 0;
            shieldSlider.value = 0;
            shieldText.text = "0";
        }
        else
        {
            shield -= mValue;
            shieldSlider.value = shield * 0.01f;
            shieldText.text = shield.ToString();
            mValue = 0;
        }
        GameObject damageGo = Instantiate(popupDamageGo, transform) as GameObject;
        damageGo.GetComponent<PopupDamage>().Value = mValue;
        HP -= mValue;
        health.value = HP / HPtemp;
        if (health.value < 0) health.value = 0;
        text.text = HP.ToString();
    }
    /// <summary>
    ///  防御
    /// </summary>
    /// <param name="mValue"></param>
    public void OnShield(int mValue)
    {
        shield += mValue;
        shieldSlider.value = shield * 0.01f;
        shieldText.text = shield.ToString();
    }
    public bool OnCost(int n)
    {
        if (Cost - n >= 0)
        {
            Cost -= n;
            costText.text = Cost.ToString();
            return true;
        }
        return false;
    }

    /// <summary>
    /// 洗牌
    /// </summary>
    /// <param name="begCards"></param>
    void Shuffle(List<GameObject> begCards)
    {
        for (int i = 0; i < begCards.Count; i++)
        {
            GameObject temp = begCards[i];
            int randomIndex = Random.Range(0, begCards.Count);
            begCards[i] = begCards[randomIndex];
            begCards[randomIndex] = temp;
        }
    }

    /// <summary>
    /// 发牌
    /// </summary>
    public void DealCard()
    {
        isDealCard = false;
        for (int i = 0; i < cardNumb; i++)
        {
            if (begCards.Count == 0)
            {
                if (endCards.Count == 0)
                {
                    Debug.Log("无卡");
                    break;
                }
                InitCard();
                //Debug.Log("更新卡组");
            }
            if (newCards.Count >= 10)
            {
                Debug.Log("手牌已满");
                break;
            }
            newCards.Add(begCards[0]);
            begCards.RemoveAt(0);
        }
        //Debug.Log("更新牌");
        CardContrl.Instance.MoveCard();
        shield = 0;
        shieldSlider.value = 0;
        shieldText.text = "0";
        Cost = 3;
        costText.text = Cost.ToString();
    }

    /// <summary>
    /// 更新卡组
    /// </summary>
    public void InitCard()
    {
        int count = endCards.Count;
        for (int i = 0; i < count; i++)
        {
            begCards.Add(endCards[0]);
            endCards[0].transform.DOLocalMove(new Vector3(15, -97f, 0f), 0.5f);
            endCards.RemoveAt(0);
        }
        Shuffle(begCards);
    }

    /// <summary>
    /// 结束回合
    /// </summary>
    public void EndTurn()
    {
        int count = newCards.Count;
        for (int i = 0; i < count; i++)
        {
            endCards.Add(newCards[0]);
            newCards[0].transform.DOLocalMove(new Vector3(-510f, -97f, 0f), 0.5f);
            newCards.RemoveAt(0);
        }
    }
    /// <summary>
    /// 出牌
    /// </summary>
    public void DisCard(GameObject card)
    {
        endCards.Add(card);
        newCards.Remove(card);
        CardContrl.Instance.MoveCard();
    }


    public void StateTable()
    {
        if (isWeak > 0)
        {
            bufWeak.SetActive(true);
        }
        if(isVulnerability>0)
        {
            bufVulnerability.SetActive(true);
        }
        if (isWeak == 0)
            bufWeak.SetActive(false);
        if (isVulnerability == 0)
            bufVulnerability.SetActive(false);
            
    }

    void OnGUI()
    {
        //如果处于等待状态，则显示操作窗口  
        if (isWaitPlayer && ifUIshow)
        {
            
            if(isDealCard)
                DealCard();
            if (GUI.Button(new Rect(0, 20, 200, 30), "结束回合"))
            {
                if (isVulnerability > 0)
                    isVulnerability -= 1;
                if (isWeak > 0)
                    isWeak -= 1;
                StateTable();
                //交换操作权  
                EndTurn();
                isWaitPlayer = false;
                ifUIshow = false;
            }
        }
    }

}
