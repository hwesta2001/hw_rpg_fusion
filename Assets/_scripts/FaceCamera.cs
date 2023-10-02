using System.Drawing;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] float dist = 2;
    [SerializeField] float movePerSecond = 20;
    [SerializeField] float degreesPerSecond = 120;
    Transform cameraTransform;
    Transform tr;

    void Start()
    {
        tr = transform;
        cameraTransform = Camera.main.transform;
    }



    void LateUpdate()
    {
        var pos = cameraTransform.position + cameraTransform.forward * dist;
        tr.position = Vector3.Lerp(tr.position, pos, movePerSecond * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(DirectionXZ());
        tr.rotation = Quaternion.RotateTowards(tr.rotation, targetRotation, degreesPerSecond * Time.deltaTime);
    }

    Vector3 DirectionXZ()
    {

        //Vector3 direction = tr.position + cameraTransform.rotation * Vector3.forward;

        Vector3 direction = cameraTransform.position - tr.position;
        //direction.y = 0;
        return -1 * direction;
    }
}