using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Weak : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text message;
    public void OnPointerEnter(PointerEventData eventData)
    {
        message.text = "你被虚弱了";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        message.text = "";
    }
}
