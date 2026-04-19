using System;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(LineRenderer))]
public class OrbitLineDisplay : MonoBehaviour
{
    public int segments = 100;
    public float lineWidth = 0.1f;
    public bool isMoon = false;
    
    
    private LineRenderer lr;
    private CelestialBody body;
    private Transform camera;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        body =  GetComponent<CelestialBody>();
        camera = Camera.main.transform;
    }

    void Start()
    {
        UpdateLineRenderer();
        DrawOrbit();
    }

    private void UpdateLineRenderer(float width = 0.1f)
    {
        lr.endWidth = width;
        lr.startWidth = width;
    }

    private void Update()
    {
        if(isMoon)
            DrawOrbit();
        
        //UpdateLineWidth();
    }

    void DrawOrbit()
    {
        if(body.orbitCenter == null)
            return;
        
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

    private void UpdateLineWidth()
    {
        float distance = Vector3.Distance(transform.position, camera.position);
        float targetWidth = distance * 0.005f;
        targetWidth = Mathf.Clamp(targetWidth, 0.1f, 0.5f);
        UpdateLineRenderer(targetWidth);
    }

    public void SetLineState(bool state)
    {
        lr.enabled = state;
    }
}