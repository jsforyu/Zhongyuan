using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Player : Character
{
    public static Player instance;
    public float speed;

    private Vector3 target;

    Rigidbody2D ri;

    public int id; //前一个对话npc的id

    Vector3 mouseposition;

    private void Awake()
    {
            instance = this;
        target = transform.position;
    }
    void Start()
    {
        ri = GetComponent<Rigidbody2D>();
        Debug.Log("1" + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Vector3.Distance(transform.position, mouseposition) < 0.01f)
        {
            transform.position = mouseposition;
            anim.SetBool("walk", false);
        }
        */

        //Move();


        //if (state == 1)
        //{
        //    transform.position = transform.position;
        //}
        if(state==0&& Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("点到UI");
                return;
            }
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if ((target.x > transform.position.x &&transform.localScale.x>0)||(target.x < transform.position.x && transform.localScale.x < 0))
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            target.y = transform.position.y;
            target.z = 0;
        }
        if(state==0) Move();
    }
    public float Speed;
    private bool isFirstClicked;

    //private void FixedUpdate()
    //{
    //    if(state != 0)
    //    {
    //        return;
    //    }
    //    if (state==0&& Input.GetMouseButtonDown(0))
    //    {
    //        _targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        _targetPos.z = 0;
    //        if ((_targetPos.x < transform.position.x && transform.localScale.x < 0)||(_targetPos.x > transform.position.x && transform.localScale.x > 0))
    //        {
    //            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    //        }
    //        //dir = _targetPos - gameObject.transform.position;
    //        isFirstClicked = true;
    //    }

    //    float distance = gameObject.transform.position.x - _targetPos.x;

    //    if (isFirstClicked )
    //    {
    //        if (Mathf.Abs(distance) > 0.5f)
    //        {
    //            Vector3 dir = (_targetPos - gameObject.transform.position).normalized;
    //            dir.z = 0;
    //            dir.y = 0;
    //            //ri.velocity = dir * speed * Time.deltaTime;
    //            ri.AddForce(dir * speed);
    //            anim.SetBool("walk", true);
    //        }
    //        else
    //        {
    //            ri.velocity = Vector2.zero;
    //            _targetPos = gameObject.transform.position;
    //            isFirstClicked = false;
    //            //gameObject.transform.position = _targetPos;
    //            anim.SetBool("walk", false);
    //        }
    //    }
    //}
    private Vector3 _targetPos;
    //private void Move()
    //{
    //    if (Input.GetMouseButtonDown(0)&&  state == 0)
    //    {
    //        mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        mouseposition.z = 0f;
    //        mouseposition.y = transform.position.y;
    //        //Debug.Log("2"+mouseposition);
    //            anim.SetBool("walk", true);
    //            ri.MovePosition(mouseposition);
    //        Vector3 dir = Vector3.Normalize(mouseposition - ri.gameObject.transform.position);
    //        ri.AddForce(dir * 10);
    //        //Debug.Log("3" + transform.position);
    //    }

    //}
    private void Move()
    {
        if(Vector3.Distance(transform.position, target)<0.1f)
        {
            anim.SetBool("walk", false);
            transform.position = target;
        }
        if(transform.position!=target)
        {
            anim.SetBool("walk", true);
            transform.position =Vector3.MoveTowards(transform.position,target,speed*Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Character>() != null)
        {
            id = collision.gameObject.GetComponent<Character>().ID;
            if (collision.gameObject.GetComponent<Character>().dialogue.currentindex >=
                collision.gameObject.GetComponent<Character>().dialogue.DialogueList.Count)
                return;
            Debug.Log(id);
            Debug.Log("撞上了");
            if (collision.gameObject.GetComponent<Character>().dialogue.currentindex < collision.gameObject.GetComponent<Character>().dialogue.DialogueList.Count)
            {
                Debug.Log("准备对话");
                Vector3 targetposition = new Vector3(collision.gameObject.transform.position.x - 2, collision.gameObject.transform.position.y + 8, 0);
                if (collision.gameObject.GetComponent<Character>().Button == null) return;
                if (collision.gameObject.GetComponent<Character>().state == 1)//可对话状态
                {
                    collision.gameObject.GetComponent<Character>().Button.transform.position = targetposition;
                    collision.gameObject.GetComponent<Character>().Button.SetActive(true);
                }

            }
        }
        if (collision.gameObject.tag == "Edge")
        {
            state =1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Character>()!=null)
        {
            id = collision.gameObject.GetComponent<Character>().ID;
            Debug.Log(id);
            Debug.Log("离开了");
            if(collision.gameObject.GetComponent<Character>().Button!=null)
            collision.gameObject.GetComponent<Character>().Button.SetActive(false);
        }
        if (collision.gameObject.tag == "Edge")
        {
            state = 0;
        }
    }
}
