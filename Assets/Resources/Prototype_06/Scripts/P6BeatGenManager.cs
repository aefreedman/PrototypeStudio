using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class P6BeatGenManager : MonoBehaviour
{

    public List<P6BeatGen> generator;
    private float lastBeat;
    public int beatsPerMinute;
    private float beatsPerSecond;
    private P6GameManager gm;

    private void Start()
    {
        generator.AddRange(GameObject.FindObjectsOfType<P6BeatGen>());
        lastBeat = Time.time;
        beatsPerSecond = beatsPerMinute / 60;
        gm = P6GameManager.Instance as P6GameManager;
    }

    private void FixedUpdate()
    {
        if (Time.time > lastBeat + (1 / beatsPerSecond))
        {
            generator[Random.Range(0, generator.Count)].GenerateBeat();
            lastBeat = Time.time;
            gm.PulseAll(Color.yellow, 1.1f);
        }
    }
}