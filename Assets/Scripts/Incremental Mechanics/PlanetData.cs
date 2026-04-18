using UnityEngine;

public class PlanetData : MonoBehaviour
{
    public float resources = 0;
    public int factories = 0;
    public int robots = 0;
    public int relays = 0;

    public OrbitalDataCenter dataCenter;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool HasDataCenter() => dataCenter != null;
}
