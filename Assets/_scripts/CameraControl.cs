using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CameraControl : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera cinemachineCamera;
    [SerializeField] float camVerticalSpeed = 3;
    [SerializeField] float camHorizontalSpeed = 3;
    [SerializeField] Transform target;
    public static CameraControl ins;

    Vector3 vert;
    float tickTime = 0;
    bool camReseted;

    void Awake()
    {
        ins = this;
        vert = Vector3.zero;
    }

    void Update()
    {
        if (target == null) return;
        if (Input.GetMouseButtonDown(0) /*|| Input.GetTouch(0).phase == TouchPhase.Began*/)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            vert = target.localEulerAngles;
        }
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            TargetRotate();
            tickTime = 0;
            camReseted = false;
        }
        else
        {
            if (camReseted) return;
            tickTime += Time.deltaTime;
            if (tickTime >= 2)
            {
                tickTime = 0;
                camReseted = true;
                vert = Vector3.zero;
                target.DOLocalRotate(Vector3.zero, .5f);
            }
        }
    }
    public void SetTarget(Transform _target)
    {
        cinemachineCamera.Follow = _target;
        cinemachineCamera.LookAt = _target;
        target = _target;
    }



    void TargetRotate()
    {
        //target.localEulerAngles += HorizontalTurn();
        vert += VerticalTurn();
        float x = Mathf.Clamp(vert.x, -60, 80);
        vert.x = x;
        vert += HorizontalTurn();
        target.localEulerAngles = vert;
    }

    Vector3 VerticalTurn()
    {
        float verDelta = Input.GetAxis("Mouse Y");
        verDelta *= (-camVerticalSpeed * Time.deltaTime);
        return new Vector3(verDelta, 0, 0);
    }

    Vector3 HorizontalTurn()
    {
        float horDelta = Input.GetAxis("Mouse X");
        horDelta *= (camHorizontalSpeed * Time.deltaTime);
        return new Vector3(0, horDelta, 0);
    }

}
