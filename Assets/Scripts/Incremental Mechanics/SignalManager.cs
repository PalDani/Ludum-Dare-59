
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SignalManager : MonoBehaviour
{
    public PlanetManager planetManager;
    public TechUpgradesManager techUpgradesManager;

    [SerializeField] private List<SignalEntry> signals = new();
    public List<SignalEntry> Signals => signals;
    public List<OrbitalDataCenter> orbitalDataCenters = new();
    
    private List<Planet> planetsDiscovered = new();

    public static SignalManager Instance { get; private set; }

    private float _t;
    
    private void Awake()
    {
        CollectPlanets();
        Instance = this;
        techUpgradesManager.OnUpgradeActivated.AddListener((x) => UpdateSignals());
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
            signals.Add(new SignalEntry()
            {
                planet  = planet,
                signalStrength = planetManager.GetSignalStrength(planet)
            });
            if(planet.IsStarterPlanet)
                planetsDiscovered.Add(planet);
        }
    }

    public void UpdateSignals()
    {
        foreach (var signalEntry in signals)
        {
            UpdateSignalForPlanet(signalEntry.planet);
        }
    }

    public void UpdateSignalForPlanet(Planet planet)
    {
        signals.First(x => x.planet == planet).signalStrength = planetManager.GetSignalStrength(planet);
        planet.UpdateSignalStrengthDisplay();
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
            if (signalEntry.planet.IsPlanetInSignalRange() && !planetsDiscovered.Contains(signalEntry.planet))
            {
                planetsDiscovered.Add(signalEntry.planet);
                AlertUI.Instance.ShowAlert($"{signalEntry.planet.planetName} can now be discovered.");
            }
        }
    }

    [Serializable]
    public class SignalEntry
    {
        public Planet planet;
        public double signalStrength;
    }
}