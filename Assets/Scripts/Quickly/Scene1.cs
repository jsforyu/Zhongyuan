using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene1 : Singleton<Scene1>
{
    public GameObject Panel;
    public GameObject panel;
    public GameObject Zimi;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (panel.GetComponent<Image>().color.a <= 0)
        {
            Panel.SetActive(false);
            CharacterManager.instance.Characters[5].SetActive(true);
            DialogueUI.instance.ShowDialogueNPC(5);//出现绿衣男的对话
        }
    }


    public void SwitchState()
    {

    }

    public void Leave()//杨氏，迎儿离开
    {
        Vector3 currentpos1 = CharacterManager.instance.Characters[1].transform.position;
        Vector3 currentpos2 = CharacterManager.instance.Characters[4].transform.position;
        Vector3 targetpos = new Vector3(-32, currentpos1.y, 0);
        Vector3 targetpos1 = new Vector3(-32, currentpos2.y, 0);
        CharacterManager.instance.Characters[1].transform.position = Vector3.MoveTowards(currentpos1, targetpos, Player.instance.speed * Time.deltaTime);
        CharacterManager.instance.Characters[4].transform.position= Vector3.MoveTowards(currentpos2, targetpos1, Player.instance.speed * Time.deltaTime);
        CharacterManager.instance.Characters[2].GetComponent<Character>().state = 1;//离开后孙文可对话
    }

    public void SunMove()//孙文移动
    {
        Vector3 targetpos = CharacterManager.instance.Characters[3].transform.position;//李杰的位置
        Vector3 currentpos= CharacterManager.instance.Characters[2].transform.position;//孙文的位置
        CharacterManager.instance.Characters[2].transform.position= Vector3.MoveTowards(currentpos, targetpos, Player.instance.speed * Time.deltaTime);
        CharacterManager.instance.Characters[3].GetComponent<Character>().state = 1;//李杰可以对话
    }

    public void Move()
    {
        
    }

    public void ChageScene()//屏幕淡入淡出
    {
        Panel.SetActive(true);
        panel.GetComponent<Image>().CrossFadeAlpha(0, 2f, false);
        //使用动画自动弹出对NPC的对话
    }

     public void StartZi()
    {
        Zimi.SetActive(true);
    }
    public void StartLi()
    {
        DialogueUI.instance.ShowDialogueNPC(3);//出现李杰的对话
    }

}
