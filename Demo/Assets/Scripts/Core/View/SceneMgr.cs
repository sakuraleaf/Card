using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr {

    #region    //初始化

    protected static SceneMgr mInstance;

    public static SceneMgr Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new SceneMgr();
            }
            return mInstance;
        }
    }

    #endregion

    private GameObject curren;

    public void SwitchScene(string name, Transform target)
    {
        GameObject scene = ResourceMgr.GetInstance().CreateGameObject("Game/UI/" + name, false, target);
        if (curren != null)
        {
            GameObject.Destroy(curren);
        }
        curren = scene;
    }

}
