using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exitg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);  
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        { 
           Application.Quit();  
        }
    }
}
