using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemDataSO : ScriptableObject
{
    public Sprite itemSprite;

    [TextArea]
    public string itemDescription;
}
