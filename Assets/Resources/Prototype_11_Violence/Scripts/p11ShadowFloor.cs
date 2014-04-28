using UnityEngine;

public class p11ShadowFloor : MonoBehaviour
{

    public GameObject normalFloor;
    public GameObject player;
    public float displacement;
    
    private void Start()
    {
    }

    private void Update()
    {
        Vector3 normalizedPlayerPos = player.transform.position.normalized;
        gameObject.transform.position = new Vector3(normalizedPlayerPos.x * displacement, transform.position.y, normalizedPlayerPos.z * displacement);
    }
}