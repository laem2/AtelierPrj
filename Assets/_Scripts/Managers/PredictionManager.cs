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

        foreach (var c in cities)
        {
            // Randomly choose predicted & real weather
            bool realIsSunny = Random.value > 0.5f;
            bool predictedIsSunny;

            // First 2 should be accurate
            if (accurateCount < 2)
            {
                predictedIsSunny = realIsSunny;
                accurateCount++;
            }
            else
            {
                predictedIsSunny = !realIsSunny;
            }

            // Assign sprites
            c.predictedIcon.sprite = predictedIsSunny ? sunnySprite : rainSprite;
            c.realIcon.sprite = realIsSunny ? sunnySprite : rainSprite;

            // Color city name based on correctness
            bool isCorrect = (predictedIsSunny == realIsSunny);
            c.cityNameText.color = isCorrect ? Color.green : Color.red;
        }
    }
}

