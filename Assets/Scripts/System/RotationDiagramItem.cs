using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HKZ
{
    public class RotationDiagramItem : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        public int PosId;
        private float offset;
        private float aniTime = 0.5f;

        private Action<float> moveAction;

        private Image image;
        private Image Image
        {
            get
            {
                if (image == null)
                {
                    image = GetComponent<Image>();
                }
                return image;
            }
        }

        private RectTransform rect;
        private RectTransform Rect
        {
            get
            {
                if (rect == null)
                {
                    rect = GetComponent<RectTransform>();
                }
                return rect;
            }
        }
        public int ImageIndex { get;  set; }

        private void Change(int index)
        {
            Debug.Log(index);
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        public void SetSprite(Sprite sprite)
        {
            Image.sprite = sprite;
        }

        public void SetPosDate(ItemPosDate date)
        {
            Rect.DOAnchorPos(Vector2.right * date.X, aniTime);
            Rect.DOScale(Vector3.one * date.ScaleTimes, aniTime);
            //Rect.anchoredPosition = Vector2.right*date.X;
            //Rect.localScale = Vector3.one * date.ScaleTimes;
            StartCoroutine(Wait(date));
        }

        private IEnumerator Wait(ItemPosDate date)
        {
            yield return new WaitForSeconds(aniTime * 0.5f);
            transform.SetSiblingIndex(date.Order);
        }

        public void OnDrag(PointerEventData eventData)
        {
            offset += eventData.delta.x;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            moveAction(offset);
            offset = 0;
        }

        public void AddMoveListener(Action<float> onMove)
        {
            moveAction = onMove;
        }

        public void ChangeId(int symbol, int totalItemNum)
        {
            int id = PosId;
            id += symbol;
            if (id < 0)
            {
                id += totalItemNum;
            }
            PosId = id % totalItemNum;
        }

        public void ChangeColore(float a)
        {
            image.color = new Color(1, 1, 1, a);
        }
    }
}

