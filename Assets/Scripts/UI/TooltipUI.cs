
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TooltipUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform uiRoot;
    [SerializeField] private TMP_Text tooltipText;
    
    [Header("Settings")]
    [SerializeField] private Vector2 tooltipOffset;
    
    private bool IsEnabled => uiRoot.gameObject.activeSelf;
    public static TooltipUI Instance { get;  private set; }
    
    public void SetTooltip(string text)
    {
        tooltipText.text = text;
    }

    private void Awake()
    {
        Instance = this;
        SetTooltipState(false);
    }

    private void Update()
    {
        if(!IsEnabled)
            return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        transform.position = mousePos + tooltipOffset;
    }

    public void SetTooltipState(bool state)
    {
        uiRoot.gameObject.SetActive(state);
    }
}