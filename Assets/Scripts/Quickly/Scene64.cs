using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Scene64 : MonoBehaviour
{
    [SerializeField] private DialogueData_So dialogueData_So;

    [SerializeField] private BagDataSO bag;

    [SerializeField] private Text npcName;

    [SerializeField] private Text dialogue;

    [SerializeField] private GameObject xuanxiang;

    [SerializeField] private GameObject shiju;


    private AudioSource audioSource;
    private int index = -1;

    private bool isok = false;

    private float showtime;

    bool isend = false;

    bool tiaozhuan = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)&&isok&&isend)
        {
            shiju.SetActive(true);
            gameObject.SetActive(false);
        }
        if (Input.GetMouseButton(0) && isok && tiaozhuan)
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index + 1);
        }
        if (Input.GetMouseButton(0) && isok && !BagController.Instance.isstop && !EventSystem.current.IsPointerOverGameObject())
        {
            index++;
            isok = false;
            showtime = 0;
            if (index == dialogueData_So.DialogueList.Count)
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
    public void answer1()
    {
        dialogue.text = "";
        npcName.text = "王兴";
        dialogue.DOText("“有什么我能帮上忙的，只管找我就是。”", 1f);
        isend = true;
        xuanxiang.SetActive(false);
    }
    public void answer2()
    {
        dialogue.text = "";
        npcName.text = "王兴";
        dialogue.DOText("“如此便好，只是还有一事让我有些在意……不如我们换个地方说？”", 1f);
        tiaozhuan = true;
        xuanxiang.SetActive(false);
    }
}
