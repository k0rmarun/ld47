using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underground : MonoBehaviour
{
    [Range(0.1f, 5f)]
    public float movementSpeed = 1.0f;

    public AudioClip walkingSound;
}
