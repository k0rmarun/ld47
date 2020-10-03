﻿using UnityEngine;

public class VisitObjective : MonoBehaviour
{
    public bool visited = false;
    public float additionalTime = 20;

    public float visit()
    {
        if (visited)
        {
            return 0;
        }

        visited = true;
        Destroy(gameObject, 0.1f);
        return additionalTime;
    }
}