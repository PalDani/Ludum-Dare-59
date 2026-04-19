
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColonizePlanetUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject uiRoot;

    [SerializeField] private Planet planet;
    
    [Header("UI Elements")]
    public TMP_Text title;
    public Button colonizeButton;
    public TMP_Text colonizeButtonText;
    
    public static ColonizePlanetUI Instance { get; private set; }

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
        
        uiRoot.SetActive(true);
    }

    public void Hide()
    {
        planet = null;
        uiRoot.SetActive(false);
    }

    private void InitUI()
    {
        colonizeButton.onClick.RemoveAllListeners();
        colonizeButton.onClick.AddListener(() =>
        {
            planet.Colonize();
            Hide();
        });

        colonizeButton.interactable = PlanetManager.Instance.CanBeColonized(planet);
        colonizeButtonText.text = $"Colonize {CalculationTables.Instance.GetColonizationCost(planet)}";
    }
    
    
}