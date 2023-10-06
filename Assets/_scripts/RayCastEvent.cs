using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RayCastEvent : MonoBehaviour
{
    public static RayCastEvent ins;
    public UnityEvent rayCastEvent;
    private Camera _camera;
    [SerializeField] LayerMask _layerMask;

    public GameObject HittedObject;

    private void OnEnable()
    {
        ins = this;
        _camera = Camera.main;
    }


    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1000, _layerMask))
            {
                Debug.DrawLine(_camera.ScreenPointToRay(Input.mousePosition).origin, hit.point, Color.red, 2);
                if (hit.collider != null)
                {
                    HittedObject = hit.collider.gameObject;
                    rayCastEvent?.Invoke();
                }
            }
            else
            {
                HittedObject = null;
            }
        }
    }
}
