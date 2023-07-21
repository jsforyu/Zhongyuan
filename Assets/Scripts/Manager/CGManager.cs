using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGManager : MonoBehaviour
{
    
    public GameObject[] cgs=new GameObject[8];
    public AudioClip[] audios = new AudioClip[3];
    public AudioSource source;
    public GameObject Panel;

    int index = 0;
    bool cgfinished;
    void Start()
    {
        source = GetComponent<AudioSource>();
        cgs[index].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        NextCG();
    }
    
    void NextCG()
    {
        if (Input.GetMouseButtonDown(0)&&!cgfinished)
        {
            if (index == 7)
            {
                cgs[index].SetActive(false);
                Panel.SetActive(true);
                cgfinished = true;
                Debug.Log("cg播完");
            }
            else
            {
                cgs[index].SetActive(false);
                cgs[++index].SetActive(true);
                source.PlayOneShot(audios[0]);
            }//翻书的音效
        }
    }

    

}
