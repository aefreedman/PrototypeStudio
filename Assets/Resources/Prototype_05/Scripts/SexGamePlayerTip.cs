using UnityEngine;

public class SexGamePlayerTip : MonoBehaviour
{

    public SexGamePlayer player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SexGamePlayerTip"))
        {
            //player.Reset();
            SexBallGameManager gm = SexBallGameManager.Instance as SexBallGameManager;
            gm.CreateEventMessage("Watch the tip!", Color.red, 0);
            StartCoroutine(gm.PauseGame(3.0f));
        }
    }
}