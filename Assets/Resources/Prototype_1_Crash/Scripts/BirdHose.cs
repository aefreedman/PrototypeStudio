using UnityEngine;

public class BirdHose : MonoBehaviour
{

    public GameObject birdPrefab;
    public float birdSpawnDelay;
    private float lastSpawn;
    public Bird.Direction direction;
    public float force;
    public Camera camera;

    private void Start()
    {
        lastSpawn = Time.time;
    }

    private void Update()
    {
        transform.position = new Vector2(transform.position.x, camera.ScreenToWorldPoint(Input.mousePosition).y);

        if (Input.GetMouseButtonDown(0))
        {
            //if (Time.time - lastSpawn > birdSpawnDelay)
            //{
                GameObject o = Instantiate(birdPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
                Bird b = o.GetComponent<Bird>();
                b.SetDirection(direction);
                b.SetForce(force);
                lastSpawn = Time.time;
                CrashGameManager.Instance().UseBird();
            //}
        }


    }


}