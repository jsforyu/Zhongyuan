using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LineTextCollisionDetection : MonoBehaviour
{

    public Text test;

    [SerializeField] private Text textComponent;

    [SerializeField] private Font font;

    [Header("Line")]
    private LineRenderer lineRenderer;

    private int count = 1;

    private int line_start;

    private int line_end;

    private string line_text = "";


    private List<Vector3> selectPoints = new List<Vector3>();
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        DrawNote();
    }
    private void DrawNote()
    {
        if (Input.GetMouseButtonDown(0))
        {
            line_start = (int)Input.mousePosition.x;
            lineRenderer.positionCount = 1;
            Vector3 drawPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
            lineRenderer.SetPosition(0, drawPosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 drawPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
            if (lineRenderer != null)
            {
                if (count - 1 >= 0 && count - 1 < lineRenderer.positionCount)
                {
                    if (Vector3.Distance(lineRenderer.GetPosition(count - 1), drawPosition) > 0.1)
                    {
                        lineRenderer.positionCount = count + 1;
                        lineRenderer.SetPosition(count, drawPosition);
                        while (count < lineRenderer.positionCount)
                        {
                            if (Vector3.Distance(lineRenderer.GetPosition(count), lineRenderer.GetPosition(count - 1)) > 0.1)
                            {
                                lineRenderer.positionCount++;
                                Vector3 dir = lineRenderer.GetPosition(count) - lineRenderer.GetPosition(count - 1);
                                dir = dir.normalized;
                                lineRenderer.SetPosition(count + 1, lineRenderer.GetPosition(count));
                            }
                            count++;
                        }
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            line_end = (int)Input.mousePosition.x;
            count = 1;
            GetCharacterPositions();
            line_text = "";
            selectPoints.Clear();
        }

    }
    private float GetCharacterWidth()   //��ȡ�����ַ��Ŀ��
    {
        CharacterInfo charInfo;
        font.RequestCharactersInTexture(textComponent.text, textComponent.fontSize);
        font.GetCharacterInfo(textComponent.text[0], out charInfo, textComponent.fontSize);
        float charWidth = charInfo.advance;
        return charWidth;
    }
    private float GetCharacterHeight()   //��ȡ�����ַ��ĸ߶�
    {
        CharacterInfo charInfo;
        font.RequestCharactersInTexture(textComponent.text, textComponent.fontSize);
        font.GetCharacterInfo(textComponent.text[0], out charInfo, textComponent.fontSize);
        float charHeight = charInfo.glyphHeight;
        return charHeight;
    }
    private void GetCharacterPositions()
    {
        TextGenerator textGenerator = new TextGenerator();
        TextGenerationSettings generationSettings = textComponent.GetGenerationSettings(textComponent.rectTransform.rect.size);
        textGenerator.Populate(textComponent.text, generationSettings);
        for (int i = 0; i < textGenerator.characterCount; i++)
        {
            UICharInfo charInfo = textGenerator.characters[i];
            Vector2 position = charInfo.cursorPos / textComponent.pixelsPerUnit-new Vector2(0,GetCharacterHeight()*1.1f);
            // ת��Ϊ�ı��������������
            Vector3 worldPosition = textComponent.rectTransform.TransformPoint(position);   
            if (line_start - (GetCharacterWidth()/2) < worldPosition.x && worldPosition.x < line_end - (GetCharacterWidth() / 2))
            {
                if (i < textComponent.text.Length)
                {
                    line_text += textComponent.text[i];
                    Vector3 charPosition = Camera.main.ScreenToWorldPoint(worldPosition) + new Vector3(0, 0, 10);
                    selectPoints.Add(charPosition); 
                }
            }
        }
        for (int i = 0; i < selectPoints.Count; i++)
        {
            lineRenderer.positionCount = selectPoints.Count + 1;
            lineRenderer.SetPositions(selectPoints.ToArray());
            if (i == selectPoints.Count - 1)
            {
                Vector3 temp = Camera.main.WorldToScreenPoint(selectPoints[i]) + new Vector3(GetCharacterWidth(), 0, 0);
                temp = Camera.main.ScreenToWorldPoint(temp);
                lineRenderer.SetPosition(i + 1, temp);
            }
        }
        test.text = line_text;
    }
    //private void LineAni()
    //{
    //    if (Drawing)
    //    {
    //        DrawingTime += Time.deltaTime;
    //        if(DrawingTime>0.8f&&DrawingNums<selectPoints.Count)
    //        {
    //            lineRenderer.positionCount = selectPoints.Count + 1;
    //            lineRenderer.SetPosition(DrawingNums, selectPoints[DrawingNums]);
    //            if (DrawingNums == selectPoints.Count - 1)
    //            {
    //                Vector3 temp = Camera.main.WorldToScreenPoint(selectPoints[DrawingNums]) + new Vector3(GetCharacterWidth(), 0, 0);
    //                temp = Camera.main.ScreenToWorldPoint(temp);
    //                lineRenderer.SetPosition(DrawingNums + 1, temp);
    //            }
    //            DrawingNums++;
    //            DrawingTime = 0;
    //        }
    //        if(DrawingNums==selectPoints.Count)
    //        {
    //            //Drawing = false;
    //            DrawingNums = 0;
    //            DrawingTime = 0;
    //        }
    //    }
    //}
}