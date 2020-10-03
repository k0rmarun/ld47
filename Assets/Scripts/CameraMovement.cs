using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float velocityY = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit raycastHit;
        float floorModifier = 1;
        bool hit = Physics.Raycast(new Ray(transform.position, Vector3.down), out raycastHit, 3, 1 << 8);
        if (hit)
        {
            var underground = raycastHit.transform.GetComponent<Underground>();
            if (underground)
            {
                floorModifier = underground.movementSpeed;
            }
        }

        var transformedDirection = transform.rotation *
                                   new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transformedDirection = 0.1f * floorModifier * transformedDirection.normalized;

        Debug.DrawRay(transform.position, transformedDirection);
        var characterController = GetComponent<CharacterController>();
        characterController.Move(transformedDirection);

        velocityY -= 0.4f * Time.deltaTime;
        characterController.Move(new Vector3(0, velocityY, 0));

        if (characterController.isGrounded)
        {
            velocityY = 0;
            if (Input.GetKey("space"))
            {
                velocityY = 0.2f;
            }
        }

        var deltaX = 10 * Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, deltaX);
    }

    private void OnTriggerEnter(Collider other)
    {
        VisitObjective objective = other.GetComponent<VisitObjective>();
        if (objective)
        {
            objective.visit();
            Debug.Log("++++++++++++++++++");
        }
    }
}