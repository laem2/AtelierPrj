using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class LineConnector : MonoBehaviour
{
    public LayerMask connectableLayer; // assign in inspector
    public Material lineMaterial; // assign in inspector
    public float lineWidth = 0.1f;

    private Camera cam;
    private bool isDrawing = false;
    private Transform startPoint;
    private LineRenderer currentLine;

    private List<LineRenderer> lines = new List<LineRenderer>();

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

            // Create new line
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
        }
        else
        {
            Destroy(currentLine.gameObject);
        }

        currentLine = null;
        isDrawing = false;
    }
}



