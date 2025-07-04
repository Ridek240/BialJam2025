using UnityEngine;
using TMPro;

public class RoomEntryTrigger : MonoBehaviour
{
    public GameObject startText;
    public float displayTime = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            startText.SetActive(true);
            Invoke(nameof(HideText), displayTime);
        }
    }

    private void HideText()
    {
        startText.SetActive(false);
    }
}