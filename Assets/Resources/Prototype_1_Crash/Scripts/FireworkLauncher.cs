using UnityEngine;

public class FireworkLauncher : MonoBehaviour
{

    public GameObject[] fireworkPrefabs;
    public float launchDelay;
    public float angleRange;
    private float lastLaunch;
    public float launchRandomness;
    
    private void Start()
    {
        lastLaunch = Time.time;
    }

    private void Update()
    {
        if (Time.time > lastLaunch + launchDelay)
        {
            Instantiate(fireworkPrefabs[Random.Range(0, fireworkPrefabs.Length)], transform.position, Quaternion.identity);
            lastLaunch = Time.time + Random.Range(-launchRandomness, launchRandomness);
        }
    }
}