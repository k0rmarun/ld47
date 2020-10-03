using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject toFollow;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = toFollow.transform.position + toFollow.transform.TransformDirection(new Vector3(0, 1.3f, -5.2f));
        transform.LookAt(toFollow.transform);
    }
}
