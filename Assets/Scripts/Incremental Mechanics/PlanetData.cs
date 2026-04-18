using System;
using UnityEngine;

public class PlanetData : MonoBehaviour
{
    public int factories = 0;
    public int robots = 0;
    public int relays = 0;

    public OrbitalDataCenter dataCenter;

    private void Awake()
    {
        tag = "CelestialBody";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool HasDataCenter() => dataCenter != null;
}
