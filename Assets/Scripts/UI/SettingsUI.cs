using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private PlanetManager planetManager;
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private List<AudioSource> effectPlayers;
    
    [Header("UI References")]
    [SerializeField] private TMP_Text signalStrengthButtonStateText;
    [SerializeField] private TMP_Text orbitLineButtonStateText;

    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectVolumeSlider;
    
    
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
        effectVolumeSlider.onValueChanged.AddListener(SetEffectsVolume);
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
    
    private void SetEffectsVolume(float volume)
    {
        foreach (var player in effectPlayers)
        {
            player.volume = volume;
        }
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
