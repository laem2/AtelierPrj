using UnityEngine;
using System.Collections.Generic;

public class Perceptron : MonoBehaviour
{
    public static Perceptron Instance;

    [System.Serializable]
    public class InputWeight
    {
        public string inputName;
        public float weight;
    }

    public List<InputWeight> inputs = new List<InputWeight>();

    void Awake()
    {
        Instance = this;
    }

    public void SetWeight(string name, float value)
    {
        foreach (var iw in inputs)
        {
            if (iw.inputName == name)
            {
                iw.weight = value;
                return;
            }
        }
    }
}

