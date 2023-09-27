using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BasicFpsMovement : MonoBehaviour
{
    [SerializeField] private bool turnWithRightClick;
    [SerializeField] private float moveSpeed = 12;
    [SerializeField] private float camVerticalSpeed = 300;
    [SerializeField] private float camHorizontalSpeed = 300;
    [SerializeField] private float camHightSpeed = 30f;
    [SerializeField] private int camHightClampMin = 1;
    [SerializeField] private int camHightClapmMax = 20;
    [SerializeField] private Transform cam;

    private Vector3 vert;
    private bool canTurn = false;
    private CharacterController controller;
    private int cameraHcarpan = 1;
    private bool up;

    private void Awake()
    {
        if (cam == null) cam = Camera.main.transform;
        controller = GetComponent<CharacterController>();

        vert = new(cam.localEulerAngles.x, transform.localEulerAngles.y, 0);
        if (cam.parent != transform) cam.parent = transform;
        cam.localEulerAngles = new(cam.localEulerAngles.x, 0, 0); ;
        cam.localPosition = new(0, cam.localPosition.y, 0);
    }

    private void Update()
    {
        controller.Move(MoveThis());
        TurnControl();
        CamHight();
        CameraRotate();
    }

    private void LateUpdate()
    {
        if (up)
        {
            float camY = cam.localPosition.y + cameraHcarpan * camHightSpeed * Time.deltaTime;
            camY = Mathf.Clamp(camY, camHightClampMin, camHightClapmMax);
            cam.localPosition = new(cam.localPosition.x, camY, cam.localPosition.z);
        }
    }

    private void CamHight()
    {
        if (Input.GetKey(KeyCode.E))
        {
            cameraHcarpan = 1;
            up = true;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            cameraHcarpan = -1;
            up = true;
        }
        else
        {
            up = false;
        }
    }

    private Vector3 MoveThis()
    {
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        return transform.forward * vertical + transform.right * horizontal;
    }

    private void CameraRotate()
    {
        if (!canTurn) return;
        transform.localEulerAngles += HorizontalTurn();
        vert += VerticalTurn();
        float x = Mathf.Clamp(vert.x, -80, 80);
        vert.x = x;
        cam.localEulerAngles = vert;
    }

    private Vector3 VerticalTurn()
    {
        float verDelta = Input.GetAxis("Mouse Y");
        verDelta *= (camVerticalSpeed * Time.deltaTime);
        float invertY = -1;
        verDelta *= invertY;
        return new Vector3(verDelta, 0, 0);
    }

    private Vector3 HorizontalTurn()
    {
        float horDelta = Input.GetAxis("Mouse X");
        horDelta *= (camHorizontalSpeed * Time.deltaTime);
        return new Vector3(0, horDelta, 0);
    }

    private void TurnControl()
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