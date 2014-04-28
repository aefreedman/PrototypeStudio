using UnityEngine;

public class TriggerSceneSwitch : MonoBehaviour
{
    public int sceneToTrigger;

    private void OnTriggerEnter(Collider other)
    {
        Application.LoadLevel(sceneToTrigger);
    }
}