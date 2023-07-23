using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

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
    public GameObject currentcharacter;
    int index;
    int eindex;
    public bool textfinshed;
    bool highstate=false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        //SaveManager.Instance.Save(evidences,"evidences");//存储证言
        //三种显示效果，追击询问，自动获得，普通效果
        //每句对话有个状态，分别对应三种显示效果，
        if (Input.GetMouseButtonDown(0) && Player.instance.state == 1&&textfinshed)
        {
            SwitchDialogue();
        }
        if(Input.GetMouseButtonDown(0) && Player.instance.state == 1 && !textfinshed)//整个流程没完全结束
        {
            if (highstate && currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].currentindex - 1].state == 2)//文字消失+自动显示下一行
            {
                fadeText.SetTrigger("Fade");
            }
        }
    }
    public void PreDialogue()
    {
        Player.instance.state = 1;
        Debug.Log(Player.instance.id);
        currentcharacter = CharacterManager.instance.Characters[Player.instance.id];
        Name.text = currentcharacter.GetComponent<Character>().chname;
        currentcharacter.GetComponent<Character>().Button.SetActive(false);
        dialogueui.gameObject.SetActive(true);
        textfinshed = true;
        SwitchDialogue();//要改成switch
    }


    public void ShowDialogueUI() //player专用
    {
        currentcharacter = CharacterManager.instance.Characters[0];
        dialogueui.gameObject.SetActive(true);
        textfinshed = true;
        StartCoroutine(ShowDialogue());
    }
    public void ShowDialogueNPC(int npcid)
    {
        currentcharacter = CharacterManager.instance.Characters[npcid];
        dialogueui.gameObject.SetActive(true);
        textfinshed = true;
        StartCoroutine(ShowDialogue());
    }

    IEnumerator ShowDialogue()
    {
        Content.text = "";
        Content.color = Color.black;//还原到原来的颜色
        Name.color = Color.black;
        textfinshed = false;
        index = currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].currentindex;
        Name.text = currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].npcName;
        for (int i = 0; i < currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].dialoguetext.Length; i++)
        {
            Content.text +=currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].dialoguetext[i];
            yield return new WaitForSeconds(textspeed);
        }
        currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].currentindex++;
        textfinshed = true;
    }

    IEnumerator ShowDialogueButton()//高亮后出现按钮
    {
        Content.text = "";
        Name.color = Color.black;
        textfinshed = false;
        int clewindex=0;
        index = currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].currentindex;
        Name.text = currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].npcName;
        Debug.Log(index);
        if (currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].clewitem !=null)
            BagController.Instance.BagAddItem(currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].clewitem);
        for (int i = 0; i < currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].dialoguetext.Length; i++)
        {
            if ((clewindex< currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].clew.Length) &&   
                (currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].dialoguetext[i] ==
                currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].clew[clewindex]))
            {
                Content.text +=
                      "<color=red>"+ currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].dialoguetext[i] + "</color>";
                clewindex++;
            }
            else Content.text +=currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].dialoguetext[i];
            yield return new WaitForSeconds(textspeed);
        }
        QuestionButton.SetActive(true);
        currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].currentindex++;
    }
    IEnumerator ShowDialogueHigh()//高亮，上浮消失
    {
        Content.text = "";
        textfinshed = false;
        Name.color = Color.black;
        Name.text = currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].npcName; index = currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].currentindex;
        if (currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].evidence != "")
        { evidences.npcName.Add(currentcharacter.GetComponent<Character>().chname);
         evidences.evidence.Add(currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].evidence);
        }
        int evidenceindex = 0;
        for (int i = 0; i < currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].dialoguetext.Length; i++)
        {
            if ((evidenceindex < currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].evidence.Length)
                &&
                (currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].dialoguetext[i] ==
                currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].evidence[evidenceindex]))
            {
                Content.text +=
                      "<color=red>" + currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].dialoguetext[i] + "</color>";
                evidenceindex++;
            }
            else Content.text +=currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[index].dialoguetext[i];
            yield return new WaitForSeconds(textspeed);
        }
        currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].currentindex++;
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
        highstate = false;//自动显示下一句话
        SwitchDialogue();
    }

    void SwitchDialogue()
    {
        if (currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].currentindex >= currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList.Count)
        {

            if (SceneManager.GetActiveScene().name == "Scene1" && currentcharacter.GetComponent<Character>().ID == 1)
            {
                Scene1.Instance.StartZi();//开始猜字谜
                currentcharacter.GetComponent<Character>().dialogueindex++;
                textfinshed = false;
                dialogueui.gameObject.SetActive(false);
                Player.instance.state = 0;
            }
            if (SceneManager.GetActiveScene().name == "Scene1" && currentcharacter.GetComponent<Character>().ID == 4)
            {
                Scene1.Instance.Leave();//迎儿离开
            }
            if (SceneManager.GetActiveScene().name == "Scene1" && currentcharacter.GetComponent<Character>().ID == 2&& currentcharacter.GetComponent<Character>().dialogueindex==0)
            {
                Scene1.Instance.SunMove();//孙文离开，李杰可以对话
            }
            if (SceneManager.GetActiveScene().name == "Scene1" && currentcharacter.GetComponent<Character>().ID == 3&&currentcharacter.GetComponent<Character>().dialogueindex == 1)
            {
                Scene1.Instance.ChageScene();//李杰第二次对话后黑屏增加NPC，
            }
            if (SceneManager.GetActiveScene().name == "Scene1" && currentcharacter.GetComponent<Character>().ID == 5) { 
                CharacterManager.instance.Characters[2].GetComponent<Character>().state=1;//绿衣男对话后孙文可以对话
            }
            if (SceneManager.GetActiveScene().name == "Scene1" && currentcharacter.GetComponent<Character>().ID == 2 && currentcharacter.GetComponent<Character>().dialogueindex == 1)
            {
                //对话完毕，转场
            }
            currentcharacter.GetComponent<Character>().dialogueindex++;
            textfinshed = false;
            dialogueui.gameObject.SetActive(false);
            Player.instance.state = 0;
        }
        else if (currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].currentindex].state == 1)
        {
            StartCoroutine(ShowDialogueButton());
        }
        else if (currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].DialogueList[currentcharacter.GetComponent<Character>().dialogue[currentcharacter.GetComponent<Character>().dialogueindex].currentindex].state == 2)
        {
            StartCoroutine(ShowDialogueHigh());
        }
        else StartCoroutine(ShowDialogue());
    }

}

