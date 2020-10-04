using UnityEngine;

public class Follower : MonoBehaviour
{
    public float offsetY = 2.5f;
    public GameObject toFollow;

    void Update()
    {
        offsetY = Mathf.Clamp(offsetY - 0.1f * Input.GetAxis("Mouse Y"), 2.5f, 5);

        Vector3 newPosition = toFollow.transform.position +
                              toFollow.transform.TransformDirection(new Vector3(0, offsetY, -7.2f));
        Vector3 delta = newPosition - toFollow.transform.position;
        RaycastHit raycastHit;
        Debug.DrawRay(toFollow.transform.position, delta);

        if (!Physics.Raycast(toFollow.transform.position, delta, out raycastHit, delta.magnitude, 1 << 8))
        {
            transform.position = newPosition;
            transform.LookAt(toFollow.transform);
        }
        else
        {
            transform.position = raycastHit.point;
            transform.LookAt(toFollow.transform);
        }
    }
}