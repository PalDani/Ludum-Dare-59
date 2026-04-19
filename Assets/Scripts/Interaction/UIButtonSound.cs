using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonSound : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Button button;

    private void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (button == null || AudioEffectPlayer.Instance == null)
            return;

        if (button.interactable)
            AudioEffectPlayer.Instance.PlayButtonSuccess();
        else
            AudioEffectPlayer.Instance.PlayButtonDenied();
    }
}