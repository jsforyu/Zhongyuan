using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LineTextCollisionDetection : Singleton<LineTextCollisionDetection>
{
    [SerializeField] private Text textComponent;

    [SerializeField] private Font font;

    [SerializeField] private Text[] notes;
    [Header("Line")]
    private LineRenderer lineRenderer;

    private int count = 1;

    private int line_start;

    private int line_end;

    private int line_starty;

    private int pagesNum = 0;

    private string line_text = "";

    private List<string> notesList = new List<string>();

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
            line_starty=(int) Input.mousePosition.y;
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
    private float GetCharacterWidth()   //获取单个字符的宽度
    {
        CharacterInfo charInfo;
        font.RequestCharactersInTexture(textComponent.text, textComponent.fontSize);
        font.GetCharacterInfo(textComponent.text[0], out charInfo, textComponent.fontSize);
        float charWidth = charInfo.advance;
        return charWidth;
    }
    private float GetCharacterHeight()   //获取单个字符的高度
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
            Vector2 position = charInfo.cursorPos / textComponent.pixelsPerUnit-new Vector2(0,GetCharacterHeight()*1.2f);
            // 转换为文本组件的世界坐标
            Vector3 worldPosition = textComponent.rectTransform.TransformPoint(position);
            //Debug.Log("世界坐标(y):" + worldPosition.y + "高度" + GetCharacterHeight() + "鼠标开始的y" + line_starty);
            if (line_start - (GetCharacterWidth()/2) < worldPosition.x && worldPosition.x < line_end - (GetCharacterWidth() / 2)&&Mathf.Abs(worldPosition.y-line_starty)<0.5*GetCharacterHeight())
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
        if(line_text!="")notesList.Add(line_text);
        else { lineRenderer.positionCount = 1; }
        UpdateNotes();
    }
    private void UpdateNotes()
    {
        int currentPage=pagesNum* notes.Length;
        for (int i=0; i<notes.Length; i++)
        {
            if(currentPage+i>=notesList.Count)
            {
                notes[i].text = string.Empty;
            }
            else
            {
                notes[i].text = notesList[currentPage+i];
            }

        }
    }

    public void NextPage()
    {
        if(notesList.Count>(pagesNum+1)*notes.Length)
        pagesNum++;
    }
    public void LastPage()
    {
        if(pagesNum>0)
        {
            pagesNum--;
        }
    }

    public void DeteleteNote(string deteleString)
    {
        notesList.Remove(deteleString);
        UpdateNotes();
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