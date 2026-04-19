using System;
using UnityEngine;

public class SharedResourcesManager : MonoBehaviour
{

    [SerializeField] private double resources;
    public double Resources => resources;
    
    public static SharedResourcesManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddResource(double amount)
    {
        resources += amount;
        
        if (double.IsNaN(resources) || double.IsInfinity(resources))
            resources = double.MaxValue;

        resources = Math.Clamp(resources, 0.0, double.MaxValue);
    }

    public void RemoveResource(double amount)
    {
        resources -= amount;
        
        if (double.IsNaN(resources) || double.IsInfinity(resources))
            resources = double.MaxValue;
        
        resources = Math.Clamp(resources, 0.0, double.MaxValue);
    }
}
