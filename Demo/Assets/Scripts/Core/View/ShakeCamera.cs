using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour {

    public Camera curCamera;

    bool isCanShake = false;

    int shakeCount = 0;
    Vector3 mCurPos;//相机的初始位置
    float radio = 0.2f;

    // Use this for initialization
    void Start()
    {
        curCamera = Camera.main;
        //ShakeCameraWithCount ();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCanShake)
        {

        }
        ShakeWithCount();
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 40), "ShakeWithCount"))
        {
            ShakeCameraWithCount();
        }
    }

    public void ShakeCameraWithCount()
    {
        mCurPos = curCamera.transform.position;
        shakeCount = Random.Range(5, 14);
        ShakeWithCount();
    }

    void ShakeWithCount()
    {
        while (shakeCount > 0)
        {
            shakeCount--;
            float r = Random.Range(-radio, radio);//随机的震动幅度
            if (shakeCount == 0)
            {
                //保证最终回归到原始位置
                curCamera.transform.position = mCurPos;
            }
            else
            {
                curCamera.transform.position = mCurPos + Vector3.one * r;
            }

        }
    }
}
