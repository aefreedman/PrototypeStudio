using UnityEngine;

public class BirdSpawner : MonoBehaviour
{

    public GameObject birdPrefab;
    public float birdSpawnDelay;
    private float lastSpawn;
    public Bird.Direction direction;

    private void Start()
    {
        lastSpawn = Time.time;
    }

    private void Update()
    {
        if (Time.time - lastSpawn > birdSpawnDelay)
        {
            if (CrashGameManager.Instance().birds > 0)
            {
                GameObject o = Instantiate(birdPrefab, new Vector3(transform.position.x, transform.position.y + Random.Range(-4.0f, 4.0f), transform.position.z), Quaternion.identity) as GameObject;
                Bird b = o.GetComponent<Bird>();
                b.SetDirection(direction);
                CrashGameManager.Instance().UseBird();
                lastSpawn = Time.time;
            }

        }
    }
}