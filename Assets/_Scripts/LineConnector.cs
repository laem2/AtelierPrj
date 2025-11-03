using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class LineConnector : MonoBehaviour
{
    public LayerMask connectableLayer;
    public Material lineMaterial;
    public float lineWidth = 0.1f;

    private Camera cam;
    private bool isDrawing = false;
    private Transform startPoint;
    private LineRenderer currentLine;

    private List<LineRenderer> lines = new List<LineRenderer>();

    [SerializeField] private int requiredInputConnections = 4;
    private int currentConnections = 0;
    private bool outputConnected = false;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            TryStartLine();

        if (isDrawing)
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos.z = 0;
            currentLine.SetPosition(1, mousePos);

            if (Mouse.current.leftButton.wasReleasedThisFrame)
                TryEndLine();
        }
    }

    void TryStartLine()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;

        Collider2D hit = Physics2D.OverlapCircle(mousePos, 0.3f, connectableLayer);
        if (hit)
        {
            startPoint = hit.transform;
            isDrawing = true;

            GameObject lineObj = new GameObject("ConnectionLine");
            LineRenderer lr = lineObj.AddComponent<LineRenderer>();
            lr.material = lineMaterial;
            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;
            lr.useWorldSpace = true;
            lr.positionCount = 2;
            lr.SetPosition(0, startPoint.position);
            lr.SetPosition(1, startPoint.position);
            lines.Add(lr);
            currentLine = lr;
        }
    }

    void TryEndLine()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;

        Collider2D hit = Physics2D.OverlapCircle(mousePos, 0.6f, connectableLayer);
        if (hit && hit.transform != startPoint)
        {
            currentLine.SetPosition(1, hit.transform.position);

            // Detect if connection is to output
            if (hit.CompareTag("output"))
            {
                if (!outputConnected)
                {
                    outputConnected = true;
                    Debug.Log("Output connected!");
                    GameManager.Instance.OnConnectionsComplete();
                }
            }
            else
            {
                currentConnections++;

                // Only trigger when all inputs are connected (but not output yet)
                if (currentConnections >= requiredInputConnections && !outputConnected)
                {
                    Debug.Log("All input connections complete!");
                }
            }
        }
        else
        {
            Destroy(currentLine.gameObject);
        }

        currentLine = null;
        isDrawing = false;
    }
}




