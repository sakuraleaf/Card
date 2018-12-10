using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {


	// Use this for initialization
	void Start () {

    }
	
    public void Click()
    {
        //Debug.Log("点击了！");
        //SceneMgr.Instance.SwitchScene("GameLoading", transform.parent);
        //Instantiate(clone);
        Global.GetInstance().loadName = "Level";
        Debug.Log(Global.GetInstance().loadName);
        new WaitForSeconds(1f);
        SceneManager.LoadScene("Loading");
        //ResourceMgr.GetInstance().CreateGameObject("Game/UI/GameLoading", false);
    }


}
