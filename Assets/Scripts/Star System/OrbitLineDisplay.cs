using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitLineDisplay : MonoBehaviour
{
    public int segments = 100;
    public float lineWidth = 0.1f;
    public bool isMoon = false;
    
    
    private LineRenderer lr;
    private CelestialBody body;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        body =  GetComponent<CelestialBody>();
    }

    void Start()
    {
        InitLineRenderer();
        DrawOrbit();
    }

    private void InitLineRenderer()
    {
        lr.endWidth = 0.1f;
        lr.startWidth = 0.1f;
    }

    private void Update()
    {
        if(isMoon)
            DrawOrbit();
    }

    void DrawOrbit()
    {
        lr.positionCount = segments + 1;

        Quaternion tilt = Quaternion.FromToRotation(Vector3.up, body.orbitAxis.normalized);

        for (int i = 0; i <= segments; i++)
        {
            float t = (float)i / segments;
            float angle = t * Mathf.PI * 2f;

            Vector3 localPos = new Vector3(
                Mathf.Cos(angle) * body.radiusX,
                0f,
                Mathf.Sin(angle) * body.radiusZ
            );

            Vector3 worldPos = body.orbitCenter.position + tilt * localPos;
            lr.SetPosition(i, worldPos);
        }
    }
}