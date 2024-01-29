using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class CameraControlOrbit : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook cineCamera;
    [SerializeField] Transform target;

    [SerializeField] float x_rot_Speed = 1;   // yatay
    [SerializeField] float y_rot_Speed = 1;   // dikey

    private readonly string XAxisName = "Mouse X";   // yatay
    private readonly string YAxisName = "Mouse Y";   // dikey

    public static CameraControlOrbit ins;
    void Awake() => ins = this;


    void Start()
    {
        cineCamera.m_XAxis.m_InputAxisName = "";   // yatay
        cineCamera.m_YAxis.m_InputAxisName = "";   // dikey
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
                cineCamera.m_XAxis.m_InputAxisValue += Input.GetAxis(XAxisName) * x_rot_Speed; // yatay
                cineCamera.m_YAxis.m_InputAxisValue = Input.GetAxis(YAxisName) * y_rot_Speed; // dikey
            }
            else
            {
                //cineCamera.m_XAxis.m_InputAxisValue = 0;
                //cineCamera.m_YAxis.m_InputAxisValue = 0;
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
