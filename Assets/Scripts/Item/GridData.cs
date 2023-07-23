using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GridData : BaseUI
{
    public ItemDataSO itemData;

    private void Start()
    {
    }
    private void Update()
    {
        if(isSelect)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (itemData != null)
                {
                    BagController.Instance.BagSelectItem(itemData);
                    BagController.Instance.SetCurrentData(itemData);
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                int index = SceneManager.GetActiveScene().buildIndex;
                if (itemData != null&&index>=6)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    BagController.Instance.BagItemCom(itemData);
                }
            }
        }
    }
}
