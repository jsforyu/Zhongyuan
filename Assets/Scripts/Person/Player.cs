using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player instance;

    public float speed;
    
    Rigidbody2D ri;
    public int id; //ǰһ���Ի�npc��id

    private void Awake()
    {
            instance = this;
    }
    void Start()
    {
        Debug.Log("1" + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        //Move();
    }

    private void Move()
    {
        if (Input.GetMouseButtonDown(0)&&  state == 0)
        {
            Vector3 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseposition.z = 0f;
            mouseposition.y = transform.position.y;
            //Debug.Log("2"+mouseposition);
            transform.position = Vector3.MoveTowards(transform.position, mouseposition, speed * Time.deltaTime);
            //Debug.Log("3" + transform.position);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Character>().ID==1)
        {
            id = collision.gameObject.GetComponent<Character>().ID;
            Debug.Log(id);
            Debug.Log("ײ����");
            if (collision.gameObject.GetComponent<Character>().dialogue.currentindex < collision.gameObject.GetComponent<Character>().dialogue.DialogueList.Count)
                collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }                     //Ӧ�ð���״̬���ж�����������
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Character>().ID == 1)
        {
            id = collision.gameObject.GetComponent<Character>().ID;
            Debug.Log(id);
            Debug.Log("�뿪��");
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void PreDialogue()
    {

    }
}
