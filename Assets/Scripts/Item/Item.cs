using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemDataSO ItemData;
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BagController.Instance.BagAddItem(ItemData);
            Destroy(gameObject);
        }
    }
}
