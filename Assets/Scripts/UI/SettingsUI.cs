using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private PlanetManager planeManager;
    [SerializeField] private AudioSource musicPlayer;
    
    [Header("UI References")]
    [SerializeField] private TMP_Text signalStrengthButtonStateText;

    [SerializeField] private Slider musicVolumeSlider;
    
    [Header("Settings")]
    [SerializeField] private bool showSignalStrength = true;
    
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

        foreach (var planet in planeManager.Planets)
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
}
