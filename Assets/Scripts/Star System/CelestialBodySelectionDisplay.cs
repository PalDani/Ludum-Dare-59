
using System;
using System.Linq;
using UnityEngine;

public class CelestialBodySelectionDisplay : MonoBehaviour
{
    private Interactable interactable;
    public Material outlineMaterial;
    public Material outlineMaterialReference;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnHoverEnter.AddListener(() => SetOutlineState(true));
        interactable.OnHoverExit.AddListener(() => SetOutlineState(false));
        InitMaterial();
    }

    private void InitMaterial()
    {
        var renderer = GetComponent<MeshRenderer>();
        foreach (var mat in renderer.materials)
        {
            if (mat == outlineMaterial)
            {
                outlineMaterialReference = mat;
            }
        }

        if (outlineMaterialReference == null)
        {
            var tmp = renderer.materials.ToList();
            tmp.Add(outlineMaterial);
            renderer.materials = tmp.ToArray();
            outlineMaterialReference = renderer.materials.Last();
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