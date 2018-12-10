using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    #region   创建单例 
    public static GameManger _instance;
    public static GameManger GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameManger();
        }
        return _instance;
    }
    #endregion

    public Player player;
    public Enemy enemy;
    public GameObject gameover;
    public GameObject GameView;

    public Transform target;

    private OperatorState mState = OperatorState.Player;

    public enum OperatorState
    {
        Quit,
        EnemyAI,
        Player
    }

    //UI延迟2.5秒调出  
    IEnumerator WaitUI()
    {
        yield return new WaitForSeconds(2.5F);
        enemy.isWaitPlayer = true;
        player.ifUIshow = true;
        player.isDealCard = true;
    }

    //  
    IEnumerator WaitAI()
    {
        yield return new WaitForSeconds(2.0F);
        enemy.isWaitPlayer = false;
    }

    //为怪物AI延迟2秒  
    IEnumerator UpdateLater()
    {
        yield return new WaitForSeconds(2.0F);
        //敌人停止等待  
        enemy.isWaitPlayer = false;
        //敌人执行AI  
        enemy.StartAI();
        yield return new WaitForSeconds(2.0F);
    }

    void Update()
    {
        //如果敌我双方有一方生命值为0，则游戏结束  
        if (player.HP <= 0)
        {
            mState = OperatorState.Quit;
            Debug.Log("游戏失败");
            
        }
        else if (enemy.HP <= 0)
        {
            mState = OperatorState.Quit;
            Debug.Log("游戏胜利");
            LevelSystem.SetLevels(Global.GetInstance().loadName, false);
            string LevelName = Global.GetInstance().loadName;
            LevelName = LevelName.Substring(LevelName.Length - 1);

            int i = int.Parse(LevelName);
            i++;
            LevelSystem.SetLevels("level" + i, true);
            //LevelSystem.SetLevels("level"+i.ToString(), true);
            gameover.SetActive(true);
            MyClass.Instance.HP = player.HP;
            //SceneManager.LoadScene("Level");
        }
        else
        {
            switch (mState)
            {
                case OperatorState.Player:
                    if (!player.isWaitPlayer)
                    {
                        StartCoroutine("UpdateLater");
                        StartCoroutine("WaitUI");
                        mState = OperatorState.EnemyAI;
                    }
                    break;
                case OperatorState.EnemyAI:
                    if (enemy.isWaitPlayer)
                    {
                        StartCoroutine("WaitAI");
                        player.isWaitPlayer = true;
                        mState = OperatorState.Player;

                    }
                    break;
            }
        }
    }

    /// <summary>
    /// 加载卡牌
    /// </summary>
    public void InitCards()
    {
        //Hashtable temp = Global.GetInstance().cardID;
        //for (int i = 0; i < Cards.Length; i++)
        //{
        //    temp.Add(Cards[i].GetComponent<CardInfo>().cardID, i);
        //}
    }

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        GameStart();
    }

    public void GameStart()
    {
        player.HP = MyClass.Instance.HP;
        //int id = (int)Global.GetInstance().cardID["001"];  //攻击
        for (int i = 0; i < MyClass.Instance.playCards.Count; i++)
        {
            GameObject go = Instantiate(MyClass.Instance.playCards[i], target);
            player.cards.Add(go);
        }
        player.GameStart();
        enemy.GameStart();
    }

    public void Click()
    {
        GameView.SetActive(true);
    }

}
