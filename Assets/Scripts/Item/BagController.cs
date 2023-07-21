using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BagController : Singleton<BagController>
{
    public Scene63 scene63;

    [SerializeField] private BagDataSO bagData;

    [SerializeField] private GameObject[] grids;

    [Header("物品描述")]

    [SerializeField] private Image itemSprite;

    [SerializeField] private Text itemDes;

    [Header("物品合成")]

    [SerializeField] private List<ItemCompositing> itemCompositings;

    [SerializeField] private Image itemComSprite_01;

    [SerializeField] private Image itemComSprite_02;

    [Header("出示证物")]

    [SerializeField] private ItemDataSO[] itemDatas;

    private ItemDataSO currentData;

    private ItemDataSO itemCom_01;

    private ItemDataSO itemCom_02;

    private bool isopen = false;

    private List<ItemDataSO> itemDataLists = new List<ItemDataSO>();

    private int itemIndex = 0;


    private void Start()
    {
        BagInit();
    }
    private void Update()
    {
        BagOpen();
    }
    private void BagOpen()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            isopen = !isopen;
            transform.GetChild(0).gameObject.SetActive(isopen);
        }
    }
    private void BagInit()
    {
        itemDataLists = bagData.itemData;
        BagUpdate();
    }

    public void BagAddItem(ItemDataSO item)
    {
        for(int i=0;i<itemDataLists.Count;i++)
        {
            if (itemDataLists[i] == null) 
            {
                itemDataLists[i] = item;
                break;
            }
        }
        BagUpdate();
    }

    public void BagUpdate()
    {
        for(int i=0;i<itemDataLists.Count;i++)
        {
            if (itemDataLists[i] != null)
            {
                grids[i].transform.GetChild(0).gameObject.SetActive(true);
                grids[i].GetComponent<GridData>().itemData = itemDataLists[i];
                grids[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite=itemDataLists[i].itemSprite;
            }
            else
            {
                grids[i].transform.GetChild(0).gameObject.SetActive(false);
                grids[i].GetComponent<GridData>().itemData = null;
            }
        }
    }

    public void BagSelectItem(ItemDataSO itemdata)
    {
        itemSprite.color = new Color(1, 1, 1, 1);
        itemSprite.sprite = itemdata.itemSprite;
        itemDes.text = itemdata.itemDescription;
    }

    public void BagItemCom(ItemDataSO itemdata)
    {
        if(itemCom_01 == null)
        {
            itemCom_01 = itemdata;
            itemComSprite_01.sprite = itemdata.itemSprite;
            itemComSprite_01.color = new Color(1, 1, 1, 1);
        }
        else
        {
            itemCom_02 = itemdata;
            itemComSprite_02.sprite = itemdata.itemSprite;
        }
        if(itemCom_01!=null&&itemCom_02!=null)
        {
            foreach(var itemcompositings in itemCompositings)
            {
                if(itemCom_01==itemcompositings.firstItem)
                {
                    if(itemCom_02==itemcompositings.secondItem)
                    {
                        for(int i=0;i<itemDataLists.Count;i++)
                        {
                            if (itemDataLists[i] == itemCom_01 || itemDataLists[i]==itemCom_02)
                            {
                                itemDataLists[i] = null;
                            }
                        }
                       for(int i=0;i<itemDataLists.Count;i++)
                        {
                            if (itemDataLists[i]==null)
                            {
                                itemDataLists[i] = itemcompositings.targetItem; 
                                break;
                            }
                        }
                        break;
                    }
                }
                if(itemCom_01==itemcompositings.secondItem)
                {
                    if(itemCom_02==itemcompositings.firstItem)
                    {
                        for (int i = 0; i < itemDataLists.Count; i++)
                        {
                            if (itemDataLists[i] == itemCom_01 || itemDataLists[i] == itemCom_02)
                            {
                                itemDataLists[i] = null;
                            }
                        }
                        for (int i = 0; i < itemDataLists.Count; i++)
                        {
                            if (itemDataLists[i] == null)
                            {
                                itemDataLists[i] = itemcompositings.targetItem;
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            itemCom_01 = itemCom_02 = null;
            itemComSprite_01.sprite = itemComSprite_02.sprite = null;
            itemComSprite_01.color =itemComSprite_02.color= new Color(1, 1, 1, 0);
            BagUpdate();
        }
    }

    public void SetCurrentData(ItemDataSO itemData)
    {
        currentData = itemData;
    }
    
    public void JudgeItem()
    {
        if (currentData == itemDatas[itemIndex])
        {
            for(int i=0;i<itemDataLists.Count;i++)
            {
                if (itemDataLists[i]==currentData)
                {
                    itemDataLists[i] = null;
                    itemSprite.color=new Color(1,1,1,0);
                    itemDes.text = "";
                    itemIndex++;
                    Scene63.Instance.isstop = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
