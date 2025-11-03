using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PredictionManager : MonoBehaviour
{
    public static PredictionManager Instance;

    [System.Serializable]
    public class CityPrediction
    {
        public string cityName;
        public TMP_Text cityNameText;       // UI Text for city name
        public Image predictedIcon;     // Image showing predicted weather
        public Image realIcon;          // Image showing actual weather
    }

    public CityPrediction[] cities;
    public Sprite rainSprite;
    public Sprite sunnySprite;

    void Awake()
    {
        Instance = this;
    }

    public void ShowPredictions()
{
    int accurateCount = 0;

        // ?? Hide all connection lines
        foreach (var line in FindObjectsByType<LineRenderer>(FindObjectsSortMode.None))
            line.enabled = false;

    // ?? Continue with your prediction logic
    foreach (var c in cities)
    {
        bool realIsSunny = Random.value > 0.5f;
        bool predictedIsSunny;

        if (accurateCount < 2)
        {
            predictedIsSunny = realIsSunny;
            accurateCount++;
        }
        else
        {
            predictedIsSunny = !realIsSunny;
        }

        c.predictedIcon.sprite = predictedIsSunny ? sunnySprite : rainSprite;
        c.realIcon.sprite = realIsSunny ? sunnySprite : rainSprite;

        bool isCorrect = (predictedIsSunny == realIsSunny);
        c.cityNameText.color = isCorrect ? Color.green : Color.red;
    }
}

}

