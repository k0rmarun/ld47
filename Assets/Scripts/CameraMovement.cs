using System.Linq;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float velocityY = 0;
    public GameObject pickedUp;
    public float groundedDuration = 0;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
 

        Underground underground = null;
        float floorDistance = 9999;
        var raycastHits = Physics.RaycastAll(new Ray(transform.position, Vector3.down), 3, 1 << 8);
        foreach (var hit in raycastHits)
        {
            if (hit.distance < floorDistance)
            {
                var _underground = hit.transform.GetComponent<Underground>();
                if (_underground)
                {
                    underground = _underground;
                    floorDistance = hit.distance;
                }
            }
        }

        float floorModifier = underground ? underground.movementSpeed : 1;
        AudioClip audioClip = underground ? underground.walkingSound : null;

        if (audioClip)
        {
            foreach (var audioSource in GetComponents<AudioSource>())
            {
                if (audioSource.outputAudioMixerGroup.name == "Steps")
                {
                    if (!audioSource.isPlaying)
                    {
                        audioSource.PlayOneShot(audioClip);
                    }
                }
            }
        }

        var transformedDirection = transform.rotation *
                                   new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transformedDirection = 0.1f * floorModifier * transformedDirection.normalized;

        Debug.DrawRay(transform.position, transformedDirection);
        var characterController = GetComponent<CharacterController>();
        characterController.Move(transformedDirection);

        velocityY -= 0.4f * Time.fixedDeltaTime;
        characterController.Move(new Vector3(0, velocityY, 0));

        if (characterController.isGrounded)
        {
            groundedDuration += Time.fixedDeltaTime;
            velocityY = 0;
            if (Input.GetKey("space") && groundedDuration > 0.5)
            {
                velocityY = 0.2f;
            }
        }
        else
        {
            groundedDuration = 0;
        }
        GetComponent<Animator>().SetBool("Jumping", !characterController.isGrounded);
        GetComponent<Animator>().SetBool("Walking", transformedDirection.magnitude > 0);
        
        var deltaX = 10 * Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, deltaX);

        if (pickedUp)
        {
            pickedUp.transform.localPosition = transform.rotation * -transform.forward;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        VisitObjective visitObjective = other.GetComponent<VisitObjective>();
        if (visitObjective)
        {
            ObjectiveRegistry.AddTime(visitObjective.visit());
        }

        PickupObjective pickupObjective = other.GetComponent<PickupObjective>();
        if (pickupObjective)
        {
            if (!pickedUp)
            {
                pickupObjective.Pickup();
                pickedUp = pickupObjective.gameObject;
                pickedUp.transform.SetParent(transform);
                pickedUp.transform.localPosition = transform.rotation * -transform.forward;
                Destroy(pickupObjective);
            }
        }

        DropObjective dropObjective = other.GetComponent<DropObjective>();
        if (dropObjective)
        {
            if (pickedUp)
            {
                ObjectiveRegistry.AddTime(dropObjective.drop(pickedUp));
                pickedUp = null;
            }
        }
    }
}