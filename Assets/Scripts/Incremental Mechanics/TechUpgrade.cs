
using UnityEngine;

public class TechUpgrade : ScriptableObject
{
    
    public string UpgradeName;
    public Upgrade_Type UpgradeType;
    public float UpgradeValue;
    
    public enum Upgrade_Type
    {
        BaseResourceGeneration,
        ResourceGenerationMultiplier,
        FactoryCostMultiplier,
        RelayCostMultiplier,
        OrbitalDataCenterMultiplier,
        SignalStrengthMultiplier,
    }
}