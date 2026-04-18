using System;
using UnityEngine;

public class SharedResourcesManager : MonoBehaviour
{

    public float resources;
    
    public static SharedResourcesManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
