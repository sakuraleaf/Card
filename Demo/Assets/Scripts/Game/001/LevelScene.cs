using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScene : MonoBehaviour {

    public void Click()
    {

        Global.GetInstance().loadName = "Game";
        Debug.Log(Global.GetInstance().loadName);
        new WaitForSeconds(1f);
        SceneManager.LoadScene("Loading");
    }
}
