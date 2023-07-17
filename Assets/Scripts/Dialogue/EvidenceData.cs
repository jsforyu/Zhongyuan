using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EvidenceData_So", menuName = "EvidenceData")]
public class EvidenceData : ScriptableObject
{
    public string[,] Evidences = new string[99, 99];

}
