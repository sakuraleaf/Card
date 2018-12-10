using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour {

    public GameObject View;
    public Transform target;

    public List<GameObject> Cards = new List<GameObject>();

    private void Start()
    {
        Debug.Log(MyClass.Instance.playCards.Count);
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, MyClass.Instance.AllCards.Count);
            Debug.Log(i);
            Cards.Add(MyClass.Instance.AllCards[randomIndex]);
            GameObject go = Instantiate(MyClass.Instance.AllCards[randomIndex], target);
            go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            go.transform.localPosition = TarLevel(i);
        }
    }

    public void OnClick()
    {
            //假设关卡的名称即为对应场景的名称
            //Application.LoadLevel(level.Name);
            SceneManager.LoadScene("Level");
    }

    public void OnClickA()
    {
        //假设关卡的名称即为对应场景的名称
        //Application.LoadLevel(level.Name);
        SceneManager.LoadScene("Level");
        MyClass.Instance.playCards.Add(Cards[0]);
    }

    public void OnClickB()
    {
        //假设关卡的名称即为对应场景的名称
        //Application.LoadLevel(level.Name);
        SceneManager.LoadScene("Level");
        MyClass.Instance.playCards.Add(Cards[1]);
    }

    public void OnClickC()
    {
        //假设关卡的名称即为对应场景的名称
        //Application.LoadLevel(level.Name);
        SceneManager.LoadScene("Level");
        MyClass.Instance.playCards.Add(Cards[2]);
    }

    Vector3 TarLevel(int i)
    {
        return new Vector3(-140f + i % 5 * 140f, 60f - i / 5 * 160f, 0f);

    }
}
