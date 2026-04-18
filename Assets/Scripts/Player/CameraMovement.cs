using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference moveAction;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float smoothTime = 0.12f;

    [Header("Bounds")]
    [SerializeField] private Vector2 xBounds = new Vector2(-50f, 50f);
    [SerializeField] private Vector2 zBounds = new Vector2(-50f, 50f);

    private Vector3 currentVelocity;
    private Vector3 targetPosition;

    private void OnEnable()
    {
        if (moveAction != null)
            moveAction.action.Enable();
    }

    private void OnDisable()
    {
        if (moveAction != null)
            moveAction.action.Disable();
    }

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (moveAction == null)
            return;

        Vector2 input = moveAction.action.ReadValue<Vector2>();

        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);

        targetPosition += moveDirection * moveSpeed * Time.deltaTime;

        targetPosition.x = Mathf.Clamp(targetPosition.x, xBounds.x, xBounds.y);
        targetPosition.z = Mathf.Clamp(targetPosition.z, zBounds.x, zBounds.y);

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref currentVelocity,
            smoothTime
        );
    }
}