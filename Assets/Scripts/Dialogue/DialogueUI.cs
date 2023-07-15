using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueUI : MonoBehaviour
{

    public static DialogueUI instance;
    public Text Name;
    public Text Content;
    public GameObject dialogueui;
    public float textspeed;

    GameObject currentcharacter;
    int index;
    bool textfinshed;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Player.instance.state == 1&&textfinshed)
        {
            StartCoroutine(ShowDialogue());
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
        StartCoroutine(ShowDialogue());
        Player.instance.state = 1;
    }


    IEnumerator ShowDialogue()
    {
        Content.text = "";
        textfinshed = false;
        for(int i = 0; i < currentcharacter.GetComponent<Character>().dialogue.DialogueList[index].Length; i++)
        {
            Content.text += currentcharacter.GetComponent<Character>().dialogue.DialogueList[index][i];
            yield return new WaitForSeconds(textspeed);
        }
        index++;
        if(index>= currentcharacter.GetComponent<Character>().dialogue.DialogueList.Count)
        {
            Player.instance.state = 0;
            dialogueui.gameObject.SetActive(false);
        }
        textfinshed = true;
    }

    


        
}
