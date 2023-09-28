using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] float dist = 2;
    Transform cameraTransform;
    Transform tr;
    void Start()
    {
        tr = transform;
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        tr.position = cameraTransform.position + cameraTransform.forward * dist;
    }

    void LateUpdate()
    {
        Vector3 lookAtPosition = tr.position + cameraTransform.forward;
        tr.LookAt(lookAtPosition, cameraTransform.up);
    }
}