
using System;
using System.Linq;
using UnityEngine;

public class CelestialBodySelectionDisplay : MonoBehaviour
{
    [SerializeField] private MeshRenderer planetMeshRenderer;
    private Interactable interactable;
    public Material outlineMaterial;
    [SerializeField] private Material outlineMaterialReference;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnHoverEnter.AddListener(() => SetOutlineState(true));
        interactable.OnHoverExit.AddListener(() => SetOutlineState(false));
        InitMaterial();
    }

    private void InitMaterial()
    {
        if (planetMeshRenderer == null)
        {
            Debug.LogError("No mesh renderer for planet!", gameObject);
            return;
        }
        foreach (var mat in planetMeshRenderer.materials)
        {
            if (mat == outlineMaterial)
            {
                outlineMaterialReference = mat;
            }
        }

        if (outlineMaterialReference == null)
        {
            var tmp = planetMeshRenderer.materials.ToList();
            tmp.Add(outlineMaterial);
            planetMeshRenderer.materials = tmp.ToArray();
            outlineMaterialReference = planetMeshRenderer.materials.Last();
        }
        SetOutlineState(false);
    }

    private void SetOutlineState(bool state)
    {
        if(outlineMaterialReference)
            outlineMaterialReference.SetInt("_Outline_Enabled", state ? 1 : 0);
    }

    [ContextMenu("Disable")]
    private void SetOutlineTestD()
    {
        SetOutlineState(false);
    }
    
    [ContextMenu("Enable")]
    private void SetOutlineTestE()
    {
        SetOutlineState(true);
    }
}