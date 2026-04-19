
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
    [SerializeField] private Transform starSystemRoot;
    [Header("Prefabs")]
    [SerializeField] private GameObject orbitalDataCenterPrefab;
    public GameObject OrbitalDataCenterPrefab => orbitalDataCenterPrefab;
    public Transform StarSystemRoot =>  starSystemRoot;
    
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
        sharedResourcesManager.AddResource(calculationTables.GetBaseResourceGenerationValue(planet.Factories));
    }

    public bool CanBuildFactory(Planet planet)
    {
        return sharedResourcesManager.Resources >= GetRequiredResourcesToBuildFactory(planet);
    }

    public bool CanBuildRelay(Planet planet)
    {
        return sharedResourcesManager.Resources >= GetRequiredResourcesToBuildRelay(planet);
    }
    
    public bool CanBuildOribtalDataCenter(Planet planet)
    {
        return sharedResourcesManager.Resources >= GetRequiredResourcesToBuildOrbitalDataCenter(planet);
    }

    public bool CanBeColonized(Planet planet)
    {
        return 
            sharedResourcesManager.Resources >= GetRequiredResourcesToColonize(planet) &&
            planet.IsPlanetInSignalRange();
    }

    public void BuildFactory(Planet planet)
    {
        SharedResourcesManager.Instance.RemoveResource(GetRequiredResourcesToBuildFactory(planet));
        planet.BuildFactory();
    }

    public double GetSignalStrength(Planet planet) => calculationTables.GetSignalStrength(planet);

    public void BuildOrbitalDataCenter(Planet planet)
    {
        SharedResourcesManager.Instance.RemoveResource(GetRequiredResourcesToBuildOrbitalDataCenter(planet));
        planet.BuildOrbitalDataCenter();
    }

    public void BuildRelay(Planet planet)
    {
        SharedResourcesManager.Instance.RemoveResource(GetRequiredResourcesToBuildRelay(planet));
        planet.BuildRelay();
    }

    public float GetDistanceBetweenPlanets(Planet p1, Planet p2)
    {
        return Vector3.Distance(p1.transform.position, p2.transform.position);
    }

    public double GetRequiredResourcesToColonize(Planet planet)
    {
        return calculationTables.GetColonizationCost(planet);
    }

    public double GetRequiredResourcesToBuildFactory(Planet planet)
    {
        return calculationTables.GetBaseFactoryCost(planet.Factories);
    }

    public double GetRequiredResourcesToBuildRelay(Planet planet)
    {
        return calculationTables.GetBaseRelayCost(planet.Relays);
    }
    
    public double GetRequiredResourcesToBuildOrbitalDataCenter(Planet planet)
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