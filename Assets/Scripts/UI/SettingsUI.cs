using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private PlanetManager planetManager;
    [SerializeField] private AudioSource musicPlayer;
    
    [Header("UI References")]
    [SerializeField] private TMP_Text signalStrengthButtonStateText;
    [SerializeField] private TMP_Text orbitLineButtonStateText;

    [SerializeField] private Slider musicVolumeSlider;
    
    [Header("Settings")]
    [SerializeField] private bool showSignalStrength = true;
    [SerializeField] private bool showOrbitLine = true;
    
    public static SettingsUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SwitchSignalStrengthState()
    {
        showSignalStrength = !showSignalStrength;
        signalStrengthButtonStateText.text =  showSignalStrength ? "Hide" : "Show";

        foreach (var planet in planetManager.Planets)
        {
            planet.SetSignalDisplayState(showSignalStrength);
        }
    }

    public void SetGameSpeed(int speed)
    {
        Time.timeScale = speed;
    }

    private void SetMusicVolume(float volume)
    {
        musicPlayer.volume = volume;
    }

    public void SetOrbitLineDisplayState()
    {
        showOrbitLine = !showOrbitLine;
        orbitLineButtonStateText.text = showOrbitLine ? "Hide" : "Show";

        foreach (var planet in planetManager.Planets)
        {
            planet.SetOrbitLineDisplayState(showOrbitLine);
        }
    }
}
