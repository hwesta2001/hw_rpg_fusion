using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class CameraControlOrbit : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook cineCamera;
    [SerializeField] Transform target;

    private string XAxisName = "Mouse X";
    private string YAxisName = "Mouse Y";

    public static CameraControlOrbit ins;
    void Awake() => ins = this;


    void Start()
    {
        cineCamera.m_XAxis.m_InputAxisName = "";
        cineCamera.m_YAxis.m_InputAxisName = "";
    }

    public void SetTarget(Transform _target)
    {
        cineCamera.Follow = _target;
        cineCamera.LookAt = _target;
        target = _target;
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (target != null && !IsPointerOnGui())
            {
                cineCamera.m_XAxis.m_InputAxisValue = Input.GetAxis(XAxisName);
                cineCamera.m_YAxis.m_InputAxisValue = Input.GetAxis(YAxisName);
            }
            else
            {
                cineCamera.m_XAxis.m_InputAxisValue = 0;
                cineCamera.m_YAxis.m_InputAxisValue = 0;
            }
        }
        else
        {
            cineCamera.m_XAxis.m_InputAxisValue = 0;
            cineCamera.m_YAxis.m_InputAxisValue = 0;
        }

    }

    bool IsPointerOnGui()
    {
        if (EventSystem.current.IsPointerOverGameObject(0))
        {
            return true;
        }
        else if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
