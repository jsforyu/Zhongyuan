using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData_So", menuName = "DialogueData")]
public class DialogueData_So : ScriptableObject
{
    //һ��person���Զ�Ӧ���dialogue
    public List<DialogueText> DialogueList;
    public int currentindex;
    public List<int> Changeindex;
}
