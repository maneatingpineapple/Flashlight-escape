using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxBattery(float bat)
    {
        slider.maxValue = bat;
        slider.value = bat;
    }
    
    public void SetBattery(float bat)
    {
        slider.value = bat;
    }

    public void DecreaseBattery(float incr)
    {
        slider.value -= incr;
    }

    public void IncreaseBattery(float incr)
    {
        slider.value += incr;
    }
}
