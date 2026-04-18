using UnityEngine;

public class ShipTransfer : MonoBehaviour
{
    public Vector3 p0;
    public Vector3 p1;
    public Vector3 p2;
    public float duration = 5f;

    private float timer;
    private bool flying;

    public void Launch(Vector3 start, Vector3 control, Vector3 end, float travelDuration)
    {
        p0 = start;
        p1 = control;
        p2 = end;
        duration = travelDuration;
        timer = 0f;
        flying = true;
        transform.position = p0;
    }

    void Update()
    {
        if (!flying) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);

        Vector3 pos = Bezier(p0, p1, p2, t);
        transform.position = pos;

        float lookAheadT = Mathf.Clamp01((timer + 0.05f) / duration);
        Vector3 lookPos = Bezier(p0, p1, p2, lookAheadT);
        Vector3 dir = (lookPos - pos).normalized;
        if (dir.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(dir);

        if (t >= 1f)
            flying = false;
    }

    Vector3 Bezier(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        float u = 1f - t;
        return u * u * a + 2f * u * t * b + t * t * c;
    }
}