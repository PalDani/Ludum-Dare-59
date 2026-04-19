
using System;
using System.Collections.Generic;
using UnityEngine;

public class TechTreeUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TechUpgradesManager techUpgradesManager;
    [SerializeField] private Transform upgradeElementsParent;
    [SerializeField] private GameObject uiRoot;

    [Header("Runtime")]
    [SerializeField] private List<TechUpgradeUIElement> upgradeUIElements;
    private float _t = 0;
    
    public static TechTreeUI Instance { get; private set; }
    
    public bool IsOpen => uiRoot.activeSelf;
    
    private void Awake()
    {
        Instance = this;
        CollectUpgrades();
        Hide();
    }

    private void Update()
    {
        if(!IsOpen)
            return;
        
        if (_t <= 2)
        {
            _t += Time.deltaTime;
            return;
        }
        
        RefreshUI();
        _t = 0;
    }

    private void CollectUpgrades()
    {
        foreach (Transform child in upgradeElementsParent)
        {
            if (child.TryGetComponent<TechUpgradeUIElement>(out var tue))
            {
                techUpgradesManager.InactiveUpgrades.Add(tue.TechUpgrade);
                upgradeUIElements.Add(tue);
            }
        }
    }

    public void RefreshUI()
    {
        foreach (var tue in upgradeUIElements)
        {
            if(tue.Bought)
                continue;
            
            tue.Refresh();
        }
    }
    
    public void Show()
    {
        RefreshUI();
        uiRoot.SetActive(true);
    }

    public void Hide()
    {
        uiRoot.SetActive(false);
    }
}