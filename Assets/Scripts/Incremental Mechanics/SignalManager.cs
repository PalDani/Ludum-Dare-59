
using System;
using System.Collections.Generic;
using UnityEngine;

public class SignalManager : MonoBehaviour
{
    public PlanetManager planetManager;

    [SerializeField] private Dictionary<Planet, float> signals;
    public Dictionary<Planet, float> Signals => signals;

    public static SignalManager Instance { get; private set; }
    
    private void Awake()
    {
        CollectPlanets();
        Instance = this;
    }

    private void CollectPlanets()
    {
        signals = new();
        foreach (var planet in planetManager.Planets)
        {
            signals.Add(planet, planetManager.GetSignalStrength(planet));
        }
    }

    public void UpdateSignals()
    {
        foreach (var planetEntry in signals)
        {
            signals[planetEntry.Key] = planetManager.GetSignalStrength(planetEntry.Key);
        }
    }

    public void UpdateSignalForPlanet(Planet planet)
    {
        signals[planet] = planetManager.GetSignalStrength(planet);
    }
}