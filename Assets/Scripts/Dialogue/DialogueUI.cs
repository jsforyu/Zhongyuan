using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueUI : MonoBehaviour
{

    public static DialogueUI instance;
    public Text Name;
    public Text Content;
    public GameObject dialogueui;
    public float textspeed;
    public float fadespeed;
    public float fadedistance;
    public float movespeed;
    public GameObject QuestionButton;
    GameObject currentcharacter;
    int index;
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

        //三种显示效果，追击询问，自动获得，普通效果
        //每句对话有个状态，分别对应三种显示效果，
        if (Input.GetMouseButtonDown(0) && Player.instance.state == 1&&textfinshed)
        {
            if (index >= currentcharacter.GetComponent<Character>().dialogue.DialogueList.Count)
            {
                index = 0;
                textfinshed = false;
                dialogueui.gameObject.SetActive(false);
                Player.instance.state = 0;
            }
            if (currentcharacter.GetComponent<Character>().dialogue.DialogueState[index] == 1)
            {
                StartCoroutine(ShowDialogueButton());
            }
            else if(currentcharacter.GetComponent<Character>().dialogue.DialogueState[index] == 2)
            {
                StartCoroutine(ShowDialogueHigh());
            }
            else StartCoroutine(ShowDialogue());
        }
        if(Input.GetMouseButtonDown(0) && Player.instance.state == 1 && !textfinshed)//整个流程没完全结束
        {
            if(highstate&&currentcharacter.GetComponent<Character>().dialogue.DialogueState[index-1] == 2)//文字消失+自动显示下一行
                StartCoroutine(FadeDialogueHigh());
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
        StartCoroutine(ShowDialogue());//要改成switch
        Player.instance.state = 1;
    }
    IEnumerator ShowDialogue()
    {
        Content.text = "";
        Content.color = Color.black;//还原到原来的颜色
        textfinshed = false;
        for(int i = 0; i < currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].Length; i++)
        {
            Content.text += currentcharacter.GetComponent<Character>().dialogue.DialogueList[index][i];
            yield return new WaitForSeconds(textspeed);
        }
        index++;
        textfinshed = true;
    }

    IEnumerator ShowDialogueButton()
    {
        Content.text = "";
        textfinshed = false;
        Content.color = Color.red;///暂时这个颜色
        for (int i = 0; i < currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].Length; i++)
        {
            Content.text += currentcharacter.GetComponent<Character>().dialogue.DialogueList[index][i];
            yield return new WaitForSeconds(textspeed);
        }
        QuestionButton.SetActive(true);
        index++;
    }

    IEnumerator FadeDialogueHigh()
    {
        Vector2 targetposition = new Vector2(Content.transform.position.x,Content.transform.position.y+fadedistance);
        Vector2.MoveTowards(Content.transform.position, targetposition, movespeed * Time.deltaTime);
        yield return null;
        Content.CrossFadeAlpha(0f, fadespeed, false);
        yield return null;
        highstate = false;
        if (index >= currentcharacter.GetComponent<Character>().dialogue.DialogueList.Count)
        {
            index = 0;
            textfinshed = false;
            dialogueui.gameObject.SetActive(false);
            Player.instance.state = 0;
        }
        textfinshed = true;
    }
    IEnumerator ShowDialogueHigh()
    {
        Content.text = "";
        textfinshed = false;
        Content.color = Color.red;///暂时这个颜色
        for (int i = 0; i < currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].Length; i++)
        {
            Content.text += currentcharacter.GetComponent<Character>().dialogue.DialogueList[index][i];
            yield return new WaitForSeconds(textspeed);
        }
        index++;
        highstate = true;
    }

    public void QuestionNext()
    {
        textfinshed = true;
        QuestionButton.SetActive(false);
        if (index >= currentcharacter.GetComponent<Character>().dialogue.DialogueList.Count)
        {
            index = 0;
            textfinshed = false;
            dialogueui.gameObject.SetActive(false);
            Player.instance.state = 0;
        }
        if (currentcharacter.GetComponent<Character>().dialogue.DialogueState[index] == 1)
        {
            StartCoroutine(ShowDialogueHigh());
        }
        else if (currentcharacter.GetComponent<Character>().dialogue.DialogueState[index] == 2)
        {
            StartCoroutine(ShowDialogueHigh());
        }
        else  StartCoroutine(ShowDialogue()); 
    }
}
