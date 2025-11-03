using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PredictButton : MonoBehaviour
{
    public GameObject mainPanel;         // assign your main content parent here
    public GameObject predictionPanel;   // the panel that shows predictions
    public CanvasGroup predictionGroup;  // for fade animation

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnPredictClicked);

        if (predictionPanel != null)
            predictionPanel.SetActive(false);
    }

    void OnPredictClicked()
    {
        // Hide main network
        if (mainPanel != null)
            mainPanel.SetActive(false);

        // Show prediction panel
        if (predictionPanel != null)
        {
            predictionPanel.SetActive(true);
            StartCoroutine(FadeInPrediction());
        }

        // Trigger predictions
        PredictionManager.Instance.ShowPredictions();
    }

    IEnumerator FadeInPrediction()
    {
        if (predictionGroup == null)
            yield break;

        predictionGroup.alpha = 0f;
        while (predictionGroup.alpha < 1f)
        {
            predictionGroup.alpha += Time.deltaTime * 2f; // fade in speed
            yield return null;
        }
    }

    public void EnableButton()
    {
        gameObject.SetActive(true);
    }
}

