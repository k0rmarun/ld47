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
        image.color = new Color(0.22f, 0.8f, 0.18f, 0.66f);
    }
}