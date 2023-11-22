using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertText : MonoBehaviour
{
    [SerializeField] Color m_Color = Color.yellow;
    [SerializeField] TextMeshProUGUI Alert_Text;
    [SerializeField] int maxLineCount = 5;
    List<string> textList = new();
    [SerializeField] float tickTime = 1f;
    float tick;
    public static AlertText ins;
    bool canTick;

    private void Awake()
    {
        ins = this;
        canTick = false;
    }

    void Start()
    {
        if (Alert_Text != null)
            Alert_Text = GetComponentInChildren<TextMeshProUGUI>();
        ResetAll();
        tick = tickTime;
        Alert_Text.color = m_Color;
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
        Alert_Text.text = "";
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
                Alert_Text.text += item + "\n";
            }
        }
    }

    public void AddText(string _text, Color? _color = null)
    {
        Color editColor = _color ?? m_Color;
        canTick = true;
        //string edited = "<color=#" + ColorUtility.ToHtmlStringRGB(editColor) + ">" + _text + "</color>";
        //textList.Insert(0, _text);
        textList.Insert(0, "<color=#" + ColorUtility.ToHtmlStringRGB(editColor) + ">" + _text + "</color>");
        WriteAll();
        //Debug.Log(edited);
        //Debug.Log("<color=red>" + _text + "</color>");
    }


    private void ResetAll()
    {
        tick = tickTime;
        textList.Clear();
        Alert_Text.text = "";
        canTick = false;
    }
}

public static class ColorExtentions
{
    public static string GetHexColor(this Color _color)
    {
        return ColorUtility.ToHtmlStringRGB(_color);
        //string col = "#ffffff";
        //if (_color == Color.green)
        //{
        //    col = "#00ff00";
        //}
        //else if (_color == Color.red)
        //{
        //    col = "#ff0000";
        //}
        //else if (_color == Color.yellow) //notDone
        //{
        //    col = "#00ff00";
        //}
        //else if (_color == Color.green) //notDone
        //{
        //    col = "#00ff00";
        //}
        //else if (_color == Color.green) //notDone
        //{
        //    col = "#00ff00";
        //}
        //else if (_color == Color.green) //notDone
        //{
        //    col = "#00ff00";
        //}
        //else if (_color == Color.green) //notDone
        //{
        //    col = "#00ff00";
        //}
        //else if (_color == Color.green) //notDone
        //{
        //    col = "#00ff00";
        //}
        //else if (_color == Color.green) //notDone
        //{
        //    col = "#00ff00";
        //}
        //else if (_color == Color.green) //notDone
        //{
        //    col = "#00ff00";
        //}

        //return col;
    }
}