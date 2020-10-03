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
        var cameraMovement = player.GetComponent<CameraMovement>();
        slider.value = Math.Max(0, cameraMovement.remainingTime);
        slider.maxValue = cameraMovement.maxTime;
    }
}
