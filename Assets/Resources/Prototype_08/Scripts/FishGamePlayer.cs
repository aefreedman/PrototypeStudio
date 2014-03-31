using UnityEngine;

public class FishGamePlayer : MonoBehaviour
{

    private FishingGameManager gm;
    private bool started;
    
    private void Start()
    {
        gm = (FishingGameManager)GameManagerBase.Instance;
        started = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger") && !started)
        {
            gm.SendMessage("TriggerDialogue");
            started = true;
        }
        if (other.CompareTag("ResetTrigger"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}