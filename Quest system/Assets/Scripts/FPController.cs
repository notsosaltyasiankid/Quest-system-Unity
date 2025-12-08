using UnityEditor.Rendering.LookDev;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPController : MonoBehaviour
{
    public float MaxSpeed => SprintInput ? SprintSpeed : WalkSpeed;
    public float Acceleration = 15f;

    [SerializeField] float WalkSpeed = 3.5f;
    [SerializeField] float SprintSpeed = 8f;
    public Vector3 CurrentVelocity {  get; private set; }
    public float CurrentSpeed {  get; private set; }

    public bool sprinting {
        get 
        {
            return SprintInput && CurrentSpeed > 0.1f; 
        }
    }

    public Vector2 LookSensitivy = new Vector2(0.1f, 0.1f);
    public float PitchLimit = 85f;
    [SerializeField] float currentPitch = 0f;
    public float CurrentPitch
    {
        get => currentPitch;

        set
        {
            currentPitch = Mathf.Clamp(value, -PitchLimit, PitchLimit);
        }
    }

    [SerializeField] float CameraNormalFov = 60f;
    [SerializeField] float CameraSprintFov = 80f;
    [SerializeField] float CameraFovSmoothing = 1f;

    float TargetCameraFov
    {
        get
        {
            return sprinting ? CameraSprintFov : CameraNormalFov;
        }
    }

    public Vector2 MoveInput;
    public Vector2 LookInput;
    public bool SprintInput;

    [SerializeField] Camera FPCamera;
    [SerializeField] CharacterController Controller;

    void OnValidate()
    {
        if (Controller == null)
        {
            Controller = GetComponent<CharacterController>();
        }
    }

    void Update()
    {
        MoveUpdate();
        LookUpdate();
        CameraUpdate();
    }

    void MoveUpdate()
    {
        Vector3 motion = transform.forward * MoveInput.y + transform.right * MoveInput.x;
        motion.y = 0f;
        motion.Normalize();

        if (motion.sqrMagnitude >= 0.01f)
        {
            CurrentVelocity = Vector3.MoveTowards(CurrentVelocity, motion * MaxSpeed, Acceleration * Time.deltaTime);
        }
        else
        {
            CurrentVelocity = Vector3.MoveTowards(CurrentVelocity, Vector3.zero, Acceleration * Time.deltaTime);
        }

        float verticalVelocity = Physics.gravity.y * 20f * Time.deltaTime;

        Vector3 fullVelocity = new Vector3(CurrentVelocity.x, verticalVelocity, CurrentVelocity.z);



        Controller.Move(fullVelocity * Time.deltaTime);

        CurrentSpeed = CurrentVelocity.magnitude;
    }

    void LookUpdate()
    {
        Vector2 input = new Vector2(LookInput.x * LookSensitivy.x, LookInput.y * LookSensitivy.y);
        
        // omhoog en omlaag kijken
        currentPitch -= input.y;

        FPCamera.transform.localRotation = Quaternion.Euler(currentPitch, 0f, 0f);

        // links en rechts kijken
        transform.Rotate(Vector3.up *  input.x);
    }

    void CameraUpdate()
    {
        float targetFOV = CameraNormalFov;

        if (sprinting)
        {
            float speedRatio = CurrentSpeed / SprintSpeed;

            targetFOV = Mathf.Lerp(CameraNormalFov, CameraSprintFov, speedRatio);
        }


        FPCamera.fieldOfView = Mathf.Lerp(FPCamera.fieldOfView, targetFOV, Time.deltaTime * CameraFovSmoothing);

    }
}