using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MingWu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string current;

    bool isSelect = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        isSelect= true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isSelect = false;
    }

    // Start is called before the first frame updat

    // Update is called once per frame
    void Update()
    {
        if (isSelect && Input.GetMouseButtonDown(0))
        {
            Zimi.Instance.AddAnswer(current);
        }
    }
}
