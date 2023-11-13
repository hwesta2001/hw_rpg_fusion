using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RayCastEvent : MonoBehaviour
{
    public static RayCastEvent ins { get; private set; }
    public UnityEvent rayCastEvent;
    private Camera _camera;
    [SerializeField] LayerMask _layerMask;

    public GameObject HittedObject;

    private void OnEnable()
    {
        ins = this;
        _camera = Camera.main;
    }

    public bool SelectionCast()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return false;
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100, _layerMask))
        {
            Debug.DrawLine(_camera.ScreenPointToRay(Input.mousePosition).origin, hit.point, Color.red, 2);
            if (hit.collider != null)
            {
                HittedObject = hit.collider.gameObject;
                rayCastEvent?.Invoke();
                return true;
            }
            return false;
        }
        else
        {
            HittedObject = null;
            return false;
        }
    }
}
