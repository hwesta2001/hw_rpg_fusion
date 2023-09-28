using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharGenPortSelect : MonoBehaviour
{
    [SerializeField] Renderer rend;
    [SerializeField] LayerMask _layerMask;
    [Space]
    [SerializeField] Transform next;
    [SerializeField] Transform prev;

    List<Texture2D> _portList;
    int _portIndex = 0;
    Camera _camera;
    CharGenerator charGenerator;
    private void Awake()
    {
        if (rend == null)
        {
            Debug.LogError("Add Quad Materail");
        }

        _camera = Camera.main;
    }

    void Start()
    {
        charGenerator = GetComponent<CharGenerator>();
        _portList = charGenerator.portList;
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, _layerMask))
            {
                if (hit.collider != null)
                {
                    if (hit.transform == next) NextTexture();
                    else if (hit.transform == prev) PrevTexture();
                }
            }
        }
    }


    void ChangeMat(Texture2D texture2D)
    {
        rend.material.mainTexture = texture2D;
        CharGenerator.ins.portIndex = _portIndex;
    }



    [ContextMenu("NextPort")]
    void NextTexture()
    {
        _portIndex++;
        if (_portIndex >= _portList.Count) { _portIndex = 0; }
        ChangeMat(_portList[_portIndex]);

    }

    [ContextMenu("PrevPort")]
    void PrevTexture()
    {
        _portIndex--;
        if (_portIndex < 0) { _portIndex = _portList.Count - 1; }
        ChangeMat(_portList[_portIndex]);
    }
}
