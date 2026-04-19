using System;
using TMPro;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public string planetName;
    [SerializeField] private int factories = 0;
    [SerializeField] private  int relays = 0;
    [SerializeField] private  bool starterPlanet = false;
    [SerializeField] private  bool colonized = false;
    [SerializeField] private bool allowedToBuildOnSurface = true;

    [Header("References")]
    [SerializeField] private Interactable interactable;
    [SerializeField] private TMP_Text planetLabel;
    public OrbitalDataCenter dataCenter;
    public GameObject signalStrengthDisplay;
    [SerializeField] private OrbitLineDisplay orbitLineDisplay;

    public int Factories => factories;
    public int Relays => relays;
    public bool Colonized => colonized;
    public bool IsStarterPlanet => starterPlanet;
    public bool AllowedToBuildOnSurface => allowedToBuildOnSurface;

    private void Awake()
    {
        tag = "CelestialBody";
        planetLabel.text =  planetName;
        
        if(interactable == null)
            interactable = GetComponent<Interactable>();
        
        interactable.OnInteract.AddListener(ShowPlanetUI);
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

    public void ShowPlanetUI()
    {
        if(colonized && IsPlanetInSignalRange() || starterPlanet)
            PlanetUI.Instance.Show(this);
        else if(!colonized)
        {
            ColonizePlanetUI.Instance.Show(this);
        }
    }

    public void Colonize()
    {
        colonized = true;
    }

    public void BuildFactory()
    {
        factories++;
    }

    public void BuildRelay()
    {
        relays++;
        UpdateSignalStrengthDisplay();
        SignalManager.Instance.UpdateSignalForPlanet(this);
    }

    public void BuildOrbitalDataCenter()
    {
        var obdcPrefab = Instantiate(PlanetManager.Instance.OrbitalDataCenterPrefab, PlanetManager.Instance.StarSystemRoot);
        dataCenter = obdcPrefab.GetComponent<OrbitalDataCenter>();
        dataCenter.Init(this);
    }
    
    public void UpdateSignalStrengthDisplay()
    {
        float signalRadius = (float) Math.Round(CalculationTables.Instance.GetSignalStrength(this));
        float targetWorldDiameter = signalRadius * 2f;

        Transform displayTransform = signalStrengthDisplay.transform;
        Transform parent = displayTransform.parent;

        Vector3 parentLossyScale = parent != null ? parent.lossyScale : Vector3.one;

        float localX = targetWorldDiameter / parentLossyScale.x;
        float localZ = targetWorldDiameter / parentLossyScale.z;

        displayTransform.localScale = new Vector3(localX, 1, localZ);
    }

    public bool IsPlanetInSignalRange()
    {
        if (HasDataCenter())
            return true;
        
        foreach (var signal in SignalManager.Instance.Signals)
        {
            if (signal.signalStrength <= 0f)
                continue;

            float distance = PlanetManager.Instance.GetDistanceBetweenPlanets(this, signal.planet);

            if (distance <= signal.signalStrength)
                return true;
        }

        return false;
    }

    public void SetSignalDisplayState(bool state)
    {
        signalStrengthDisplay.SetActive(state);
    }
    

    public void SetOrbitLineDisplayState(bool state)
    {
        orbitLineDisplay.SetLineState(state);
    }
    
}
