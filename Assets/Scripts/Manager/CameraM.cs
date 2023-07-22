using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraM : MonoBehaviour
{
    public Animator cameraAnim;
    public GameObject dialogueui;
    public float movetime;
    public BoxCollider2D coll;
    public BoxCollider2D backcoll;

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
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
        if (Player.instance.transform.position.x<=(coll.bounds.size.x/2+backcoll.bounds.min.x))
        {
            transform.position = new Vector3(coll.bounds.size.x / 2 + backcoll.bounds.min.x, 0, transform.position.z);
            return;
        }
        if(Player.instance.transform.position.x >= (backcoll.bounds.max.x-coll.bounds.size.x / 2 ))
        {
            transform.position = new Vector3(backcoll.bounds.max.x - coll.bounds.size.x / 2, 0, transform.position.z);
            return;
        }
        else transform.position = new Vector3(Player.instance.transform.position.x,0,-10);
        //Vector3 offset =Player.instance.transform.position - transform.position;
        //if(Mathf.Abs(offset.x)>5)
        //transform.position = Vector3.Lerp(transform.position, Player.instance.transform.position - offset, Time.deltaTime * 5);
    }
}
