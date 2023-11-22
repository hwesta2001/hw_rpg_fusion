using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public static PlayerInput PlayerInput { get; private set; }
    private void Awake()
    {
        Instance = this;
        PlayerInput = GetComponent<PlayerInput>();
    }


}
