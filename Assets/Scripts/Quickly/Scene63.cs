using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Scene63 : Singleton<Scene63>
{
    [SerializeField] private DialogueData_So dialogueData_So;

    [SerializeField] private BagDataSO bag;

    [SerializeField] private Text npcName;

    [SerializeField] private Text dialogue;

    [SerializeField] private GameObject xiansuo;

    [SerializeField] private GameObject evidenceUI;

    [SerializeField] private GameObject dialogueUI;

    [SerializeField] private GameObject cg;

    private AudioSource audioSource;

    private int index = 0;

    private bool isok = false;

    private float showtime;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        npcName.text = dialogueData_So.DialogueList[index].npcName;
        dialogue.text = dialogueData_So.DialogueList[index].dialoguetext;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && isok&&!BagController.Instance.isstop && !EventSystem.current.IsPointerOverGameObject())
        {
            index++;
            isok = false;
            showtime = 0;
            if (index >= dialogueData_So.DialogueList.Count )
            {
                int index = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(index + 1);
            }
            if(index==43)
            {
                cg.SetActive(true);
            }
            if(index==3||index==16 || index == 20||index==30)
            {
                BagController.Instance.isstop = true;
                evidenceUI.SetActive(true);
            }
            else if(index==8)
            {
                dialogueUI.SetActive(true);
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
            dialogueUI.SetActive(false);
            isok = true;
        }
    }
}
