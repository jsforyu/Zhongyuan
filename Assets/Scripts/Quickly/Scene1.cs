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
            DialogueUI.instance.ShowDialogueNPC(5);//���������еĶԻ�
        }
    }


    public void SwitchState()
    {

    }

    public void Leave()//���ϣ�ӭ���뿪
    {
        Vector3 currentpos1 = CharacterManager.instance.Characters[1].transform.position;
        Vector3 currentpos2 = CharacterManager.instance.Characters[4].transform.position;
        Vector3 targetpos = new Vector3(-32, currentpos1.y, 0);
        Vector3 targetpos1 = new Vector3(-32, currentpos2.y, 0);
        CharacterManager.instance.Characters[1].transform.position = Vector3.MoveTowards(currentpos1, targetpos, Player.instance.speed * Time.deltaTime);
        CharacterManager.instance.Characters[4].transform.position= Vector3.MoveTowards(currentpos2, targetpos1, Player.instance.speed * Time.deltaTime);
        CharacterManager.instance.Characters[2].GetComponent<Character>().state = 1;//�뿪�����ĿɶԻ�
    }

    public void SunMove()//�����ƶ�
    {
        Vector3 targetpos = CharacterManager.instance.Characters[3].transform.position;//��ܵ�λ��
        Vector3 currentpos= CharacterManager.instance.Characters[2].transform.position;//���ĵ�λ��
        CharacterManager.instance.Characters[2].transform.position= Vector3.MoveTowards(currentpos, targetpos, Player.instance.speed * Time.deltaTime);
        CharacterManager.instance.Characters[3].GetComponent<Character>().state = 1;//��ܿ��ԶԻ�
    }

    public void Move()
    {
        
    }

    public void ChageScene()//��Ļ���뵭��
    {
        Panel.SetActive(true);
        panel.GetComponent<Image>().CrossFadeAlpha(0, 2f, false);
        //ʹ�ö����Զ�������NPC�ĶԻ�
    }

     public void StartZi()
    {
        Zimi.SetActive(true);
    }
    public void StartLi()
    {
        DialogueUI.instance.ShowDialogueNPC(3);//������ܵĶԻ�
    }

}
