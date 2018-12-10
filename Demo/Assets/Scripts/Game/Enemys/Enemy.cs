using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Camera curCamera;

    int shakeCount = 0;
    Vector3 mCurPos;//相机的初始位置
    float radio = 0.2f;

    public void ShakeCameraWithCount()
    {
        mCurPos = curCamera.transform.position;
        shakeCount = Random.Range(5, 7);
        StartCoroutine("ShakeWithCount");
        //ShakeWithCount();
    }
    IEnumerator ShakeWithCount()
    {
        while (shakeCount > 0)
        {
            yield return new WaitForSeconds(0.05f);

            shakeCount--;
            float r = Random.Range(-radio, radio);//随机的震动幅度
            Debug.Log(r);
            if (shakeCount == 0)
            {
                //保证最终回归到原始位置
                //curCamera.transform.position = mCurPos;
            }
            else
            {
                curCamera.transform.position = mCurPos + Vector3.one * r;
                Debug.Log(curCamera.transform.position);
            }

        }
    }

    public float HP = 40;
    public float HPtemp;
    public int damage = 6;
    public float shield = 0;//盾

    public int state = 0;

    public Slider shieldSlider;
    public Text shieldText;

    public Player player;
    public Slider health;
    public Text text;

    public Text message;

    public GameObject popupDamageGo;

    public bool isWaitPlayer = true;

    public void GameStart()
    {
        HPtemp = HP;

        curCamera = Camera.main;

        player = GameObject.Find("Player").GetComponent<Player>();
        text.text = HP.ToString();
        health.value = HP;

        shieldSlider.value = shield * 0.01f;
        shieldText.text = shield.ToString();
    }

    public void OnDamage(int mValue)
    {
        ShakeCameraWithCount();
        if (player.isWeak>0)
            mValue = (int)(mValue * 0.75f);

        if (mValue - shield >= 0)
        {
            mValue -= (int)shield;
            shield = 0;
            shieldSlider.value = 0;
            shieldText.text = "0";
        }
        else
        {
            shield -= mValue;
            shieldSlider.value = shield * 0.01f;
            shieldText.text = shield.ToString();
            mValue = 0;
        }

        GameObject damageGo = Instantiate(popupDamageGo, transform) as GameObject;
        damageGo.GetComponent<PopupDamage>().Value = mValue;
        HP -= mValue;
        health.value = HP/ HPtemp;
        if (health.value < 0) health.value = 0;
        text.text = HP.ToString();
    }

    public void OnShield(int mValue)
    {
        shield += mValue;
        shieldSlider.value = shield * 0.01f;
        shieldText.text = shield.ToString();
    }

    public virtual void StartAI()
    {
       
    }

}
