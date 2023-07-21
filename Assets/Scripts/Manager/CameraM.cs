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
    private void LateUpdate()
    {
        if (cameraAnim.GetBool("return") == true)
        {
            FollowPlayer();
        }
    }

    public void FollowPlayer()
    {
        transform.position = new Vector3(Player.instance.transform.position.x,0,-10);
        //Vector3 offset =Player.instance.transform.position - transform.position;
        //if(Mathf.Abs(offset.x)>5)
        //transform.position = Vector3.Lerp(transform.position, Player.instance.transform.position - offset, Time.deltaTime * 5);
    }
}
