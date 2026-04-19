using System;
using UnityEngine;

public class StartMenuUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject uiRoot;
    [SerializeField] private PlanetUI planetUI;
    
    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void Start()
    {
        SettingsUI.Instance.SetOrbitLineDisplayState();
    }

    public void StartGame()
    {
        SettingsUI.Instance.SetOrbitLineDisplayState();
        Time.timeScale = 1;
        uiRoot.SetActive(false);
        planetUI.Show(PlanetUI.Instance.CurrentPlanet);
    }
}
