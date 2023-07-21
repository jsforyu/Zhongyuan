using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scene62 : MonoBehaviour
{
    [SerializeField] private DialogueData_So dialogueData_So;

    [SerializeField] private BagDataSO bag;

    [SerializeField] private Text npcName;

    [SerializeField] private Text dialogue;

    [SerializeField] private GameObject xiansuo;

    [SerializeField] private GameObject[] xuanxiang;

    [SerializeField] private Scene63 scene63;

    private AudioSource audioSource;
    private int index = 0;

    private bool isok = false;

    private float showtime;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        npcName.text = dialogueData_So.DialogueList[index].npcName;
        dialogue.DOText(dialogueData_So.DialogueList[index].dialoguetext, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && isok)
        {
            index++;
            isok = false;
            showtime = 0;
            if (index >= dialogueData_So.DialogueList.Count&& !EventSystem.current.IsPointerOverGameObject())
            {
                npcName.text = dialogueData_So.DialogueList[index-1].npcName;
                dialogue.text = dialogueData_So.DialogueList[index - 1].dialoguetext;
                foreach (GameObject xuan in xuanxiang)
                {
                    xuan.SetActive(true);
                }
            }
            else
            {
                dialogue.text = "";
                npcName.text = dialogueData_So.DialogueList[index].npcName;
                dialogue.DOText(dialogueData_So.DialogueList[index].dialoguetext, 1f);
            }
        }
        showtime += Time.deltaTime;
        if (showtime >= 2)
        {
            showtime = 0;
            isok = true;
        }
    }
    public void Answer1()
    {
        npcName.text = "ӭ��";
        dialogue.DOText("�����˸�磬��ʱ���ĪҪ����Ц�ˡ���", 1f);
        foreach(GameObject xuan in xuanxiang)
        {
            xuan.SetActive(false);
        }
    }

    public void Answer2()
    {
        scene63.enabled = true;
        this.enabled = false;

    }

    public void Answer3()
    {
        npcName.text = "���";
        dialogue.DOText("����������С�ֵ���ɱ����ң����ǲ���վ��һ�ߵ�������Ц�ɲ�����㿪����", 1f);
        foreach (GameObject xuan in xuanxiang)
        {
            xuan.SetActive(false);
        }
    }
}
