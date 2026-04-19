
using System;
using System.Collections.Generic;
using UnityEngine;

public class SignalManager : MonoBehaviour
{
    public PlanetManager planetManager;

    [SerializeField] private Dictionary<Planet, double> signals;
    public Dictionary<Planet, double> Signals => signals;
    public List<OrbitalDataCenter> orbitalDataCenters = new();
    
    private List<Planet> planetsDiscovered = new();

    public static SignalManager Instance { get; private set; }

    private float _t;
    
    private void Awake()
    {
        CollectPlanets();
        Instance = this;
    }

    private void Update()
    {
        if (_t < 2)
        {
            _t += Time.deltaTime;
            return;
        }

        CheckForNewPlanetInSignalRange();
        _t = 0;
    }

    private void CollectPlanets()
    {
        signals = new();
        foreach (var planet in planetManager.Planets)
        {
            signals.Add(planet, planetManager.GetSignalStrength(planet));
            if(planet.IsStarterPlanet)
                planetsDiscovered.Add(planet);
        }
    }

    public void UpdateSignals()
    {
        foreach (var signalEntry in signals)
        {
            signals[signalEntry.Key] = planetManager.GetSignalStrength(signalEntry.Key);
        }
    }

    public void UpdateSignalForPlanet(Planet planet)
    {
        signals[planet] = planetManager.GetSignalStrength(planet);
        CheckForNewPlanetInSignalRange();
    }

    public void AddOrbitalDataCenter(OrbitalDataCenter orbitalDataCenter)
    {
        orbitalDataCenters.Add(orbitalDataCenter);
        DataCenterSignalDisplay.Instance.RebuildLines();
    }

    private void CheckForNewPlanetInSignalRange()
    {
        foreach (var signalEntry in signals)
        {
            if (signalEntry.Key.IsPlanetInSignalRange() && !planetsDiscovered.Contains(signalEntry.Key))
            {
                planetsDiscovered.Add(signalEntry.Key);
                Debug.Log($"{signalEntry.Key.planetName} can now be colonized.");
                AlertUI.Instance.ShowAlert($"{signalEntry.Key.planetName} can now be colonized.");
            }
        }
    }
}