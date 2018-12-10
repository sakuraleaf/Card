using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCard : MonoBehaviour {

    public GameObject View;
    public Transform target;

    public Slider Viewslider;

    private void Start()
    {
        Debug.Log(MyClass.Instance.playCards.Count);
        for (int i = 0; i < MyClass.Instance.playCards.Count; i++)
        {
            Debug.Log(i);
            GameObject go = Instantiate(MyClass.Instance.playCards[i], target);
            go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            go.transform.localPosition = TarLevel(i);
        }
    }

    public void Click()
    {
        View.SetActive(false);
    }

    public void MoveView()
    {
        int tmp = MyClass.Instance.playCards.Count / 5 * 80;
        target.localPosition = new Vector3(0f, tmp * Viewslider.value, 0f);
    }

    Vector3 TarLevel(int i)
    {
        return new Vector3(-220f + i % 5 * 110f, 80f - i / 5 * 160f, 0f);
        
    }
}
