using TMPro;
using UnityEngine;

public class RenderTutorialText : MonoBehaviour
{
    public TMP_Text textBoxFirstTime;
    public TMP_Text textBoxMoreTimes;
    public float timeRemaining;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var textBox = getTextBox();
            textBox.gameObject.transform.parent.gameObject.SetActive(true);
            textBox.gameObject.SetActive(true);
            timeRemaining = other.GetComponent<CameraMovement>().remainingTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CameraMovement>().remainingTime = timeRemaining;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var textBox = getTextBox();
            textBox.gameObject.transform.parent.gameObject.SetActive(false);
            textBox.gameObject.SetActive(false);
        }
    }

    private TMP_Text getTextBox()
    {
        if (CameraMovement.respawnCount == 0)
        {
            return textBoxFirstTime;
        }

        return textBoxMoreTimes;
    }
}