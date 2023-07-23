using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Caidan : MonoBehaviour
{
    [SerializeField] private GameObject[] gongneng;
    bool isOpen = false;

    Animator animator;
        private void Start()
    {
        animator = GetComponent<Animator>();    
    }
    // Start is called before the first frame update
    public void OpenMain()
    {
        isOpen = !isOpen;
        if ( isOpen )
        {
            animator.SetTrigger("Show");
        }
        else
        {
            animator.SetTrigger("Hide");
        }
    }

    public void OpenGongneng0()
    {
        if (gongneng[0].activeSelf)
        { gongneng[0].SetActive(false); }
        else
        {
            gongneng[0].SetActive(true);
        }
        for(int i=0;i<gongneng.Length;i++) 
        {
            if (i == 0) return;
            gongneng[i].SetActive(false);
        }
    }
    public void OpenGongneng1()
    {
        if (gongneng[1].activeSelf)
        { gongneng[1].SetActive(false); }
        else
        {
            gongneng[1].SetActive(true);
        }
        for (int i = 0; i < gongneng.Length; i++)
        {
            if (i == 1) return;
            gongneng[i].SetActive(false);
        }
    }
    public void OpenGongneng2()
    {
        if (gongneng[2].activeSelf)
        { gongneng[2].SetActive(false); }
        else
        {
            gongneng[2].SetActive(true);
        }
        for (int i = 0; i < gongneng.Length; i++)
        {
            if (i == 2) return;
            gongneng[i].SetActive(false);
        }
    }
}
