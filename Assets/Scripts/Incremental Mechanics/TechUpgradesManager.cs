
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TechUpgradesManager : MonoBehaviour
{
    [SerializeField] private List<TechUpgrade> activeUpgrades;
    [SerializeField] private List<TechUpgrade> inactiveUpgrades;

    public List<TechUpgrade> ActiveUpgrades => activeUpgrades;
    public List<TechUpgrade> InactiveUpgrades => inactiveUpgrades;
    public UnityEvent<TechUpgrade> OnUpgradeActivated;

    public static TechUpgradesManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void ActivateUpgrade(TechUpgrade upgrade)
    {
        if(activeUpgrades.Contains(upgrade) || !inactiveUpgrades.Contains(upgrade))
            return;

        SharedResourcesManager.Instance.RemoveResource(upgrade.UpgradeCost);
        activeUpgrades.Add(upgrade);
        inactiveUpgrades.Remove(upgrade);
        OnUpgradeActivated?.Invoke(upgrade);
        AlertUI.Instance.ShowAlert($"{upgrade.UpgradeName} has been upgraded from the tech tree.");
    }
    
}