using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class LauncherSceneTrigger : MonoBehaviour
{

    public int sceneIndexToLoad;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetMouseButtonDown(0))
        {
            Application.LoadLevel(sceneIndexToLoad);
        }
    }
    

}
