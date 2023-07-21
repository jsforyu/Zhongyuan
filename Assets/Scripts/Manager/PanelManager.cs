using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject dialogue;
    public void ShowUI()
    {
        this.gameObject.SetActive(false);
        dialogue.SetActive(true);
        DialogueUI.instance.ShowDialogueUI();
    }
}
