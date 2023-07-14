using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    public List<GameObject> Characters;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
