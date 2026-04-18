
using System;
using UnityEngine;

public class CalculationTables : MonoBehaviour
{
    [Header("Resources")]
    [SerializeField] private float baseResourceGeneration = 1;
    [SerializeField] private float baseResourceGenerationMultiplier = 1;
    private float defaultBaseResourceGeneration;
    private float defaultBaseResourceGenerationMultiplier;

    [Header("Costs")]
    [SerializeField] private float baseRelayCost = 10;
    [SerializeField] private float baseRelayCostMultiplier = 1.15f;
    private float  defaultBaseRelayCost;
    private float  defaultBaseRelayCostMultiplier;
    
    [Header("Signal")]
    [SerializeField] private float baseSignalStrength = 1;
    [SerializeField] private float baseSignalStrengthMultiplier = 1; //Updated with tech tree
    private float  defaultBaseSignalStrength;
    private float  defaultBaseSignalStrengthMultiplier;
    
    
    public static CalculationTables Instance;

    private void Awake()
    {
        Instance = this;
        InitDefaultValues();
    }

    private void Start()
    {
        TechUpgradesManager.Instance.OnUpgradeActivated.AddListener(RecalculateValues);
    }

    private void InitDefaultValues()
    {
        defaultBaseResourceGeneration = baseResourceGeneration;
        defaultBaseResourceGenerationMultiplier = baseResourceGenerationMultiplier;
        defaultBaseRelayCost = baseRelayCost;
        defaultBaseRelayCostMultiplier = baseRelayCostMultiplier;
        defaultBaseSignalStrength = baseSignalStrength;
        defaultBaseSignalStrengthMultiplier = baseSignalStrengthMultiplier;
    }
    
    public float GetBaseResourceGenerationValue(int factories)
    {
        return baseResourceGeneration * factories * baseResourceGenerationMultiplier;
    }

    public float GetBaseRelayCost(int relays)
    {
        return  baseRelayCost * relays * baseRelayCostMultiplier;
    }

    public void RecalculateValues(TechUpgrade activatedUpgrade)
    {
        baseResourceGeneration =  defaultBaseResourceGeneration;
        baseResourceGenerationMultiplier =  defaultBaseResourceGenerationMultiplier;
        defaultBaseRelayCost = baseRelayCost;
        defaultBaseRelayCostMultiplier = baseRelayCostMultiplier;
        defaultBaseSignalStrength = baseSignalStrength;
        defaultBaseSignalStrengthMultiplier = baseSignalStrengthMultiplier;
        
        foreach (var upgrade in TechUpgradesManager.Instance.ActiveUpgrades)
        {
            switch (upgrade.UpgradeType)
            {
                case  TechUpgrade.Upgrade_Type.BaseResourceGeneration:
                    baseResourceGeneration += upgrade.UpgradeValue;
                    break;
                case TechUpgrade.Upgrade_Type.ResourceGenerationMultiplier:
                    baseResourceGenerationMultiplier += upgrade.UpgradeValue;
                    break;
                case TechUpgrade.Upgrade_Type.RelayCostMultiplier:
                    baseRelayCost += upgrade.UpgradeValue;
                    break;
                case TechUpgrade.Upgrade_Type.SignalStrengthMultiplier:
                    baseSignalStrength += upgrade.UpgradeValue;
                    break;
                default:
                    Debug.LogError("Unknown upgrade type!");
                    break;
            }
        }
    }
}