using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementVersus : MonoBehaviour
{
    [Header("Movement")]
    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float stopSpeed;
    [SerializeField] private float rotationSpeed;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    [Header("References")]
    [SerializeField] private PlayerInputVersus playerInput;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private PlayerEvents playerEvents;

    private Transform currentDock;

    private bool hasFish;
    public bool HasFish { get { return hasFish; } }

    public int fishes;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        initialSpeed = speed;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
    private void Start()
    {
        //OnGameStart();
    }

    private void OnGameStart()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        speed = initialSpeed;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        playerState.ResetState();
    }
    void Update()
    {
        if (playerState.IsPlayerState(PlayerStateType.Moving))
        {
            BoatRotation();
        }
    }
    void FixedUpdate()
    {
        switch (playerState.currentState)
        {
            case PlayerStateType.Moving:
                Move();
                break;
            case PlayerStateType.Paused:
                Paused();
                break;
            case PlayerStateType.Rotating:
                Rotate();
                break;
            case PlayerStateType.Stopping:
                StopBoat();
                break;
            case PlayerStateType.Crashed:
                break;
            default:
                break;
        }
    }
    private void Paused()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    private void Move()
    {
        rb.linearVelocity = transform.right * speed;
    }
    private void BoatRotation()
    {
        if (!playerInput.GetIsPressed) return;

        transform.Rotate(Vector3.up * playerInput.GetAngle * playerInput.GetRotationSpeed * rotationSpeed);
    }
    #region Crash
    public void Crash()
    {
        if (playerState.IsPlayerState(PlayerStateType.Crashed))
            return;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        playerState.SetState(PlayerStateType.Crashed);
        playerEvents.Crashed();

        Camera.main.transform.DOShakePosition(0.3f, 0.5f, 10, 90, false, true);
    }
    public void RecoverFromCrash()
    {
        playerState.SetState(PlayerStateType.Moving);
    }
    #endregion 
    #region stop
    public void BeginStop(Transform dock)
    {
        currentDock = dock;
        playerState.SetState(PlayerStateType.Stopping);
    }
    void StopBoat()
    {
        if (speed > 0)
        {
            speed -= Time.deltaTime * stopSpeed;
            rb.linearVelocity = transform.right * speed;
        }
        else
        {
            speed = 0;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            playerState.SetState(PlayerStateType.Rotating);
            playerEvents.PlayerStopped();
        }
    }
    #endregion 
    public void RestartPosition()
    {
        playerState.SetState(PlayerStateType.Paused);
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

    #region BoatDockRotation
    private void Rotate()
    {
        if (currentDock == null) return;

        Vector3 dockDirection = currentDock.TransformDirection(-Vector3.forward);
        dockDirection.y = 0;
        dockDirection.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(dockDirection);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            180f * Time.deltaTime
        );

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        float dot = Vector3.Dot(transform.forward, dockDirection);

        if (dot >= 0.99f)
        {
            ResetAfterArrival();
        }
    }

    public void ResetAfterArrival()
    {
        speed = initialSpeed;
        playerState.SetState(PlayerStateType.Moving);
    }
    #endregion
    public void RecolectFish()
    {
        hasFish = true;
        playerEvents.FishRecolected();
    }
    public void Stop()
    {
        playerState.SetState(PlayerStateType.Paused);
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
    }
    public void canMove()
    {
        playerState.SetState(PlayerStateType.Moving);
    }
}

