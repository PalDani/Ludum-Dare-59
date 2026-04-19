using System;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject uiRoot;
    [SerializeField] private PlanetUI planetUI;

    [Header("Hidden UI elements at start")]
    [SerializeField] private List<GameObject> hiddenAtStart;
    
    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void Start()
    {
        foreach (var go in hiddenAtStart)
        {
            go.SetActive(false);
        }
        SettingsUI.Instance.SetOrbitLineDisplayState();
    }

    public void StartGame()
    {
        SettingsUI.Instance.SetOrbitLineDisplayState();
        Time.timeScale = 1;
        uiRoot.SetActive(false);
        foreach (var go in hiddenAtStart)
        {
            go.SetActive(true);
        }
        CameraMovement.Instance.FocusOnTarget(PlanetUI.Instance.CurrentPlanet.transform);
    }
}
