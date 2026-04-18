using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public Transform orbitCenter;
    public float orbitRadius = 5f;
    public float orbitSpeedDegPerSec = 20f;
    public float startAngleDeg = 0f;

    [Header("Optional ellipse")]
    public float radiusX = 5f;
    public float radiusZ = 5f;

    [Header("Optional tilt")]
    public Vector3 orbitAxis = Vector3.up;

    [Header("Spinning")]
    public float selfRotationSpeed = 50f; // degrees per second
    public Vector3 selfRotationAxis = Vector3.up;
    
    private float angleDeg;

    void Start()
    {
        angleDeg = startAngleDeg;
        if (radiusX <= 0f) radiusX = orbitRadius;
        if (radiusZ <= 0f) radiusZ = orbitRadius;
    }

    void Update()
    {
        if (orbitCenter == null) return;

        angleDeg += orbitSpeedDegPerSec * Time.deltaTime;
        float angleRad = angleDeg * Mathf.Deg2Rad;

        Vector3 localOffset = new Vector3(
            Mathf.Cos(angleRad) * radiusX,
            0f,
            Mathf.Sin(angleRad) * radiusZ
        );

        Quaternion tilt = Quaternion.FromToRotation(Vector3.up, orbitAxis.normalized);
        Vector3 worldOffset = tilt * localOffset;

        transform.position = orbitCenter.position + worldOffset;
        transform.Rotate(selfRotationAxis.normalized, selfRotationSpeed * Time.deltaTime, Space.Self);
    }
    
    public Vector3 GetPositionAtTime(float time)
    {
        float futureAngle = startAngleDeg + orbitSpeedDegPerSec * time;
        float angleRad = futureAngle * Mathf.Deg2Rad;
    
        Vector3 localOffset = new Vector3(
            Mathf.Cos(angleRad) * radiusX,
            0f,
            Mathf.Sin(angleRad) * radiusZ
        );
    
        Quaternion tilt = Quaternion.FromToRotation(Vector3.up, orbitAxis.normalized);
        return orbitCenter.position + tilt * localOffset;
    }
}