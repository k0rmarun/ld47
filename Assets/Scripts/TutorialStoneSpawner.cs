using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStoneSpawner : MonoBehaviour
{
    public GameObject hiddenStones;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CameraMovement.respawnCount > 0)
            {
                hiddenStones.SetActive(true);
            }
        }
    }
}