
using System;
using UnityEngine;

public class CalculationTables : MonoBehaviour
{
    [Header("Resources")]
    public float baseResourceGeneration = 1;

    [Header("Costs")]
    public float baseRelayCost = 10;
    public float baseRelayCostMultiplier = 1.15f;
    [Header("Signal")]
    public float baseSignalStrength = 1;
    public float baseSignalStrengthMultiplier = 1; //Updated with tech tree
    
    public static CalculationTables Instance;

    private void Awake()
    {
        Instance = this;
    }
}