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


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }



    public void PreDialogue()
    {
        Debug.Log(Player.instance.id);
        GameObject currentcharacter = CharacterManager.instance.Characters[Player.instance.id];
        currentcharacter.transform.GetChild(0).gameObject.SetActive(false);
        dialogueui.gameObject.SetActive(true);
    }
}
