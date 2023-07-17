using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvidenceUI : MonoBehaviour
{
    public EvidenceData evidenceData;

    public Text[] npcName;

    public Text[] description;

    int index = 0;

    private void Awake()
    {
        EvidenceUpdate();
    }
    public void EvidenceUpdate()
    {
        for (int i = npcName.Length * index; i < npcName.Length * (index + 1); i++)
        {
            if (i < evidenceData.npcName.Count)
            {
                if (evidenceData.npcName[i] != null)
                {
                    npcName[i-4*index].text = evidenceData.npcName[i];
                    description[i-4*index].text = evidenceData.evidence[i];
                }
            }
            else
            {
                npcName[i - 4 * index].text = "Пе";
                description[i-4*index].text = "Пе";
            }
        }
    }

    public void NextButton()
    {
        if(evidenceData.npcName.Count>4*(index+1))
        {
            index++;
        }
        EvidenceUpdate();
    }

    public void LastButton()
    {
        if (index == 0) return;
        index--;
        EvidenceUpdate();
    }
}
