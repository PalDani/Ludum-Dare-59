
using System;
using UnityEngine;

public class LookTowardsCamera : MonoBehaviour
{
    Transform cam;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(cam);
    }
}