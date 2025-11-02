using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] public int placedCount = 0;
    public int totalInputs = 4;
    [HideInInspector] public bool allPlaced = false;

    [SerializeField] private LineConnector lineConnector; // assign in inspector

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // ensure line drawer is disabled on start
        if (lineConnector != null)
            lineConnector.enabled = false;
    }

    public void OnInputPlaced()
    {
        placedCount++;

        if (placedCount >= totalInputs && !allPlaced)
        {
            allPlaced = true;
            Debug.Log("All inputs placed! You can now connect them.");

            // Disable drag scripts
            Drag[] draggables = FindObjectsByType<Drag>(FindObjectsSortMode.None);
            foreach (var d in draggables)
                d.enabled = false;

            // Enable the line drawer
            if (lineConnector != null)
                lineConnector.enabled = true;
        }
    }
}

