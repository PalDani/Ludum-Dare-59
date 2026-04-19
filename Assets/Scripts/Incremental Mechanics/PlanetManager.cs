
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    [SerializeField] private List<Planet> planets = new();
    public List<Planet> Planets => planets;
    public float simulationTime = 1;
    
    [Header("References")]
    [SerializeField] private SharedResourcesManager  sharedResourcesManager;
    [SerializeField] private CalculationTables  calculationTables;
    
    private float _t = 0;
    
    public static  PlanetManager Instance { get;  private set; }
    
    private void Awake()
    {
        Instance = this;
        
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
        sharedResourcesManager.resources += calculationTables.GetBaseResourceGenerationValue(planet.Factories);
    }

    public bool CanBuildFactory(Planet planet)
    {
        return sharedResourcesManager.resources >= GetRequiredResourcesToBuildFactory(planet);
    }

    public bool CanBuildRelay(Planet planet)
    {
        return sharedResourcesManager.resources >= GetRequiredResourcesToBuildRelay(planet);
    }
    
    public bool CanBuildOribtalDataCenter(Planet planet)
    {
        return sharedResourcesManager.resources >= GetRequiredResourcesToBuildRelay(planet);
    }

    public bool CanBeColonized(Planet planet)
    {
        return 
            sharedResourcesManager.resources >= GetRequiredResourcesToColonize(planet) &&
            planet.IsPlanetInSignalRange();
    }

    public void BuildFactory(Planet planet)
    {
        SharedResourcesManager.Instance.resources -= GetRequiredResourcesToBuildFactory(planet);
        planet.BuildFactory();
    }

    public float GetSignalStrength(Planet planet) => calculationTables.GetSignalStrength(planet);

    public void BuildOrbitalDataCenter(Planet planet)
    {
        
    }

    public void BuildRelay(Planet planet)
    {
        SharedResourcesManager.Instance.resources -= GetRequiredResourcesToBuildRelay(planet);
        planet.BuildRelay();
    }

    public float GetDistanceBetweenPlanets(Planet p1, Planet p2)
    {
        return Vector3.Distance(p1.transform.position, p2.transform.position);
    }

    /*public float CalculateSignalDistance(PlanetData planet)
    {
        return planet.relays * CalculationTables.Instance.baseSignalStrength * CalculationTables.Instance.baseSignalStrengthMultiplier;
    }*/

    public float GetRequiredResourcesToColonize(Planet planet)
    {
        return calculationTables.GetColonizationCost(planet);
    }

    public float GetRequiredResourcesToBuildFactory(Planet planet)
    {
        return calculationTables.GetBaseFactoryCost(planet.Factories);
    }

    public float GetRequiredResourcesToBuildRelay(Planet planet)
    {
        return calculationTables.GetBaseRelayCost(planet.Relays);
    }
    
    public float GetRequiredResourcesToBuildOrbitalDataCenter(Planet planet)
    {
        return calculationTables.GetOrbitalDataCenterCost(GetOrbitalDataCenterCount());
    }

    public int GetOrbitalDataCenterCount()
    {
        int result = 0;
        foreach (var planet in planets)
        {
            if (planet.HasDataCenter())
                result++;
        }
        return result;
    }
    
}