using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemCom", menuName = "New Compositing")]
public class ItemCompositing : ScriptableObject
{
    public ItemDataSO firstItem;
    public ItemDataSO secondItem;
    public ItemDataSO targetItem;
}
