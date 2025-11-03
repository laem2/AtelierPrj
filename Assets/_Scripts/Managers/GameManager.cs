using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] public int placedCount = 0;
    public int totalInputs = 4;
    [HideInInspector] public bool allPlaced = false;

    [SerializeField] private LineConnector lineConnector;
    [SerializeField] private GameObject predictButton; // assign in Inspector

    private bool allConnected = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (lineConnector != null)
            lineConnector.enabled = false;

        if (predictButton != null)
            predictButton.SetActive(false);
    }

    public void OnInputPlaced()
    {
        placedCount++;

        if (placedCount >= totalInputs && !allPlaced)
        {
            allPlaced = true;
            Debug.Log("? All inputs placed! You can now connect them.");

            // Disable drag scripts
            Drag[] draggables = FindObjectsByType<Drag>(FindObjectsSortMode.None);
            foreach (var d in draggables)
                d.enabled = false;

            // Enable line connector
            if (lineConnector != null)
                lineConnector.enabled = true;
        }

        CheckIfReadyToPredict();
    }

    public void OnConnectionsComplete()
    {
        allConnected = true;
        Debug.Log(" All connections complete!");
        CheckIfReadyToPredict();
    }

    private void CheckIfReadyToPredict()
    {
        //  Only show Predict button when BOTH conditions are true
        if (allPlaced && allConnected)
        {
            Debug.Log("All ready! Showing Predict button...");
            predictButton.SetActive(true);

            Debug.Log($"Button assigned: {predictButton != null}");
            Debug.Log($"Button activeSelf: {predictButton.activeSelf}");
            Debug.Log($"Button activeInHierarchy: {predictButton.activeInHierarchy}");
        }

    }
}



