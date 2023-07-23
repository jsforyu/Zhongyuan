using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Scene7 : MonoBehaviour
{
    [SerializeField] private DialogueData_So dialogueData_So;

    [SerializeField] private Text npcName;

    [SerializeField] private Text dialogue;

    [SerializeField] private GameObject end;

    [SerializeField] private GameObject cg1;

    [SerializeField] private GameObject cg2;

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

        if (Input.GetMouseButton(0) && isok  && !EventSystem.current.IsPointerOverGameObject())
        {
            index++;
            isok = false;
            showtime = 0;
            if(index==14) { cg1.SetActive(true); }
            if(index==30) { cg2.SetActive(true); audioSource.Play(); }
            if (index >= dialogueData_So.DialogueList.Count)
            {
                end.SetActive(true);
                gameObject.SetActive(false);

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
}
