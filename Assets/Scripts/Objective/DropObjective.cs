using System;
using UnityEngine;
using UnityEngine.UI;

public class DropObjective : MonoBehaviour
{
    public bool visited = false;
    public float additionalTime = 20;
    public Image image;


    private void Start()
    {
        ObjectiveRegistry.AddObjective();
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem)
        {
            particleSystem.Stop();
        }
    }

    public float drop(GameObject pickedUp)
    {
        if (visited)
        {
            return 0;
        }

        visited = true;
        ObjectiveRegistry.CompleteObjective();
        image.color = new Color(0.22f, 0.8f, 0.18f, 0.66f);
        pickedUp.transform.SetParent(transform);
        pickedUp.transform.localPosition = transform.up / 2;

        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem)
        {
            particleSystem.Play();
        }
        
        return additionalTime;
    }
}