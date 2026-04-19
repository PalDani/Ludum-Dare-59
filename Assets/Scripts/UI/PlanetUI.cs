
using System;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject uiRoot;
    [SerializeField] private PlanetManager planetManager;
    [SerializeField] private SharedResourcesManager sharedResourcesManager;
    
    [Header("Runtime references")]
    [SerializeField] private Planet planet;
    public Planet CurrentPlanet => planet;
    
    
    [Header("Basic UI Elements")]
    [SerializeField] private TMP_Text title;
    [Header("Factory UI")]
    [SerializeField] private GameObject factoryRoot;
    [SerializeField] private TMP_Text factoryText;
    [SerializeField] private Button factoryAddButton;
    [SerializeField] private TMP_Text factoryAddButtonText;
    
    [Header("Relay UI")]
    [SerializeField] private GameObject relayRoot;
    [SerializeField] private TMP_Text relayText;
    [SerializeField] private Button relayAddButton;
    [SerializeField] private TMP_Text relayAddButtonText;
    
    [Header("Orbital DC UI")]
    [SerializeField] private TMP_Text obdcText;
    [SerializeField] private Button obdcBuildButton;
    [SerializeField] private TMP_Text obdcBuildButtonText;
    
    private bool IsOpen => uiRoot.activeSelf;
    
    public static PlanetUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        var p = planet;
        Hide();
        planet = p;
    }

    private void Start()
    {
        //Show(planet);
    }

    private float _t = 0;
    private void Update()
    {
        if(!IsOpen)
            return;

        if (_t <= 1)
        {
            _t += Time.deltaTime;
            return;
        }
        
        RefreshUI();
        _t = 0;
    }

    public void Show(Planet planet)
    {
        this.planet = planet;
        title.text = planet.planetName;
        InitUI();
        
        uiRoot.SetActive(true);
        CameraMovement.Instance.FocusOnTarget(planet.transform);
    }

    public void Hide()
    {
        planet = null;
        uiRoot.SetActive(false);
        if(CameraMovement.Instance != null)
            CameraMovement.Instance.StopFocus();
    }

    private void InitUI()
    {
        if (planet.AllowedToBuildOnSurface)
        {
            factoryAddButton.onClick.RemoveAllListeners();
            factoryAddButton.onClick.AddListener(() =>
            {
                planetManager.BuildFactory(planet);
                RefreshUI();
            });
        }
        else
        {
            factoryRoot.SetActive(false);
        }
        
        relayAddButton.onClick.RemoveAllListeners();
        relayAddButton.onClick.AddListener(() =>
        {
            planetManager.BuildRelay(planet);
            RefreshUI();
        });

        obdcBuildButton.onClick.RemoveAllListeners();
        obdcBuildButton.onClick.AddListener(() =>
        {
            planetManager.BuildOrbitalDataCenter(planet);
            RefreshUI();
        });
        
        RefreshUI();
    }

    private void RefreshUI()
    {
        //Factory
        factoryAddButtonText.text = $"+1 ({Math.Round(planetManager.GetRequiredResourcesToBuildFactory(planet))})";
        factoryAddButton.interactable = planetManager.CanBuildFactory(planet);
        factoryText.text = $"Factories: {planet.Factories}";
        
        //Relay
        relayAddButtonText.text = $"+1 ({Math.Round(planetManager.GetRequiredResourcesToBuildRelay(planet))})";
        relayAddButton.interactable = planetManager.CanBuildRelay(planet);
        relayText.text = $"Relays: {planet.Relays}";
        
        //OBDC
        if(!planet.HasDataCenter())
        {
            obdcBuildButtonText.text = $"Build ({Math.Round(planetManager.GetRequiredResourcesToBuildOrbitalDataCenter(planet))})";
            obdcBuildButton.interactable = planetManager.CanBuildOribtalDataCenter(planet);
        }
        else
        {
            obdcBuildButtonText.text = "Done";
            obdcBuildButton.interactable = false;
        }
        
    }
}