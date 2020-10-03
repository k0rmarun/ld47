using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainingTime : MonoBehaviour
{
    public GameObject player;
    void Update()
    {
        var slider = GetComponent<Slider>();
        slider.value = Math.Max(0, ObjectiveRegistry.RemainingTime);
        slider.maxValue = ObjectiveRegistry.MaxTime;
    }
}
