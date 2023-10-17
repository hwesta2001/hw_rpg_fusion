using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharGenPortSelect : MonoBehaviour
{
    [SerializeField] List<Texture2D> _portList;
    int _portIndex = 0;
    CharManager charGenerator;

    void Start()
    {
        charGenerator = GetComponent<CharManager>();
        _portList = charGenerator.portList;
    }

    public void NextTexture()
    {
        _portIndex++;
        if (_portIndex >= _portList.Count) _portIndex = 0;
        CharManager.ins.PortIndex = _portIndex;
    }

    public void PrevTexture()
    {
        _portIndex--;
        if (_portIndex < 0) _portIndex = _portList.Count - 1;
        CharManager.ins.PortIndex = _portIndex;
    }
}
