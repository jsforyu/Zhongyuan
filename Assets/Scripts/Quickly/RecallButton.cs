using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecallButton : MonoBehaviour
{
    [SerializeField] private Text text1;

    [SerializeField] private Text text2;

    [SerializeField] private Text text3;

    [SerializeField] private Text text4;

    [SerializeField] private Text text5;

    [SerializeField] private Text text6;

    [SerializeField] private Text text7;

    [SerializeField] private Scene52 scene52;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (text1.text == "Ů" && text2.text == "Ů" && text3.text == "��" && text4.text == "��" && text5.text == "��" && text6.text == "��" && text7.text == "ˮ")
        {
            scene52.enabled = true;
            transform.parent.gameObject.SetActive(false);
        }
    }

    public void recallButton()
    {
        if(text1.text== "Ů"&&text2.text== "Ů"&&text3.text== "��"&&text4.text== "��"&&text5.text== "��"&&text6.text== "��"&&text7.text=="ˮ")
        {
            scene52.enabled = true;
            gameObject.SetActive(false);
        }
    }
}
