using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference zoomAction;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float smoothTime = 0.12f;

    [Header("Zoom")]
    [SerializeField] private float zoomSpeed = 15f;
    [SerializeField] private float minY = 10f;
    [SerializeField] private float maxY = 80f;

    [Header("Bounds")]
    [SerializeField] private Vector2 xBounds = new Vector2(-50f, 50f);
    [SerializeField] private Vector2 zBounds = new Vector2(-50f, 50f);

    [Header("Focus")]
    [SerializeField] private Vector3 focusOffset = new Vector3(0f, 12f, -12f);
    [SerializeField] private bool useCustomFocusOffset = false;

    private Vector3 currentVelocity;
    private Vector3 targetPosition;

    private Transform focusTarget;
    private bool isFocused;
    private Vector3 currentFocusOffset;

    public static CameraMovement Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        moveAction?.action.Enable();
        zoomAction?.action.Enable();
    }

    private void OnDisable()
    {
        moveAction?.action.Disable();
        zoomAction?.action.Disable();
    }

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (isFocused && focusTarget != null)
        {
            HandleFocusedCamera();
        }
        else
        {
            HandleFreeCamera();
        }

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref currentVelocity,
            smoothTime
        );
    }

    private void HandleFreeCamera()
    {
        if (moveAction == null)
            return;

        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        targetPosition += moveDirection * moveSpeed * Time.unscaledDeltaTime;

        if (zoomAction != null)
        {
            Vector2 zoomInput = zoomAction.action.ReadValue<Vector2>();
            float zoomValue = zoomInput.y;

            Vector3 zoomDelta = transform.forward * zoomValue * zoomSpeed * Time.unscaledDeltaTime;
            Vector3 candidate = targetPosition + zoomDelta;

            if (candidate.y >= minY && candidate.y <= maxY)
            {
                targetPosition = candidate;
            }
        }

        ClampTargetPosition();
    }

    private void HandleFocusedCamera()
    {
        if (focusTarget == null)
        {
            StopFocus();
            return;
        }

        if (zoomAction != null)
        {
            Vector2 zoomInput = zoomAction.action.ReadValue<Vector2>();
            float zoomValue = zoomInput.y;

            Vector3 zoomDelta = transform.forward * zoomValue * zoomSpeed * Time.unscaledDeltaTime;
            Vector3 candidateOffset = currentFocusOffset + zoomDelta;

            float distance = candidateOffset.magnitude;
            float minDistance = 8f;
            float maxDistance = 40f;

            if (distance >= minDistance && distance <= maxDistance)
            {
                currentFocusOffset = candidateOffset;
            }
        }

        targetPosition = focusTarget.position + currentFocusOffset;
    }

    private void ClampTargetPosition()
    {
        targetPosition.x = Mathf.Clamp(targetPosition.x, xBounds.x, xBounds.y);
        targetPosition.z = Mathf.Clamp(targetPosition.z, zBounds.x, zBounds.y);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
    }

    public void FocusOnTarget(Transform target)
    {
        if (target == null)
            return;

        focusTarget = target;
        isFocused = true;

        if (useCustomFocusOffset)
            currentFocusOffset = focusOffset;
        else
            currentFocusOffset = transform.position - target.position;

        targetPosition = focusTarget.position + currentFocusOffset;
    }

    public void StopFocus()
    {
        isFocused = false;
        focusTarget = null;
        targetPosition = transform.position;
    }

    public bool IsFocused()
    {
        return isFocused;
    }
}