using UnityEngine;
using System.Collections;

public class P6Beat : MonoBehaviour
{

    public Vector3 target;
    public P6Square pad;
    public int lifeAfterEntry = 0;
    public int lifeAfterEntryInit = 100;
    private bool entry;

    void Start()
    {
        Reset();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("P6Pad"))
        {
            entry = true;
            pad = other.gameObject.GetComponent<P6Square>();
            //other.gameObject.GetComponent<P6Square>().beats.Add(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
    }
    
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target, 1.0f * Time.deltaTime);
        if (entry)
        {
            lifeAfterEntry -= 1;
            if (lifeAfterEntry <= 0)
            {
                Reset();
            }
        }
    }

    public void Reset()
    {
        entry = false;
        if (pad != null)
        {
            pad.beats.Remove(this);
        }
        pad = null;
        lifeAfterEntry = lifeAfterEntryInit;
        ObjectPool.Recycle(this);
    }
}
