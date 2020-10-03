using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitObjective : MonoBehaviour
{
    public bool visited = false;

    public float additionalTime = 20;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

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