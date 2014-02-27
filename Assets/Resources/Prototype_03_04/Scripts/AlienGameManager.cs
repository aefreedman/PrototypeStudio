using UnityEngine;
using System.Collections;

public class AlienGameManager : GameManagerBase
{

    public GameObject sun;
    public AlienPlanet[] planets;
    public string[] messages;
    public GameObject planetPrefab;
    public float[] planetHappy;
    private bool makeBabyPlanet;
    private float babyPlanetDelay = 3.0f;
    private float lastBabyPlanetTime;
    private float messageDelay = 25.0f;
    private float messageRandomness = 5.0f;
    private float nextMessage;
    private float lastMessage;

    protected override void Start()
    {
        base.Start();
        Physics.gravity = new Vector3(0, -3.0f, 0);
        if (AlienMenu.Instance)
        {
            AlienMenu.Instance.DisableAllMenuChoices();
        }
        if (startDebug)
        {
            AlienMenu.Instance.SetToStartConditions();
        }
        else
        {
            StartCoroutine(StartInstructions());
        }
        planetHappy = new float[planets.Length];
        lastBabyPlanetTime = Time.time;
        lastMessage = Time.time;
        nextMessage = lastMessage + messageDelay + Random.Range(-messageRandomness, messageRandomness);
    }

    protected override void Update()
    {
        base.Update();

        if (Time.time > nextMessage)
        {
            RandomMessage();
        }

        for (int i = 0; i < planets.Length; i++)
        {
            planetHappy[i] = 1000;
            foreach (var p2 in planets)
            {
                if (planets[i] != p2)
                {
                    planetHappy[i] = 1000.0f / Vector2.Distance(planets[i].transform.position, p2.transform.position); 
                }
            }
            if (planetHappy[i] > 1500.0f && makeBabyPlanet)
            {
                GameObject o = GameObject.Instantiate(planetPrefab) as GameObject;
                o.transform.parent = sun.transform;
                GameObject planet = o.GetComponentInChildren<AlienPlanet>().gameObject;
                planet.transform.position = new Vector3(Random.Range(2.0f, 6.0f), 0, 0);
                planet.renderer.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                o.GetComponent<AutoRotate>().rotation = new Vector3(0, 0, Random.Range(3.0f, 7.0f));
                lastBabyPlanetTime = Time.time;
                makeBabyPlanet = false;
                AlienAudioManager.Instance.PlayClipOneShot((int)AlienAudioManager.clips.pop);
            }
        }

        if (Time.time > lastBabyPlanetTime + babyPlanetDelay)
        {
            makeBabyPlanet = true;
        }

    }

    private IEnumerator StartInstructions()
    {
        int size = 300;
        float life = 5.0f;
        AlienGameManager.Instance.CreateEventMessage("Welcome!", Color.white, life, 2.0f, size * 2);
        yield return new WaitForSeconds(3);
        AlienGameManager.Instance.CreateEventMessage("This is Humanity Training Simulator 23052.a_3c", Color.white, life, 2.0f, size);
        yield return new WaitForSeconds(3);
        AlienGameManager.Instance.CreateEventMessage("The planets are sad", Color.white, life, 2.0f, size);
        yield return new WaitForSeconds(3);
        AlienGameManager.Instance.CreateEventMessage("Make them happy", Color.white, life, 2.0f, size);
        if (AlienMenu.Instance)
        {
            AlienMenu.Instance.SetToStartConditions();
        }
    }

    private void RandomMessage()
    {
        lastMessage = Time.time;
        nextMessage = lastMessage + messageDelay + Random.Range(-messageRandomness, messageRandomness);

        int max = messages.Length;
        int number = Random.Range(0, max);
        int size = 300;
        float life = 5.0f;
        AlienGameManager.Instance.CreateEventMessage(messages[number], Color.white, life, 2.0f, size * 2);
    }
}