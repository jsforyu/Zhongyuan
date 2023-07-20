using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData_So", menuName = "DialogueData")]
public class DialogueData_So : ScriptableObject
{
    //一个person可以对应多个dialogue
    public List<DialogueText> DialogueList;
    public int currentindex;
    public List<int> Changeindex;
}
