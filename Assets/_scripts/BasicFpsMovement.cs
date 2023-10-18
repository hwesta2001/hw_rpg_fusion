using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class BasicFpsMovement : MonoBehaviour
{
    [SerializeField] bool turnWithRightClick = true;
    [SerializeField] float moveSpeed = 12;
    [SerializeField] float camVerticalSpeed = 300;
    [SerializeField] float camHorizontalSpeed = 300;
    [SerializeField] float camHightSpeed = 300f;
    [SerializeField] int camHightClampMin = -10;
    [SerializeField] int camHightClapmMax = 30;
    [SerializeField] Transform cam;

    Vector3 vert;
    bool canTurn = false;
    CharacterController controller;
    float cameraHcarpan = 1;
    bool up;

    [SerializeField] InputActionReference _move, _mouse_scroll; // set in inspector

    void Awake()
    {
        if (cam == null) cam = Camera.main.transform;
        controller = GetComponent<CharacterController>();

        vert = new(cam.localEulerAngles.x, transform.localEulerAngles.y, 0);
        if (cam.parent != transform) cam.parent = transform;
        cam.localEulerAngles = new(cam.localEulerAngles.x, 0, 0); ;
        cam.localPosition = new(0, cam.localPosition.y, 0);
#if UNITY_ANDROID
        turnWithRightClick = false;
#endif
#if UNITY_EDITOR
        turnWithRightClick = true;
#endif
    }


    void Update()
    {
        controller.Move(MoveThis());
        TurnControl();
        if (EventSystem.current.IsPointerOverGameObject()) return;
        CamHight();
        CameraRotate();
        //}
        //void LateUpdate()
        //{
        if (up)
        {
            float camY = cam.localPosition.y + cameraHcarpan * camHightSpeed * Time.deltaTime;
            camY = Mathf.Clamp(camY, camHightClampMin, camHightClapmMax);
            cam.localPosition = new(cam.localPosition.x, camY, cam.localPosition.z);
        }
    }

    void CamHight()
    {

        if (_mouse_scroll.action.ReadValue<float>() != 0)
        {
            cameraHcarpan = -_mouse_scroll.action.ReadValue<float>() * .001f;
            up = true;
        }
        else
        {
            up = false;
        }
    }

    Vector3 MoveThis()
    {

        float horizontal = _move.action.ReadValue<Vector2>().normalized.x * moveSpeed * Time.deltaTime;
        float vertical = -_move.action.ReadValue<Vector2>().normalized.y * moveSpeed * Time.deltaTime;
        return transform.forward * vertical + transform.right * horizontal;

    }

    void CameraRotate()
    {
        if (!canTurn) return;
        transform.localEulerAngles += HorizontalTurn();
        vert += VerticalTurn();
        float x = Mathf.Clamp(vert.x, -60, 80);
        vert.x = x;
        cam.localEulerAngles = vert;
    }

    Vector3 VerticalTurn()
    {
        float verDelta = Input.GetAxis("Mouse Y");
        verDelta *= (-camVerticalSpeed * Time.deltaTime);
        //float invertY = -1;
        //verDelta *= invertY;
        return new Vector3(verDelta, 0, 0);
    }

    Vector3 HorizontalTurn()
    {
        float horDelta = Input.GetAxis("Mouse X");
        horDelta *= (camHorizontalSpeed * Time.deltaTime);
        return new Vector3(0, horDelta, 0);
    }

    void TurnControl()
    {
        if (turnWithRightClick)
        {
            if (Input.GetMouseButton(1))
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
                canTurn = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                canTurn = false;
            }
        }
        else
        {
            if (Input.GetMouseButton(1))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                canTurn = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                canTurn = true;
            }
        }
    }
}