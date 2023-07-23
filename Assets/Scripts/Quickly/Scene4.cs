using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene4 : MonoBehaviour
{
    [SerializeField] private DialogueData_So dialogueData_So;

    [SerializeField] private BagDataSO bag;

    [SerializeField] private Text npcName;

    [SerializeField] private Text dialogue;

    [SerializeField] private GameObject evidence;

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
        if (Input.GetMouseButton(0) && isok)
        {
            evidence.SetActive(false);
            index++;
            isok = false;
            showtime = 0;
            if (index ==) { }
            if (index ==) { }
            if (index >= dialogueData_So.DialogueList.Count)
            {
                int temp = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(temp + 1);
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
}
