using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Animations;


public class DialogueUI : MonoBehaviour
{

    public static DialogueUI instance;
    public Text Name;
    public Text Content;
    public GameObject dialogueui;
    public float textspeed;
    public GameObject QuestionButton;
    public Animator fadeText;
    public EvidenceData evidences;
    GameObject currentcharacter;
    int index;
    int eindex;
    bool textfinshed;
    bool highstate=false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    private void Update()
    {

        //������ʾЧ����׷��ѯ�ʣ��Զ���ã���ͨЧ��
        //ÿ��Ի��и�״̬���ֱ��Ӧ������ʾЧ����
        if (Input.GetMouseButtonDown(0) && Player.instance.state == 1&&textfinshed)
        {
            SwitchDialogue();
        }
        if(Input.GetMouseButtonDown(0) && Player.instance.state == 1 && !textfinshed)//��������û��ȫ����
        {
            if (highstate && currentcharacter.GetComponent<Character>().dialogue.DialogueList[currentcharacter.GetComponent<Character>().dialogue.currentindex - 1].state == 2)//������ʧ+�Զ���ʾ��һ��
            {
                fadeText.SetTrigger("Fade");
            }
        }
    }
    public void PreDialogue()
    {
        Debug.Log(Player.instance.id);
        currentcharacter = CharacterManager.instance.Characters[Player.instance.id];
        Name.text = currentcharacter.GetComponent<Character>().chname;
        currentcharacter.transform.GetChild(0).gameObject.SetActive(false);
        dialogueui.gameObject.SetActive(true);
        textfinshed = true;
        SwitchDialogue();//Ҫ�ĳ�switch
        Player.instance.state = 1;
    }
    IEnumerator ShowDialogue()
    {
        Content.text = "";
        Content.color = Color.black;//��ԭ��ԭ������ɫ
        textfinshed = false;
        index = currentcharacter.GetComponent<Character>().dialogue.currentindex;
        for (int i = 0; i < currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].dialoguetext.Length; i++)
        {
            Content.text +=currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].dialoguetext[i];
            yield return new WaitForSeconds(textspeed);
        }
        currentcharacter.GetComponent<Character>().dialogue.currentindex++;
        textfinshed = true;
    }

    IEnumerator ShowDialogueButton()//��������ְ�ť
    {
        Content.text = "";
        textfinshed = false;
        int clewindex=0;
        index = currentcharacter.GetComponent<Character>().dialogue.currentindex;
        Debug.Log(index);
        if (currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].clewitem !=null)
            BagController.Instance.BagAddItem(currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].clewitem);
        for (int i = 0; i < currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].dialoguetext.Length; i++)
        {
            if ((clewindex< currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].clew.Length) &&   
                (currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].dialoguetext[i] ==
                currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].clew[clewindex]))
            {
                Content.text +=
                      "<color=red>"+ currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].dialoguetext[i] + "</color>";
                clewindex++;
            }
            else Content.text +=currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].dialoguetext[i];
            yield return new WaitForSeconds(textspeed);
        }
        QuestionButton.SetActive(true);
        currentcharacter.GetComponent<Character>().dialogue.currentindex++;
    }
    IEnumerator ShowDialogueHigh()//�������ϸ���ʧ
    {
        Content.text = "";
        textfinshed = false;
        index = currentcharacter.GetComponent<Character>().dialogue.currentindex;
        if (currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].evidence != "")
        { evidences.Evidences[eindex,0] = currentcharacter.GetComponent<Character>().chname;
         evidences.Evidences[eindex, 1] = currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].evidence;
        }
        Debug.Log(evidences.Evidences[eindex, 0]+ evidences.Evidences[eindex, 1]);
        int evidenceindex = 0;
        for (int i = 0; i < currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].dialoguetext.Length; i++)
        {
            if ((evidenceindex < currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].evidence.Length)
                &&
                (currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].dialoguetext[i] ==
                currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].evidence[evidenceindex]))
            {
                Content.text +=
                      "<color=red>" + currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].dialoguetext[i] + "</color>";
                evidenceindex++;
            }
            else Content.text +=currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].dialoguetext[i];
            yield return new WaitForSeconds(textspeed);
        }
        currentcharacter.GetComponent<Character>().dialogue.currentindex++;
        highstate = true;
    }
    public void QuestionNext()
    {
        QuestionButton.SetActive(false);
        SwitchDialogue();
    }

    public void FadeContinue()
    {
        Content.text = "";
    }

    public void FadeEnd()
    {
        Content.color = new Color(Color.black.r, Color.black.g, Color.black.b,255);
        Debug.Log(Content.color.a);
        highstate = false;//�Զ���ʾ��һ�仰
        SwitchDialogue();
    }

    void SwitchDialogue()
    {
        if (currentcharacter.GetComponent<Character>().dialogue.currentindex >= currentcharacter.GetComponent<Character>().dialogue.DialogueList.Count)
        {
            textfinshed = false;
            dialogueui.gameObject.SetActive(false);
            Player.instance.state = 0;
        }
        else if (currentcharacter.GetComponent<Character>().dialogue.DialogueList[currentcharacter.GetComponent<Character>().dialogue.currentindex].state == 1)
        {
            StartCoroutine(ShowDialogueButton());
        }
        else if (currentcharacter.GetComponent<Character>().dialogue.DialogueList[currentcharacter.GetComponent<Character>().dialogue.currentindex].state == 2)
        {
            StartCoroutine(ShowDialogueHigh());
        }
        else StartCoroutine(ShowDialogue());
    }
}

