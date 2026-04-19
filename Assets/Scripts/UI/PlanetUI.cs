
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject uiObjectRoot;
    [SerializeField] private PlanetManager planetManager;
    [SerializeField] private SharedResourcesManager sharedResourcesManager;
    
    [Header("Runtime references")]
    [SerializeField] private Planet planet;
    
    
    [Header("Basic UI Elements")]
    [SerializeField] private TMP_Text title;
    [Header("Factory UI")]
    [SerializeField] private TMP_Text factoryText;
    [SerializeField] private Button factoryAddButton;
    [SerializeField] private TMP_Text factoryAddButtonText;
    
    private bool IsOpen => uiObjectRoot.activeSelf;
    
    public static PlanetUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        Hide();
    }

    public void Show(Planet planet)
    {
        this.planet = planet;
        title.text = planet.planetName;
        InitUI();
        
        uiObjectRoot.SetActive(true);
    }

    public void Hide()
    {
        planet = null;
        uiObjectRoot.SetActive(false);
    }

    private void InitUI()
    {
        factoryAddButton.onClick.RemoveAllListeners();
        factoryText.text = $"Factories: {planet.factories}";
        
        //Add event listeners
        factoryAddButton.onClick.AddListener(() =>
        {
            planetManager.BuildFactory(planet);
            RefreshUI();
        });
        
        RefreshUI();
    }

    private void RefreshUI()
    {
        factoryAddButtonText.text = $"+1 ({planetManager.GetRequiredResourcesToBuildFactory(planet)})";
        factoryAddButton.interactable = planetManager.CanBuildFactory(planet);
    }
}