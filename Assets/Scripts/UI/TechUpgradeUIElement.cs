
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TechUpgradeUIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    [SerializeField] private TechUpgrade techUpgrade;
    [SerializeField] private Button  upgradeButton;
    [SerializeField] private TMP_Text  upgradeButtonText;

    public TechUpgrade TechUpgrade => techUpgrade;
    
    private bool bought = false;
    public bool Bought =>  bought;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        upgradeButton.onClick.AddListener(BuyUpgrade);
        upgradeButtonText.text = $"{TechUpgrade.UpgradeName} ({TechUpgrade.UpgradeCost})";
        gameObject.name = $"[UPGRADE] {TechUpgrade.UpgradeName}";
    }

    public bool CanBeBought()
    {
        return SharedResourcesManager.Instance.Resources >= techUpgrade.UpgradeCost;
    }

    public void Refresh()
    {
        if (bought)
            return;
            
        upgradeButton.interactable = CanBeBought();
    }

    public void BuyUpgrade()
    {
        TechUpgradesManager.Instance.ActivateUpgrade(techUpgrade);
        upgradeButton.image.color = Color.green;
        bought = true;
        upgradeButton.interactable = false;
        upgradeButton.onClick.RemoveAllListeners();
        TechTreeUI.Instance.RefreshUI();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipUI.Instance.SetTooltip(TechUpgrade.UpgradeDescription);
        TooltipUI.Instance.SetTooltipState(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipUI.Instance.SetTooltipState(false);
    }
}