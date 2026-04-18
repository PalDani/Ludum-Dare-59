
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

    public static TechUpgradesManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ActivateUpgrade(string name)
    {
        foreach (var upgrade in inactiveUpgrades)
        {
            if (upgrade.name.Equals(name))
            {
                activeUpgrades.Add(upgrade);
                activeUpgrades.Remove(upgrade);
                return;
            }
        }
        Debug.LogError($"No upgrade found with the name: {name}");
    }
    
}