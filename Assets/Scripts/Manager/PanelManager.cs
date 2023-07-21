using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject dialogue;

    private void Start()
    {
        Player.instance.state = 1;
    }

    public void ShowUI()
    {
        this.gameObject.SetActive(false);
        dialogue.SetActive(true);
        DialogueUI.instance.ShowDialogueUI();
    }

    public void ShowUI2()
    {
        this.gameObject.SetActive(false);
    }
}
