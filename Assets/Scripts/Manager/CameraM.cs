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

            if (Player.instance.transform.position != transform.position)//相机和角色位置不相等的时候
            {
                transform.position = Vector3.Lerp(transform.position, Player.instance.transform.position, movetime * Time.deltaTime);
            }
    }
}
