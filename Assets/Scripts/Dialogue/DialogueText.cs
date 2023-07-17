using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData_So", menuName = "DialogueText")]
public class DialogueText : ScriptableObject
{
    public string dialoguetext;//文字
    public string evidence; //证言
    public string clew; //线索
    public int state;  //对话状态，0为普通，1为追问，2为高亮消失
    public ItemDataSO clewitem;
}
