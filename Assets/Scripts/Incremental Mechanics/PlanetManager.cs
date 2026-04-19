
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public List<Planet> planets = new();
    public float simulationTime = 1;
    
    [Header("References")]
    [SerializeField] private SharedResourcesManager  sharedResourcesManager;
    [SerializeField] private CalculationTables  calculationTables;
    
    private float _t = 0;
    private void Awake()
    {
        if(planets.Count == 0)
            CollectPlanets();
    }

    private void CollectPlanets()
    {
        planets = FindObjectsByType<Planet>(FindObjectsSortMode.InstanceID).ToList();
    }

    public void Update()
    {
        if (_t < simulationTime)
        {
            _t += Time.deltaTime;
            return;
        }
        
        foreach (var planet in planets)
        {
            UpdatePlanet(planet);
        }

        _t = 0;
    }

    private void UpdatePlanet(Planet planet)
    {
        GenerateResources(planet);
    }

    private void GenerateResources(Planet planet)
    {
        sharedResourcesManager.resources += calculationTables.GetBaseResourceGenerationValue(planet.factories);
    }

    public bool CanBuildFactory(Planet planet)
    {
        return sharedResourcesManager.resources >= calculationTables.GetBaseFactoryCost(planet.factories);
    }

    public void BuildFactory(Planet planet)
    {
        planet.factories++;
    }

    /*public float CalculateSignalDistance(PlanetData planet)
    {
        return planet.relays * CalculationTables.Instance.baseSignalStrength * CalculationTables.Instance.baseSignalStrengthMultiplier;
    }*/

    public float GetRequiredResourcesToBuildFactory(Planet planet)
    {
        return planet.factories > 0 ? planet.factories : 1 * calculationTables.GetBaseFactoryCost(planet.factories);
    }
    
    public float GetRequiredResourcesToBuildFactories(Planet planet, int amount)
    {
        float cost = 0;
        for (int i = 0; i < amount; i++)
        {
            cost +=  calculationTables.GetBaseFactoryCost(planet.factories + i);
        }

        return cost;
    }
    
}