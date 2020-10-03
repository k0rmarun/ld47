using UnityEngine;
using UnityEngine.UI;

public class PickupObjective : MonoBehaviour
{
    public Image image;

    private void Start()
    {
        ObjectiveRegistry.AddObjective();
    }

    public void Pickup()
    {
        ObjectiveRegistry.CompleteObjective();
        image.color = Color.grey;
    }
}