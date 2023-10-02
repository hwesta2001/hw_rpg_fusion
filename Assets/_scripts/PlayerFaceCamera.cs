using UnityEngine;
public class PlayerFaceCamera : MonoBehaviour
{
    Transform cameraTransform;
    Transform tr;
    float degreesPerSecond = 150f;

    void Start()
    {
        tr = transform;
        cameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {

        // Make the quad face the camera's position and align with the camera's up vector
        //transform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward, cameraTransform.rotation * Vector3.up);
        Quaternion targetRotation = Quaternion.LookRotation(DirectionXZ());
        tr.rotation = Quaternion.RotateTowards(tr.rotation, targetRotation, degreesPerSecond * Time.deltaTime);
    }

    Vector3 DirectionXZ()
    {
        Vector3 direction = cameraTransform.position - tr.position;
        direction.y = 0;
        return -1 * direction;
    }
}
