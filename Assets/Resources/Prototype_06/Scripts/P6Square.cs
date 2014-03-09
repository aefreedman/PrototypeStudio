using UnityEngine;
using System.Collections.Generic;

public class P6Square : MonoBehaviour
{

    public List<P6Beat> beats;
    public float points;

    void Start()
    {
        beats = new List<P6Beat>();
    }

    void FixedUpdate()
    {

    }

    public int Press()
    {
        int p = 0;
        for (int i = 0; i < beats.Count; i++)
        {
            p += beats[i].lifeAfterEntry;
            beats[i].Reset();
        }
        beats.Clear();
        points += p;

        if (p >= 90)
        {
            GameManagerBase.Instance.CreateEventMessage("Amazing!", Color.yellow, false);
            GetComponentInChildren<TweenPulse>().Pulse(Color.yellow, 0.9f);
        }
        else
        {
            GetComponentInChildren<TweenPulse>().Pulse(Color.white, 0.9f);
        }

        return p;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("P6Beat"))
        {
            beats.Add(other.gameObject.GetComponent<P6Beat>());
        }
    }


}
