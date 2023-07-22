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

    [SerializeField] private GameObject xuanxiang;

    [SerializeField] private Scene63 scene63;

    private AudioSource audioSource;
    private int index = -1;

    private bool isok = false;

    private float showtime;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && isok&&!BagController.Instance.isstop && !EventSystem.current.IsPointerOverGameObject())
        {
            index++;
            isok = false;
            showtime = 0;
            if (index >= dialogueData_So.DialogueList.Count)
            {
                    xuanxiang.SetActive(true);
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
            isok = true;
        }
    }
    public void Answer1()
    {
        dialogue.text = "";
        npcName.text = "ӭ��";
        dialogue.DOText("�����˸�磬��ʱ���ĪҪ����Ц�ˡ���", 1f);
    }

    public void Answer2()
    {
        scene63.enabled = true;
        xuanxiang.SetActive(false);
        this.enabled = false;

    }

    public void Answer3()
    {
        dialogue.text = "";
        npcName.text = "���";
        dialogue.DOText("����������С�ֵ���ɱ����ң����ǲ���վ��һ�ߵ�������Ц�ɲ�����㿪����", 1f);
    }
}
