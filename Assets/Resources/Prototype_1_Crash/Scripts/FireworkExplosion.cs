using UnityEngine;

public class FireworkExplosion : MonoBehaviour
{

    private float lifetime = 1.0f;
    private float start;
    
    private void Start()
    {
        start = Time.time;
    }

    private void Update()
    {
        if (Time.time > lifetime + start)
        {
            Destroy(gameObject);
        }
    }
}