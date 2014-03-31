using UnityEngine;
using System.Collections;

public class BPMFinder : MonoBehaviour
{

    public AudioClip song;
    public float bpm;
    private ArrayList previousSamples;
    public float averageBpm;
    public int sampleSize;
    public int beats;
    private bool sampling;
    private float lastSample;
    private int samples;

    private void Start()
    {
        sampling = false;
        bpm = 0;
        audio.clip = song;
        audio.PlayDelayed(3.0f);
        averageBpm = 0;
        previousSamples = new ArrayList();
    }

    private void Update()
    {
        if (sampling)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.L))
            {
                beats++;
            }
            if (Time.time > lastSample + sampleSize)
            {
                previousSamples.Add(bpm);
                bpm = beats / (sampleSize / 60.0f);
                samples++;
                beats = 0;
                float sum = 0.0f;
                for (int i = 0; i < previousSamples.Count; i++)
                {
                    sum += (float)previousSamples[i];
                }
                averageBpm = sum / samples;
                lastSample = Time.time;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.L))
            {
                sampling = true;
                beats++;
                lastSample = Time.time;
            }
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 100), bpm.ToString() + " " + averageBpm.ToString());
    }
}

