using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public float maxTime = 60;
    public float velocityY = 0;
    public GameObject pickedUp;
    public float remainingTime = 30;
    public static int respawnCount = 0;

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

        velocityY -= 0.4f * Time.fixedDeltaTime;
        characterController.Move(new Vector3(0, velocityY, 0));

        if (characterController.isGrounded)
        {
            velocityY = 0;
            if (Input.GetKey("space"))
            {
                velocityY = 0.2f;
            }
        }
        GetComponent<Animator>().SetBool("Jumping", !characterController.isGrounded);
        GetComponent<Animator>().SetBool("Walking", transformedDirection.magnitude > 0);
        
        var deltaX = 10 * Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, deltaX);

        remainingTime -= Time.fixedDeltaTime;

        if (remainingTime < 0)
        {
            respawnCount++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

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
            remainingTime += visitObjective.visit();
        }

        PickupObjective pickupObjective = other.GetComponent<PickupObjective>();
        if (pickupObjective)
        {
            if (!pickedUp)
            {
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
                remainingTime += dropObjective.drop(pickedUp);
                pickedUp = null;
            }
        }

        if (remainingTime > maxTime)
        {
            remainingTime = maxTime;
        }
    }
}