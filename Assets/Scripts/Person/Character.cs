using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int ID;
    public string chname;
    public List<DialogueData_So> dialogue;
    public int state;//����״̬��0�����ߣ�1�ɶԻ�
    public Animator anim;
    public GameObject Button;
    public int dialogueindex;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

}
