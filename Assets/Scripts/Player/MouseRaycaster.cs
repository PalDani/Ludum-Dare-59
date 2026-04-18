using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRaycaster : MonoBehaviour
{
    public InputActionReference clickAction;
    public LayerMask interactableLayer;

    public Interactable currentTarget;
    private Interactable lastTarget;

    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        clickAction.action.Enable();
        clickAction.action.performed += Clicked;
    }

    private void OnDisable()
    {
        clickAction.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        CheckRaycast();
    }

    private void CheckRaycast()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, interactableLayer))
        {
            Interactable newTarget = hit.collider.GetComponent<Interactable>();
            currentTarget = newTarget;
            
            if (newTarget != lastTarget)
            {
                if (lastTarget != null)
                    lastTarget.HoverExit();

                if (newTarget != null)
                    newTarget.HoverEnter();

                lastTarget = newTarget;
            }
        }
        else
        {
            if (lastTarget != null)
            {
                lastTarget.HoverExit();
                lastTarget = null;
            }

            currentTarget = null;
        }
    }

    public void Clicked(InputAction.CallbackContext context)
    {
        if(currentTarget == null)
            return;
        
        currentTarget.Interact();
    }
}
