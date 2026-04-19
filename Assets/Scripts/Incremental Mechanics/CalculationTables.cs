
using System;
using UnityEngine;

public class CalculationTables : MonoBehaviour
{
    [Header("Resources")]
    [SerializeField] private double baseResourceGeneration = 1;
    [SerializeField] private double baseResourceGenerationMultiplier = 1;
    private double defaultBaseResourceGeneration;
    private double defaultBaseResourceGenerationMultiplier;

    [Header("Costs")]
    [SerializeField] private double baseFactoryCost = 10;
    [SerializeField] private double baseFactoryCostMultiplier = 1.15f;
    [SerializeField] private double baseRelayCost = 10;
    [SerializeField] private double baseRelayCostMultiplier = 1.15f;
    [SerializeField] private double baseOrbitalDataCenterCost = 500;
    [SerializeField] private double baseOrbitalDataCenterCostMultiplier = 1.1f;
    [SerializeField] private double baseColonizationCost = 300;
    [SerializeField] private double baseColonizationCostDistanceMultiplier = 0.5f;
    //Default costs
    private double  defaultBaseRelayCost;
    private double  defaultBaseRelayCostMultiplier;
    private double defaultFactoryCost;
    private double defaultFactoryCostMultiplier;
    private double defaultOrbitalDataCenterCost;
    private double defaultOrbitalDataCenterCostMultiplier;
    
    [Header("Signal")]
    [SerializeField] private double baseSignalStrength = 1;
    [SerializeField] private double baseSignalStrengthMultiplier = 1; //Updated with tech tree
    private double  defaultBaseSignalStrength;
    private double  defaultBaseSignalStrengthMultiplier;
    
    [Header("References")]
    public TechUpgradesManager  techUpgradesManager;
    
    public static CalculationTables Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        InitDefaultValues();
    }

    private void Start()
    {
        techUpgradesManager.OnUpgradeActivated.AddListener(RecalculateValues);
    }

    private void InitDefaultValues()
    {
        defaultBaseResourceGeneration = baseResourceGeneration;
        defaultBaseResourceGenerationMultiplier = baseResourceGenerationMultiplier;
        defaultFactoryCost = baseFactoryCost;
        defaultFactoryCostMultiplier = baseFactoryCostMultiplier;
        defaultBaseRelayCost = baseRelayCost;
        defaultBaseRelayCostMultiplier = baseRelayCostMultiplier;
        defaultBaseSignalStrength = baseSignalStrength;
        defaultBaseSignalStrengthMultiplier = baseSignalStrengthMultiplier;
    }
    
    public double GetBaseResourceGenerationValue(int factories)
    {
        return baseResourceGeneration * factories * baseResourceGenerationMultiplier;
    }

    public double GetBaseFactoryCost(int factories)
    {
        return baseFactoryCost * Math.Pow(baseFactoryCostMultiplier, factories);
    }

    public double GetBaseRelayCost(int relays)
    {
        return baseRelayCost * Math.Pow(baseRelayCostMultiplier, relays);
    }

    public double GetOrbitalDataCenterCost(int obdcs)
    {
        return baseOrbitalDataCenterCost * Math.Pow(baseOrbitalDataCenterCostMultiplier, obdcs);
    }

    public double GetSignalStrength(Planet planet)
    {
        return planet.Relays * baseSignalStrength * baseSignalStrengthMultiplier;
    }

    public double GetColonizationCost(Planet planet)
    {
        return baseColonizationCost;
    }

    public void RecalculateValues(TechUpgrade activatedUpgrade)
    {
        baseResourceGeneration = defaultBaseResourceGeneration;
        baseResourceGenerationMultiplier = defaultBaseResourceGenerationMultiplier;

        baseFactoryCost = defaultFactoryCost;
        baseFactoryCostMultiplier = defaultFactoryCostMultiplier;

        baseRelayCost = defaultBaseRelayCost;
        baseRelayCostMultiplier = defaultBaseRelayCostMultiplier;

        baseSignalStrength = defaultBaseSignalStrength;
        baseSignalStrengthMultiplier = defaultBaseSignalStrengthMultiplier;
        
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
                case TechUpgrade.Upgrade_Type.FactoryCostMultiplier:
                    baseFactoryCostMultiplier += upgrade.UpgradeValue;
                    break;
                case TechUpgrade.Upgrade_Type.RelayCostMultiplier:
                    baseRelayCostMultiplier += upgrade.UpgradeValue;
                    break;
                case TechUpgrade.Upgrade_Type.SignalStrengthMultiplier:
                    baseSignalStrengthMultiplier += upgrade.UpgradeValue;
                    break;
                default:
                    Debug.LogError("Unknown upgrade type!");
                    break;
            }
        }
        
        Debug.Log("Recalculated values");
    }
}