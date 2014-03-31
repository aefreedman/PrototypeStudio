using UnityEngine;

public class Money : MonoBehaviour
{

    public float lifetime;
    private float start;
    
    private void Start()
    {
        rigidbody.AddForce(new Vector3(Random.Range(-100, 100), 300, -20));
        start = Time.time;
    }

    private void Update()
    {
        if (Time.time > start + lifetime)
        {
            Destroy(gameObject);
        }
    }
}