using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int ID;
    public string chname;
    public DialogueData_So dialogue;
    public int state;
    public Animator anim;
    public GameObject Button;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

}
