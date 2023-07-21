using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraM : MonoBehaviour
{
    public Animator cameraAnim;
    public GameObject dialogueui;
    public float movetime;
    void Start()
    {
        cameraAnim.SetBool("return",false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraAnim.GetBool("return") == true)
        {
            FollowPlayer();
        }
    }

    public void EndAnim()
    {
        cameraAnim.SetBool("return",true );
    }

    public void StartDialogue()
    {        
        Player.instance.state = 1;
        dialogueui.SetActive(true);
        DialogueUI.instance.ShowDialogueUI();
        
    }


    public void FollowPlayer()
    {
        Vector3 offset =Player.instance.transform.position - transform.position;
        if(Mathf.Abs(offset.x)>5)
        transform.position = Vector3.Lerp(transform.position, Player.instance.transform.position - offset, Time.deltaTime * 5);
    }
}
