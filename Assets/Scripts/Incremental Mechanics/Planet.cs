using System;
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
    public OrbitalDataCenter dataCenter;
    public GameObject signalStrengthDisplay;

    public int Factories => factories;
    public int Relays => relays;

    private void Awake()
    {
        tag = "CelestialBody";
        
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
        if(IsPlanetInSignalRange() || starterPlanet)
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
        
    }
    
    private void UpdateSignalStrengthDisplay()
    {
        float scale = CalculationTables.Instance.GetSignalStrength(this);
        
        signalStrengthDisplay.transform.localScale = new Vector3(scale, scale, scale);
    }

    public bool IsPlanetInSignalRange()
    {
        foreach (var signal in SignalManager.Instance.Signals)
        {
            if(signal.Value <= PlanetManager.Instance.GetDistanceBetweenPlanets(this, signal.Key))
            {
                return true;
            }
        }

        return false;
    }

    public void SetSignalDisplayState(bool state)
    {
        signalStrengthDisplay.SetActive(state);
    }
    
}
