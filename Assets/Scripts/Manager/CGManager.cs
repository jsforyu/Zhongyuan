using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGManager : MonoBehaviour
{
    
    public GameObject[] cgs=new GameObject[11];
    public AudioClip[] audios = new AudioClip[11];
    public AudioSource source;
    int index = 0;

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
        if (Input.GetMouseButtonDown(0))
        {
            cgs[index].GetComponent<AudioSource>().clip = audios[0];
            cgs[index].GetComponent<AudioSource>().Play();
            cgs[index].SetActive(false);
            cgs[++index].SetActive(true);
            //source.PlayOneShot(audios[0]);//翻书的音效
            if (index >= 11)
            {
                //切换下一个场景
            }
        }
    }
}
