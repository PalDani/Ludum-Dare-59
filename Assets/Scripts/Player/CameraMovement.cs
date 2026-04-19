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

    private Vector3 currentVelocity;
    private Vector3 targetPosition;

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
        if (moveAction == null)
            return;

        // WASD movement on horizontal plane
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        targetPosition += moveDirection * moveSpeed * Time.unscaledDeltaTime;

        // Zoom from scroll wheel / Vector2 action
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

        // Clamp bounds
        targetPosition.x = Mathf.Clamp(targetPosition.x, xBounds.x, xBounds.y);
        targetPosition.z = Mathf.Clamp(targetPosition.z, zBounds.x, zBounds.y);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref currentVelocity,
            smoothTime
        );
    }
}