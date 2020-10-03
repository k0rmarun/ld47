using UnityEngine;

public class TutorialStoneSpawner : MonoBehaviour
{
    public GameObject hiddenStones;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ObjectiveRegistry.RespawnCount > 0)
            {
                hiddenStones.SetActive(true);
            }
        }
    }
}