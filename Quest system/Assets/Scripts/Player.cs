using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] FPController FPController;

    void OnMove(InputValue value)
    {
        FPController.MoveInput = value.Get<Vector2>();
    }
    void OnLook(InputValue value)
    {
        FPController.LookInput = value.Get<Vector2>();
    }

    void OnSprint(InputValue value)
    {
        FPController.SprintInput = value.isPressed;
    }

    void OnValidate()
    {
        if (FPController != null) { GetComponent<FPController>(); }
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
}
