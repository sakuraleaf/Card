using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyClass : MonoBehaviour {

    static MyClass _instance;

    static public MyClass Instance
    {
        get
        {
            if (_instance == null)
            {
                // 尝试寻找该类的实例。此处不能用GameObject.Find，因为MonoBehaviour继承自Component。
                _instance = Object.FindObjectOfType(typeof(MyClass)) as MyClass;

                if (_instance == null)  // 如果没有找到
                {
                    GameObject go = new GameObject("_MyClass"); // 创建一个新的GameObject
                    DontDestroyOnLoad(go);  // 防止被销毁
                    _instance = go.AddComponent<MyClass>(); // 将实例挂载到GameObject上
                }
            }
            return _instance;
        }
    }
    
    public float  HP;

    public List<GameObject> AllCards = new List<GameObject>();
    public List<GameObject> playCards = new List<GameObject>();
    private void Awake()
    {
        LevelSystem.SetLevels("level0", true);

        HP = 60;
        AllCards = Resources.LoadAll<GameObject>("Game/Cards").ToList();
        //InitCards();
        for (int i = 0; i < 5; i++)
        {
            playCards.Add(AllCards[0]);
        }
        //id = (int)Global.GetInstance().cardID["002"];//防御
        for (int i = 0; i < 5; i++)
        {
            playCards.Add(AllCards[1]);
        }
        playCards.Add(AllCards[2]);
        playCards.Add(AllCards[3]);
        LevelSystem.SetLevels("level0", true);
    }

}
