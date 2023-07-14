using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace HKZ
{
    public class RotationDiagram2D : MonoBehaviour
    {
        public Vector2 ItemSize;
        public Sprite[] ItemSprites;
        public float offset;
        public float ScaleTimesMin;
        public float ScaleTimesMax;

        private List<RotationDiagramItem> itemList;
        private List<ItemPosDate> posDateList;
        private int currentIndex = 1;
        private int max = 2;
        private int min = 0;

        private void Awake()
        {
            itemList = new List<RotationDiagramItem>();
            posDateList = new List<ItemPosDate>();
            CreateItem();
            CalulateDate();
            SetItemDate();
        }

        private void Start()
        {
            ///btns = transform.GetComponentInChildren<Button>();
        }

        private GameObject CreateTemplate()
        {
            GameObject item = new GameObject("Template");
            item.AddComponent<RectTransform>().sizeDelta = ItemSize;
            item.AddComponent<Image>();
            item.AddComponent<RotationDiagramItem>();
            item.AddComponent<Button>();
            return item;
        }

        private void CreateItem()
        {
            GameObject template = CreateTemplate();
            RotationDiagramItem itemTemplate = null;
            //Resurces->prefab->实例化->gameObject
            for (int i = 0; i < 3; i++)
            {
                itemTemplate = Instantiate(template).GetComponent<RotationDiagramItem>();
                itemTemplate.SetParent(transform);
                int initialImageIndex = i;
                itemTemplate.SetSprite(ItemSprites[initialImageIndex]);
                itemTemplate.ImageIndex = initialImageIndex;
                itemTemplate.AddMoveListener(Change);
                itemList.Add(itemTemplate);
            }

            Destroy(template);
        }

        private void Change(float offsetX)
        {
            int symbol = offsetX > 0 ? 1 : -1;
            Change(symbol);
        }

        private void Change(int symbol)
        {
            int totalImages = ItemSprites.Length; // 图片总数
            foreach (RotationDiagramItem item in itemList)
            {
                item.ChangeId(symbol, itemList.Count);
                if (symbol > 0)
                {
                    if (item.PosId == 2)
                    {
                        int newIndex = min-1;
                        if (newIndex < 0)
                        {
                            newIndex = totalImages - 1;
                        }
                        else if (newIndex >= totalImages)
                        {
                            newIndex = 0;
                        }
                        min = newIndex;
                        item.SetSprite(ItemSprites[newIndex]);
                        item.ImageIndex = newIndex;
                    }
                    if(item.PosId==1)
                    {
                        max = item.ImageIndex;
                    }
                }
                else
                {
                    if (item.PosId == 1)
                    {
                        int newIndex = max+1;
                        if (newIndex < 0)
                        {
                            newIndex = totalImages - 1;
                        }
                        else if (newIndex >= totalImages)
                        {
                            newIndex = 0;
                        }
                        max= newIndex;
                        item.SetSprite(ItemSprites[newIndex]);
                        item.ImageIndex = newIndex;
                    }
                    if (item.PosId == 2)
                    {
                        min = item.ImageIndex;
                    }
                }
            }

            for (int i = 0; i < posDateList.Count; i++)
            {
                itemList[i].SetPosDate(posDateList[itemList[i].PosId]);
            }

            foreach(RotationDiagramItem item in itemList)
            {
                if(item.PosId==1||item.PosId==2)
                {
                    item.ChangeColore(0.5f);
                }
                else
                {
                    item.ChangeColore(1f);
                }
            }

        }

        private void CalulateDate()
        {

            List<ItemDate> itemDateList = new List<ItemDate>();

            float length = (ItemSize.x + offset) * itemList.Count;
            float radioOffset = 1 / (float)itemList.Count;
            float radio = 0;

            for (int i = 0; i < itemList.Count; i++)
            {
                ItemDate itemDate = new ItemDate();
                itemDate.PosId = i;
                itemDateList.Add(itemDate);
                itemList[i].PosId = i;


                int imageIndex = (i + 1) % 3; // 计算图片索引，使初始状态最左边的物品显示第一张图片，中间的物品显示第二张图片，右边的物品显示第三张图片
                itemList[i].SetSprite(ItemSprites[imageIndex]);
                itemList[i].ImageIndex = imageIndex;

                ItemPosDate date = new ItemPosDate();
                date.X = GetX(radio, length);
                date.ScaleTimes = GetScaleTimes(radio, ScaleTimesMin, ScaleTimesMax);

                radio += radioOffset;
                posDateList.Add(date);
            }

            itemDateList = itemDateList.OrderBy(u => posDateList[u.PosId].ScaleTimes).ToList();

            for (int i = 0; i < itemDateList.Count; i++)
            {
                posDateList[itemDateList[i].PosId].Order = i;
            }
        }

        private void SetItemDate()
        {
            for (int i = 0; i < posDateList.Count; i++)
            {
                itemList[i].SetPosDate(posDateList[i]);
            }
        }

        private float GetX(float radio, float length)
        {
            if (radio > 1 || radio < 0)
            {
                Debug.LogError("当前比例必须是0-1");
                return 0;
            }

            if (radio >= 0 && radio < 0.25f)
            {
                return length * radio;
            }
            else if (radio >= 0.25f && radio < 0.75f)
            {
                return length * (0.5f - radio);
            }
            else
            {
                return length * (radio - 1);
            }
        }

        private float GetScaleTimes(float radio, float min, float max)
        {
            if (radio > 1 || radio < 0)
            {
                Debug.LogError("当前比例必须是0-1");
                return 0;
            }

            float scaleOffset = (max - min) / 0.5f;
            if (radio < 0.5f)
            {
                return max - scaleOffset * radio;
            }
            else
            {
                return max - scaleOffset * (1 - radio);
            }
        }
    }

    public class ItemPosDate
    {
        public float X;
        public float ScaleTimes;
        public int Order;
    };

    public struct ItemDate
    {
        public int PosId;
    };
}

