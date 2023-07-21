using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Clue : MonoBehaviour
{
    [SerializeField] private Text textComponent;

    [SerializeField] private Text[] xiansuo;

    [SerializeField] private Text[] linggan;

    [SerializeField] private Font font;

    [SerializeField] private GameObject duihua;

    [SerializeField] private Scene53 scene53;

    [Header("Line")]
    private LineRenderer lineRenderer;

    private int count = 1;

    private int line_start;

    private int line_end;

    private int line_starty;

    private int pagesNum = 0;

    private string line_text = "";

    private List<Vector3> selectPoints = new List<Vector3>();

    private List<string> strings = new List<string>();
    // Start is called before the first frame update
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        DrawNote();
        if(strings.Count==3)
        {
            duihua.SetActive(true);
            scene53.enabled = true;
            transform.parent.gameObject.SetActive(false);
        }
    }
    private void DrawNote()
    {
        if (Input.GetMouseButtonDown(0))
        {
            line_start = (int)Input.mousePosition.x;
            line_starty = (int)Input.mousePosition.y;
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
            Vector2 position = charInfo.cursorPos / textComponent.pixelsPerUnit - new Vector2(0, GetCharacterHeight() * 1.2f);
            // ת��Ϊ�ı��������������
            Vector3 worldPosition = textComponent.rectTransform.TransformPoint(position);
            //Debug.Log("��������(y):" + worldPosition.y + "�߶�" + GetCharacterHeight() + "��꿪ʼ��y" + line_starty);
            if (line_start - (GetCharacterWidth() / 2) < worldPosition.x && worldPosition.x < line_end - (GetCharacterWidth() / 2) && Mathf.Abs(worldPosition.y - line_starty) < 0.5 * GetCharacterHeight())
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
                Vector3 temp = Camera.main.WorldToScreenPoint(selectPoints[i]) + new Vector3(GetCharacterWidth(), 0, 10);
                temp = Camera.main.ScreenToWorldPoint(temp);
                lineRenderer.SetPosition(i + 1, temp);
            }
        }
        if (line_text != "")
        {
                for(int j=0;j<line_text.Length;j++)
                {
                if (line_text[j] =='Ů'&&!strings.Contains("Ů��"))
                {
                    if(j+1< line_text.Length)
                    {
                        Debug.Log(line_text);
                        strings.Add("Ů��");
                            xiansuo[pagesNum].text = "Ů��";
                            linggan[pagesNum].text = "Ů��Ů�ӣ�Ů֮�ӣ�������һ�������������ָ�������������硣";
                            pagesNum++;
                    }
                }
                }
                for(int i=0;i<line_text.Length;i++)
            {
                if (line_text[i]=='��'&&!strings.Contains("��"))
                {
                    Debug.Log(line_text);
                    strings.Add("��");
                    xiansuo[pagesNum].text = "��";
                    linggan[pagesNum].text = "����ʳҲ������˵������ġ�СŮ�ӡ�Ҫ�׵á���Ů�ӡ��ļ�ҵ";
                    pagesNum++;
                }
            }
                for(int i=0;i<line_text.Length;i++)
            {
                if (line_text[i]=='��'&&i+2<line_text.Length&& !strings.Contains("����ˮ"))
                {
                    Debug.Log(line_text);
                    strings.Add("����ˮ");
                    xiansuo[pagesNum].text = "����ˮ";
                    linggan[pagesNum].text = "Ҫ��֪����������ʱ�־���������ʲô�£������ͱ����ҵ��������ˮ�����ڡ�";
                    pagesNum++;
                }
            }

        }             
        else { lineRenderer.positionCount = 1; };
    }
}
