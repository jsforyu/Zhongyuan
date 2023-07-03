using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeteleNotes : MonoBehaviour
{
    private Button deteleButton;
    private void Start()
    {
        deteleButton=GetComponent<Button>();
        deteleButton.onClick.AddListener(Detele);
    }
    public void Detele()
    {
        string deteleString = transform.parent.GetChild(0).GetComponent<Text>().text;
        LineTextCollisionDetection.Instance.DeteleteNote(deteleString);
    }
}
