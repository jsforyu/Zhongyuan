using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : Singleton<DragManager>   
{
    [SerializeField] private Transform startTransform;

    [SerializeField] private Transform targetTransform;

     public List<GameObject> startLists;

     public List<GameObject> targetLists;
    public void ListRefresh()
    {
        for(int i=0;i<startLists.Count; i++)
        {
            startLists[i].transform.SetParent(startTransform, false);
        }
        for(int i=0;i<targetLists.Count;i++)
        {
            targetLists[i].transform.SetParent(targetTransform, false);
        }
    }
    public bool CheckInTargetList(GameObject obj)
    {
        if(targetLists != null)
        {
            if(targetLists.Contains(obj))
            {
                return true;
            }
        }
        return false;
    }

    public void AddToTargetList(GameObject obj)
    {
        targetLists.Add(obj);
    }
    public void RemoveAtTargetList(GameObject obj)
    {
        targetLists.Remove(obj);
    }

    public void RemoveAtStartList(GameObject obj)
    {
        startLists.Remove(obj);
    }
    public bool CheckInStartList(GameObject obj)
    {
        if (startLists != null)
        {
            if (startLists.Contains(obj))
            {
                return true;
            }
        }
        return false;
    }
    public void AddToStartList(GameObject obj)
    {
        startLists.Add(obj);
    }
}
