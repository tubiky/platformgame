using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    public void SetMaxValue(float maxValue)
    {
        slider.maxValue = maxValue;
    }

    public void SetCurrentValue(float currentValue)
    {
        slider.value = currentValue;
    }
}
