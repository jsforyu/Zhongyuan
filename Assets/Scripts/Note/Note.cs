using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public EvidenceData evidenceData;

    int pagesNum = 0;

    public Text[] npcName;

    public Text[] des;
    private void Update()
    {
        UpdateNotes();
    }
    private void UpdateNotes()
    {
        int currentPage = pagesNum * npcName.Length;
        for (int i = 0; i < npcName.Length; i++)
        {
            if (currentPage + i >= evidenceData.npcName.Count)
            {
                npcName[i].text = string.Empty;
                des[i].text = string.Empty;
            }
            else
            {
                npcName[i].text = evidenceData.npcName[currentPage + i];
                des[i].text = evidenceData.evidence[currentPage + i];
            }

        }
    }
    public void NextPage()
    {
        if (evidenceData.npcName.Count > (pagesNum + 1) * npcName.Length)
            pagesNum++;
    }
    public void LastPage()
    {
        if (pagesNum > 0)
        {
            pagesNum--;
        }
    }
}
