
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public List<PlanetData> planets = new();
    public float simulationTime = 1;

    private float _t = 0;
    private void Awake()
    {
        if(planets.Count == 0)
            CollectPlanets();
    }

    private void CollectPlanets()
    {
        planets = FindObjectsByType<PlanetData>(FindObjectsSortMode.InstanceID).ToList();
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

    private void UpdatePlanet(PlanetData planet)
    {
        GenerateResources(planet);
    }

    private void GenerateResources(PlanetData planet)
    {
        SharedResourcesManager.Instance.resources += CalculationTables.Instance.GetBaseResourceGenerationValue(planet.factories);
    }

    /*public float CalculateSignalDistance(PlanetData planet)
    {
        return planet.relays * CalculationTables.Instance.baseSignalStrength * CalculationTables.Instance.baseSignalStrengthMultiplier;
    }*/
}