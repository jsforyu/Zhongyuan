using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Scene61 : MonoBehaviour
{
    [SerializeField] private Scene62 scene62;

    [SerializeField] private DialogueData_So dialogueData_So;

    [SerializeField] private BagDataSO bag;

    [SerializeField] private Text npcName;

    [SerializeField] private Text dialogue;

    [SerializeField] private GameObject xiansuo;

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
        if (index >= dialogueData_So.DialogueList.Count)
        {
            xiansuo.SetActive(true);
            scene62.enabled = true;
            this.enabled = false;

        }
        else if (Input.GetMouseButton(0) && isok)
        {
            index++;
            isok = false;
            showtime = 0;
            if (index >= dialogueData_So.DialogueList.Count)
            {
                return;
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
