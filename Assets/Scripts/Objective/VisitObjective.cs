using UnityEngine;
using UnityEngine.UI;

public class VisitObjective : MonoBehaviour
{
    public bool visited = false;
    public float additionalTime = 20;
    public Image image;

    private void Start()
    {
        ObjectiveRegistry.AddObjective();
    }

    public float visit()
    {
        if (visited)
        {
            return 0;
        }

        visited = true;
        ObjectiveRegistry.CompleteObjective();
        image.color = new Color(0.22f, 0.8f, 0.18f, 0.66f);
        Destroy(gameObject, 0.1f);
        return additionalTime;
    }
}