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
        if (beats.Count == 0)
        {
            GetComponentInChildren<TweenPulse>().Pulse(Color.red, 0.5f);
            GameManagerBase.Instance.CreateEventMessage("Miss", Color.red, 0.15f, 0.1f, 200, 0, false);
            P6GameManager.Instance.combo = 0;
            P6GameManager.Instance.points -= P6GameManager.Instance.beatsPerMinute / 4 * P6GameManager.Instance.beatsPerMinute / 4;
            return -100;
        }
        else
        {
            for (int i = 0; i < beats.Count; i++)
            {
                p += beats[i].lifeAfterEntry;
                beats[i].Reset();
            }
            beats.Clear();
            points += p;

            if (p >= 90)
            {
                GameManagerBase.Instance.CreateEventMessage("Wonderful!", Color.yellow, 0.15f, 0.1f, 200, 0, false);
                GetComponentInChildren<TweenPulse>().Pulse(Color.yellow, 0.7f);
            }
            else if (p < 90 && p >= 70)
            {
                GameManagerBase.Instance.CreateEventMessage("Great!", Color.cyan, 0.15f, 0.1f, 200, 0, false);
                GetComponentInChildren<TweenPulse>().Pulse(Color.cyan, 0.7f);
            }
            else if (p < 70 && p >= 50)
            {
                GameManagerBase.Instance.CreateEventMessage("Good", Color.green, 0.15f, 0.1f, 200, 0, false);
                GetComponentInChildren<TweenPulse>().Pulse(Color.green, 0.7f);
            }
            else
            {
                GameManagerBase.Instance.CreateEventMessage("Poor", Color.red, 0.15f, 0.1f, 200, 0, false);
                GetComponentInChildren<TweenPulse>().Pulse(Color.red, 0.7f);
            }
            P6GameManager.Instance.combo++;
            return p;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("P6Beat"))
        {
            beats.Add(other.gameObject.GetComponent<P6Beat>());
        }
    }


}
