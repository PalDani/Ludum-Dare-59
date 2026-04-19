using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class SettingsUI : MonoBehaviour
{

    [Header("References")]
    [SerializeField] public PlanetManager planeManager;
    [Header("UI References")]
    [SerializeField] private TMP_Text signalStrengthButtonStateText;
    
    [Header("Settings")]
    [SerializeField] private bool showSignalStrength = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
