using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemDataSO ItemData;
    public int itemstate;//0Ϊ���Ե��飬1Ϊ�����Ե���
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BagController.Instance.BagAddItem(ItemData);
            Destroy(gameObject);
        }
    }

    private void OnMouseEnter()
    {
        if (itemstate == 0)
        {

        }
    }
}
