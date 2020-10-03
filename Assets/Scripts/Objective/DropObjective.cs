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
    }

    public float drop(GameObject pickedUp)
    {
        if (visited)
        {
            return 0;
        }

        visited = true;
        ObjectiveRegistry.CompleteObjective();
        image.color = Color.grey;
        pickedUp.transform.SetParent(transform);
        pickedUp.transform.localPosition = transform.up / 2;
        return additionalTime;
    }
}