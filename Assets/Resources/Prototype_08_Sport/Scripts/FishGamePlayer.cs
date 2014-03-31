using UnityEngine;

public class FishGamePlayer : MonoBehaviour
{

    private bool started;
    
    private void Start()
    {
        started = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger") && !started)
        {
            DialogueController.Instance.SendMessage("TriggerDialogue");
            started = true;
        }
        if (other.CompareTag("ResetTrigger"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}