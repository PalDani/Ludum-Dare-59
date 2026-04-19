
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
    [SerializeField] private float baseFactoryCost = 10;
    [SerializeField] private float baseFactoryCostMultiplier = 1.15f;
    [SerializeField] private float baseRelayCost = 10;
    [SerializeField] private float baseRelayCostMultiplier = 1.15f;
    [SerializeField] private float baseOrbitalDataCenterCost = 500;
    [SerializeField] private float baseOrbitalDataCenterCostMultiplier = 1.1f;
    [SerializeField] private float baseColonizationCost = 300;
    [SerializeField] private float baseColonizationCostDistanceMultiplier = 0.5f;
    //Default costs
    private float  defaultBaseRelayCost;
    private float  defaultBaseRelayCostMultiplier;
    private float defaultFactoryCost;
    private float defaultFactoryCostMultiplier;
    private float defaultOrbitalDataCenterCost;
    private float defaultOrbitalDataCenterCostMultiplier;
    
    [Header("Signal")]
    [SerializeField] private float baseSignalStrength = 1;
    [SerializeField] private float baseSignalStrengthMultiplier = 1; //Updated with tech tree
    private float  defaultBaseSignalStrength;
    private float  defaultBaseSignalStrengthMultiplier;
    
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
    
    public float GetBaseResourceGenerationValue(int factories)
    {
        return baseResourceGeneration * factories * baseResourceGenerationMultiplier;
    }

    public float GetBaseFactoryCost(int factories)
    {
        factories = factories > 0 ? factories : 1;
        return baseFactoryCost * baseFactoryCostMultiplier * factories;
    }

    public float GetBaseRelayCost(int relays)
    {
        relays = relays > 0 ? relays : 1;
        return  baseRelayCost * relays * baseRelayCostMultiplier;
    }

    public float GetOrbitalDataCenterCost(int obdcs)
    {
        obdcs =  obdcs > 0 ? obdcs : 1;
        return baseOrbitalDataCenterCost * obdcs * baseOrbitalDataCenterCostMultiplier;
    }

    public float GetSignalStrength(Planet planet)
    {
        return planet.Relays * baseSignalStrength * baseSignalStrengthMultiplier;
    }

    public float GetColonizationCost(Planet planet)
    {
        return baseColonizationCost;
    }

    public void RecalculateValues(TechUpgrade activatedUpgrade)
    {
        baseResourceGeneration =  defaultBaseResourceGeneration;
        baseResourceGenerationMultiplier =  defaultBaseResourceGenerationMultiplier;
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
                    baseRelayCost += upgrade.UpgradeValue;
                    break;
                case TechUpgrade.Upgrade_Type.OrbitalDataCenterMultiplier:
                    defaultOrbitalDataCenterCost += upgrade.UpgradeValue;
                    break;
                case TechUpgrade.Upgrade_Type.SignalStrengthMultiplier:
                    baseSignalStrength += upgrade.UpgradeValue;
                    break;
                default:
                    Debug.LogError("Unknown upgrade type!");
                    break;
            }
        }
        
        Debug.Log("Recalculated values");
    }
}