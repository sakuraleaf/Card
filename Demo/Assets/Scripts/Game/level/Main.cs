using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml.Serialization;

public class Main : MonoBehaviour
{
    //关卡列表
    private List<Level> m_levels;

    private int target = 0;


    void Start()
    {
        //获取关卡
        m_levels = LevelSystem.LoadLevels();
        //动态生成关卡
        foreach (Level l in m_levels)
        {
            GameObject prefab = (GameObject)Instantiate((Resources.Load("Level") as GameObject));
            //数据绑定
            DataBind(prefab, l);
            //设置父物体
            prefab.transform.SetParent(GameObject.Find("UIRoot/LevelPanel").transform);
            prefab.transform.localPosition = TarLevel(target++);
            prefab.transform.localScale = new Vector3(1, 1, 1);
            //将关卡信息传给关卡
            prefab.GetComponent<LevelEvent>().level = l;
        }
        LevelSystem.SetLevels("level0", true);

    }


    /// <summary>
    /// 数据绑定
    /// </summary>
    void DataBind(GameObject go, Level level)
    {
        //为关卡绑定关卡名称
        go.transform.Find("LevelName").GetComponent<Text>().text = level.Name;
        //为关卡绑定关卡图片
        Texture2D tex2D;
        if (level.UnLock)   
        {
            tex2D = Resources.Load("Image/Lock/nolocked") as Texture2D;
        }
        else
        {
            tex2D = Resources.Load("Image/Lock/locked") as Texture2D;
        }
        Sprite sprite = Sprite.Create(tex2D, new Rect(0, 0, tex2D.width, tex2D.height), new Vector2(0.5F, 0.5F));
        go.transform.GetComponent<Image>().sprite = sprite;
    }
    /// <summary>
    /// UI位置
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    Vector3 TarLevel(int i)
    {
        if (i < 5)
            return new Vector3(-220f + i * 110f, 80f, 0f);
        else
            return new Vector3(-220f + (i - 5) * 110f, -80f, 0f);
    }

}