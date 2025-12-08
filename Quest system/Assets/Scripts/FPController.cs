using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPController : MonoBehaviour
{
    public float MaxSpeed = 3.5f;
    public float Acceleration = 15f;

    public Vector3 CurrentVelocity {  get; private set; }
    public float CurrentSpeed {  get; private set; }

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

    public Vector2 MoveInput;
    public Vector2 LookInput;


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
}