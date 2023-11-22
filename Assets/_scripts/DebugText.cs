using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DebugText : MonoBehaviour
{
    [SerializeField] Color m_Color = Color.yellow;
    [SerializeField] TextMeshProUGUI debugTextMP;
    [SerializeField] int maxLineCount = 20;
    List<string> textList = new();
    [SerializeField] float tickTime = 1f;
    float tick;
    public static DebugText ins;
    private bool canTick;

    private void Awake()
    {
        ins = this;
        canTick = false;
    }

    void Start()
    {
        if (debugTextMP != null)
            debugTextMP = GetComponentInChildren<TextMeshProUGUI>();
        ResetAll();
        tick = tickTime;
        debugTextMP.color = m_Color;
    }

    void Update()
    {
        Ticking();
    }

    private void Ticking()
    {
        if (!canTick) return;
        tick -= Time.deltaTime;
        if (tick < 0)
        {
            tick = tickTime - ((tickTime - (tickTime * .4f)) * textList.Count / maxLineCount);

            textList.RemoveAt(textList.Count - 1);
            WriteAll();
        }
    }

    private void WriteAll()
    {
        debugTextMP.text = "";
        if (textList.Count <= 0)
        {
            ResetAll();
            return;
        }
        else
        {
            if (textList.Count > maxLineCount)
            {
                textList.RemoveRange(maxLineCount, textList.Count - maxLineCount);

            }
            foreach (var item in textList)
            {
                debugTextMP.text += item + "\n";
            }
        }
    }

    public void AddText(string _text)
    {
        canTick = true;
        textList.Insert(0, _text);
        WriteAll();
        //Debug.Log("<color=yellow>" + _text + "</color>");
    }


    private void ResetAll()
    {
        tick = tickTime;
        textList.Clear();
        debugTextMP.text = "";
        canTick = false;
    }
}
