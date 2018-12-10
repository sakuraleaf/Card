using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMgr : MonoBehaviour {

    #region   初始化
    private static ResourceMgr minstance;
    //获取单例
    public static ResourceMgr GetInstance()
    {
        if(minstance == null)
        {
            minstance = new GameObject("ResourceMgr").AddComponent<ResourceMgr>();
        }
        return minstance;
    }
    private ResourceMgr()
    {
        hashtable = new Hashtable();
    }

    #endregion

    //资源集合
    private Hashtable hashtable;

    public T Load<T>(string path, bool cache) where T : UnityEngine.Object
    {
        if (hashtable.Contains(path))
        {
            return hashtable[path] as T;
        }

        T assetOjb = Resources.Load<T>(path);
        
        if(assetOjb == null)
        {
            Debug.LogError("资源不存在 path=" + path);
        }
        if (cache)
        {
            hashtable.Add(path, assetOjb);
        }
        return assetOjb;
    }
    
    public GameObject CreateGameObject(string path, bool cache, Transform target)
    {
        GameObject assetOjb = Load<GameObject>(path, cache);
        GameObject go = Instantiate(assetOjb, target) as GameObject;
        if(go == null)
        {
            Debug.LogError("创建物体失败 path=" + path);
        }
        return go;
    }
}
