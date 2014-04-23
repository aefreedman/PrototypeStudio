using UnityEngine;

/// <summary>
/// Used to have 3D text auto-rotate to face player
/// </summary>
public class TextLookAtPlayer : MonoBehaviour
{

    private GameObject player;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.LookAt(player.transform.position);
        transform.Rotate(Vector3.up * 180);
    }
}