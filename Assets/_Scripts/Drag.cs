using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Drag : MonoBehaviour
{
    private Vector3 startPosition;
    private bool isDragging = false;
    private Camera mainCam;
    private bool placed = false;

    void Start()
    {
        mainCam = Camera.main;
        startPosition = transform.position;
    }

    void Update()
    {
        // Disable drag if all placed
        if (GameManager.Instance.allPlaced || placed)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (IsMouseOverThis())
            {
                isDragging = true;
            }
        }

        if (isDragging)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldPos = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -mainCam.transform.position.z));
            worldPos.z = transform.position.z;
            transform.position = worldPos;

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                isDragging = false;
                TryPlace();
            }
        }
    }

    bool IsMouseOverThis()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = mainCam.ScreenPointToRay(mousePos);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        return hit.collider != null && hit.collider.gameObject == gameObject;
    }

    void TryPlace()
    {
        float snapRadius = 1.5f; // increase if needed (try 1 or 1.5)
        Collider2D[] nearby = Physics2D.OverlapCircleAll(transform.position, snapRadius);

        Transform closestTarget = null;
        float closestDist = Mathf.Infinity;

        foreach (var hit in nearby)
        {
            if (hit.CompareTag("drop"))
            {
                float dist = Vector2.Distance(transform.position, hit.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestTarget = hit.transform;
                }
            }
        }

        // If found a close enough target, snap to it
        if (closestTarget != null)
        {
            transform.position = closestTarget.position;
            placed = true;
            GameManager.Instance.OnInputPlaced();
        }
        else
        {
            transform.position = startPosition;
        }
    }

}

