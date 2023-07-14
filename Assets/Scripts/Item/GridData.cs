using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridData : BaseUI
{
    public ItemDataSO itemData;

    private void Update()
    {
        if(isSelect)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (itemData != null)
                {
                    BagController.Instance.BagSelectItem(itemData);
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if(itemData != null)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    BagController.Instance.BagItemCom(itemData);
                }
            }
        }
    }
}
