using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChageManager : MonoBehaviour
{
    public List<GameObject> Texts = new List<GameObject>();
    int index;
    void Start()
    {
        Texts[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Next()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (index == 2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            Texts[index].SetActive(false);
            Texts[++index].SetActive(true);
        }
    }
}
