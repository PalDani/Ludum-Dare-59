using TMPro;
using UnityEngine;

public class ResourcesUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SharedResourcesManager sharedResourcesManager;
    [SerializeField] private PlanetManager planetManager;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text resourceText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        resourceText.text = $"Resources:\n{sharedResourcesManager.resources}";
    }
}
