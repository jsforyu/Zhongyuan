using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NoteRecall : BaseUI
{
    [SerializeField] private string[] replace;

    Text currentText; 

    int currentIndex=0;
    private void Start()
    {
        currentText = GetComponent<Text>();
    }
    private void Update()
    {
        ChangeText();
    }
    private void ChangeText()
    {
        if(isSelect)
        {
            if(Input.GetMouseButtonDown(0))
            {
                currentText.text = replace[currentIndex];
                currentIndex++;
                currentIndex %= replace.Length;
            }
        }
    }
}
