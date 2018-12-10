using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoading : MonoBehaviour {

    public Slider m_slider;  //滑条
    public Text m_text;     //数值
	// Use this for initialization
	void Start () {
        StartCoroutine(Test());
	}
	
    IEnumerator Test()
    {
        int displayProgress = 0;
        int toProgress = 0;
        //AsyncOperation op = Application.LoadLevelAsync(Global.GetInstance().loadName);
        Debug.Log(Global.GetInstance().loadName);
        AsyncOperation op = SceneManager.LoadSceneAsync(Global.GetInstance().loadName);
        //AsyncOperation op = SceneManager.LoadSceneAsync("Game");
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            toProgress = (int)op.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();
            }
        }
        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            SetLoadingPercentage(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
    }
    public void SetLoadingPercentage(int DisplayProgress)
    {
        m_slider.value = DisplayProgress * 0.01f;
        m_text.text = DisplayProgress.ToString() + "%";
    }

}
