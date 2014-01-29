using UnityEngine;
using System.Collections;

public class Firework : MonoBehaviour
{

    public float launchForce;
    public float fuseTime;
    private float launchTime;
    public float launchAngleRandomness;
    bool exploded;
    public float fuseRandomness;
    public GameObject explosionCollider;

    void Start()
    {
        rigidbody2D.AddForce(new Vector2(Random.Range(-launchAngleRandomness, launchAngleRandomness), launchForce));
        particleSystem.Stop();
        particleSystem.playOnAwake = false;
        launchTime = Time.time;
        exploded = false;
        fuseTime += Random.Range(-fuseRandomness, fuseRandomness);
        collider2D.enabled = false;
    }

    void Update()
    {
        if (!exploded)
        {
            if (Time.time > launchTime + fuseTime)
            {
                particleSystem.Emit(Random.Range(350, 450));
                Instantiate(explosionCollider, transform.position, Quaternion.identity);
                audio.PlayOneShot(audio.clip);
                renderer.enabled = false;
                collider2D.enabled = true;
                GetComponent<TrailRenderer>().enabled = false;
                exploded = true;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.gameObject.name == "Ground")
        {
            DestroyObject(gameObject);
        }
        if (coll.collider.gameObject.name == "Edge")
        {
            DestroyObject(gameObject);
        }
    }


}
