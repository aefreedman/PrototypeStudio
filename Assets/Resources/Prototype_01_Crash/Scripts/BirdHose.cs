﻿using UnityEngine;

public class BirdHose : MonoBehaviour
{

    public GameObject birdPrefab;
    public float birdSpawnDelay;
    public Bird.Direction direction;
    public float force;

    private void Start()
    {
    }

    private void Update()
    {
        transform.position = new Vector2(transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        if (Input.GetMouseButtonDown(0))
        {
            CrashGameManager gm = CrashGameManager.Instance();
            if (gm.birds > 0)
            {
                GameObject o = Instantiate(birdPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
                Bird b = o.GetComponent<Bird>();
                b.SetDirection(direction);
                b.SetForce(force);
                gm.UseBird();
            }


        }


    }


}