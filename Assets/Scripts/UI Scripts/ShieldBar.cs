using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{

    public Slider slider;

    public Gradient gradient;

    public Image fill;
  
    public void SetMaxShields(float shield)
    {
        slider.maxValue = shield;
        slider.value = shield;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetShield(float shield)
    {
        slider.value = shield;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
