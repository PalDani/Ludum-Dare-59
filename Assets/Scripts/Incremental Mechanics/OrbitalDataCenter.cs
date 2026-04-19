
using System;
using UnityEngine;

public class OrbitalDataCenter : MonoBehaviour
{
    [SerializeField] private Planet planet;
    [SerializeField] private CelestialBody celestialBody;

    private void Start()
    {
        
    }

    public void Init(Planet planet)
    {
        this.planet = planet;
        celestialBody.orbitCenter = planet.transform;
        celestialBody.radiusX *= planet.transform.localScale.x;
        celestialBody.radiusZ *= planet.transform.localScale.x;
        celestialBody.orbitRadius *= planet.transform.localScale.x;
        SignalManager.Instance.AddOrbitalDataCenter(this);
    }
}