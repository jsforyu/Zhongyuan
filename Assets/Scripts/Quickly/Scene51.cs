using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene51 : MonoBehaviour
{
    [SerializeField] private DialogueData_So dialogueData_So;

    [SerializeField] private BagDataSO bag;

    [SerializeField] private Text npcName;

    [SerializeField] private Text dialogue;

    [SerializeField] private GameObject evidence;

    [SerializeField] private ItemDataSO targetData;

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
            if (index >= dialogueData_So.DialogueList.Count)
            {
                return;
            }
            else
            {
                dialogue.text = "";
                npcName.text = dialogueData_So.DialogueList[index].npcName;
                dialogue.DOText(dialogueData_So.DialogueList[index].dialoguetext, 1f);
                if (dialogueData_So.DialogueList[index].clewitem != null)
                {
                    audioSource.Play();
                    for (int i = 0; i < bag.itemData.Count; i++)
                    {
                        if (bag.itemData[i] == targetData)
                        {
                            bag.itemData[i] = dialogueData_So.DialogueList[index].clewitem;
                            evidence.SetActive(true);
                            break;
                        }
                        else if (bag.itemData[i] == null)
                        {
                            bag.itemData[i] = dialogueData_So.DialogueList[index].clewitem;
                            evidence.SetActive(true);
                            break;
                        }
                    }
                }
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
