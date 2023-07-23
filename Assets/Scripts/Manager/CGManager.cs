using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CGManager : Singleton<CGManager>
{ 

    public GameObject[] cgs=new GameObject[8];
    public AudioClip[] audios = new AudioClip[3];
    public AudioSource source;
    public GameObject Panel;
    public GameObject Panel1;
    public bool ischange = false;
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
        if (ischange && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
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
