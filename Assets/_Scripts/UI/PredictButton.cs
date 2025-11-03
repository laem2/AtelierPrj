using UnityEngine;
using UnityEngine.UI;

public class PredictButton : MonoBehaviour
{
    public GameObject predictionPanel; // To show results later

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnPredictClicked);
    }

    void OnPredictClicked()
    {
        predictionPanel.SetActive(true);
        PredictionManager.Instance.ShowPredictions();
        gameObject.SetActive(false); // hide after click if you want
    }

    public void EnableButton()
    {
        gameObject.SetActive(true);
    }
}
