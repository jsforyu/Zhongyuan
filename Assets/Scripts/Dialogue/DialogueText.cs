using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData_So", menuName = "DialogueText")]
public class DialogueText : ScriptableObject
{
    public string npcName; //�Ի�����
    public string dialoguetext;//����
    public string evidence; //֤��
    public string clew; //����
    public int state;  //�Ի�״̬��0Ϊ��ͨ��1Ϊ׷�ʣ�2Ϊ������ʧ
    public ItemDataSO clewitem;
}
