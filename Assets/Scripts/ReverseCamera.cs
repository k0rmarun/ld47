using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseCamera : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;

    private void Update()
    {
        transform.position = player.transform.position;
        transform.LookAt(camera.transform);
    }
}
