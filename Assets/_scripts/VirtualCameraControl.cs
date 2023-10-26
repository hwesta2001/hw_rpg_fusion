using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class VirtualCameraControl : MonoBehaviour
{

    [SerializeField] CinemachineFreeLook freeLookCam;
    [SerializeField] float Xspeed = 3;
    [SerializeField] float Yspeed = 3;

    public static VirtualCameraControl ins;
    void Awake()
    {
        ins = this;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            freeLookCam.m_XAxis.m_MaxSpeed = Xspeed;
            freeLookCam.m_YAxis.m_MaxSpeed = Yspeed;
        }
        else
        {
            freeLookCam.m_XAxis.m_MaxSpeed = 0;
            freeLookCam.m_YAxis.m_MaxSpeed = 0;
        }
    }
    public void SetTarget(Transform _target)
    {
        freeLookCam.Follow = _target;
    }
}
