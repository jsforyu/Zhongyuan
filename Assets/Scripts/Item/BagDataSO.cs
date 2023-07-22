using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Bag",menuName ="New Bag")]
public class BagDataSO : ScriptableObject
{
    public List<ItemDataSO> itemData;

    public int sceneid;
}
