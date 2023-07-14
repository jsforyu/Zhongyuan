using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected bool isSelect;
    public void OnPointerEnter(PointerEventData eventData)
    {
        isSelect = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isSelect = false;
    }
}
