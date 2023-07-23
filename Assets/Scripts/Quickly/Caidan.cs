using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Caidan : MonoBehaviour
{
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
}
