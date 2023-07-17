using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemDataSO ItemData;
    public int itemstate;//0为可以调查，1为不可以调查


    bool isstay;
    private void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0)&&isstay)
        {
            isstay = false;
            this.transform.localScale = new Vector3(1, 1, 1);
            DialogueUI.instance.dialogueui.SetActive(true);
        }
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
            isstay = true;
            this.transform.localScale = new Vector3(2, 2, 1);
        }
    }

    private void OnMouseExit()
    {
        if (itemstate == 0)
        {
            isstay = false;
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
