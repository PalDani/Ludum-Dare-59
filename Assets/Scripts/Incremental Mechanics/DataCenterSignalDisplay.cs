using System.Collections.Generic;
using UnityEngine;

public class DataCenterSignalDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SignalManager signalManager;
    [SerializeField] private Material lineMaterial;

    [Header("Visuals")]
    [SerializeField] private float lineWidth = 0.1f;
    [SerializeField] private int sortingOrder = 0;
    [SerializeField] private bool updateEveryFrame = true;

    private readonly List<LineRenderer> activeLines = new();
    private readonly List<LineRenderer> linePool = new();

    public static DataCenterSignalDisplay Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RebuildLines();
    }

    private void Update()
    {
        if (updateEveryFrame)
            RebuildLines();
    }

    public void RebuildLines()
    {
        ClearActiveLines();

        if (signalManager == null || signalManager.orbitalDataCenters == null)
            return;

        List<OrbitalDataCenter> centers = signalManager.orbitalDataCenters;

        for (int i = 0; i < centers.Count; i++)
        {
            if (centers[i] == null) 
                continue;

            for (int j = i + 1; j < centers.Count; j++)
            {
                if (centers[j] == null) 
                    continue;

                CreateOrUpdateLine(centers[i].transform.position, centers[j].transform.position);
            }
        }
    }

    private void CreateOrUpdateLine(Vector3 start, Vector3 end)
    {
        LineRenderer lr = GetLineFromPool();

        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        activeLines.Add(lr);
    }

    private LineRenderer GetLineFromPool()
    {
        foreach (LineRenderer lr in linePool)
        {
            if (!lr.gameObject.activeSelf)
            {
                lr.gameObject.SetActive(true);
                return lr;
            }
        }

        GameObject lineObject = new GameObject("DataCenterConnection");
        lineObject.transform.SetParent(transform);

        LineRenderer newLine = lineObject.AddComponent<LineRenderer>();
        newLine.useWorldSpace = true;
        newLine.material = lineMaterial;
        newLine.startWidth = lineWidth;
        newLine.endWidth = lineWidth;
        newLine.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        newLine.receiveShadows = false;
        newLine.textureMode = LineTextureMode.Stretch;
        newLine.alignment = LineAlignment.View;
        newLine.sortingOrder = sortingOrder;

        linePool.Add(newLine);
        return newLine;
    }

    private void ClearActiveLines()
    {
        for (int i = 0; i < activeLines.Count; i++)
        {
            if (activeLines[i] != null)
                activeLines[i].gameObject.SetActive(false);
        }

        activeLines.Clear();
    }
}