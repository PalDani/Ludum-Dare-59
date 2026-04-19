#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Tech Upgrade")]
public class TechUpgrade : ScriptableObject
{
    public string UpgradeName;
    [TextArea]
    public string UpgradeDescription;
    public float UpgradeCost;
    public Upgrade_Type UpgradeType;
    public float UpgradeValue;

    public enum Upgrade_Type
    {
        BaseResourceGeneration,
        ResourceGenerationMultiplier,
        FactoryCostMultiplier,
        RelayCostMultiplier,
        SignalStrengthMultiplier,
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        UpgradeName = name;
    }
#endif
}