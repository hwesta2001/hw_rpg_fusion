using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class VirtualCameraControl : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera_1;
    [SerializeField] CinemachineVirtualCamera virtualCamera_2;
    public static VirtualCameraControl ins;
    CinemachinePOV pov;

    private void Awake()
    {
        if (ins != null) Destroy(gameObject);
        else ins = this;

        if (virtualCamera_1 == null || virtualCamera_2 == null)
        {
            Debug.LogError("Put virtual cameras in sloats");
        }

        pov = virtualCamera_1.GetCinemachineComponent<CinemachinePOV>();
        virtualCamera_1.gameObject.SetActive(false);
        virtualCamera_2.gameObject.SetActive(true);
    }

    public void SetTarget(Transform _target)
    {
        virtualCamera_1.Follow = _target;
        virtualCamera_1.LookAt = _target;
        virtualCamera_2.Follow = _target;
        virtualCamera_2.LookAt = null;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (virtualCamera_1.gameObject.activeSelf == true) return;
            virtualCamera_2.transform.localPosition = virtualCamera_1.transform.localPosition;
            pov.m_VerticalAxis.Value = virtualCamera_2.transform.eulerAngles.x;
            virtualCamera_2.gameObject.SetActive(false);
            virtualCamera_1.gameObject.SetActive(true);
        }
        else
        {
            if (virtualCamera_2.gameObject.activeSelf == true) return;
            virtualCamera_1.transform.localPosition = virtualCamera_2.transform.localPosition;
            virtualCamera_1.gameObject.SetActive(false);
            virtualCamera_2.gameObject.SetActive(true);
        }
    }

}
