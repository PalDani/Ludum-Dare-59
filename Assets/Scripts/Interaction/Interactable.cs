
using System;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    
    public UnityEvent OnInteract;
    public UnityEvent OnHoverEnter;
    public UnityEvent OnHoverExit;
    
    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public virtual void Interact()
    {
        //Debug.Log($"Interacted with {gameObject.name}", gameObject);
        OnInteract?.Invoke();
    }

    public virtual void HoverEnter()
    {
        OnHoverEnter?.Invoke();
    }

    public virtual void HoverExit()
    {
        OnHoverExit?.Invoke();
    }
}