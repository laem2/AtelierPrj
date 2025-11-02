using UnityEngine;
using UnityEngine.UI;

public class WeightSlider : MonoBehaviour
{
    public string inputName; // e.g. "Input1"
    public Slider slider;

    void Start()
    {
        slider.minValue = -1f;
        slider.maxValue = 1f;
        slider.value = 0f;
        slider.onValueChanged.AddListener(UpdateWeight);
    }

    void UpdateWeight(float value)
    {
        Perceptron.Instance.SetWeight(inputName, value);
    }
}

