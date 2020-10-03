using UnityEngine;

public class DropObjective : MonoBehaviour
{
    public bool visited = false;
    public float additionalTime = 20;

    public float drop(GameObject pickedUp)
    {
        if (visited)
        {
            return 0;
        }

        visited = true;
        pickedUp.transform.SetParent(transform);
        pickedUp.transform.localPosition = transform.up / 2;
        // Destroy(pickedUp, 0.1f);
        return additionalTime;
    }
}