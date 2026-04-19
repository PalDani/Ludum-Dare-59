using System;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public string planetName;
    public int factories = 0;
    public int robots = 0;
    public int relays = 0;
    public bool allowedToBuildOnSurface = true;

    [Header("References")]
    [SerializeField] private Interactable interactable;
    public OrbitalDataCenter dataCenter;

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
        PlanetUI.Instance.Show(this);
    }
    
}
