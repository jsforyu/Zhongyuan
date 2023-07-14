using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NoteDrag : BaseUI
{

    void Update()
    {
        AddNote();
    }
    public void AddNote()
    {
        if(isSelect)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(!DragManager.Instance.CheckInTargetList(gameObject))
                {
                    DragManager.Instance.AddToTargetList(gameObject);
                    DragManager.Instance.RemoveAtStartList(gameObject);
                    DragManager.Instance.ListRefresh();
                }
                else
                {
                    DragManager.Instance.AddToStartList(gameObject);
                    DragManager.Instance.RemoveAtTargetList(gameObject);
                    DragManager.Instance.ListRefresh();
                }
            }
        }
    }
}
