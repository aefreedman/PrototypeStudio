using UnityEngine;

public abstract class GameManagerBase : MonoBehaviour
{
    public enum State { PreGame, InGame, GameOver, PostGame };
    public State gameState;
    public bool startDebug;
    private GameObject eventTextPrefab;
    public GameObject[] eventTextAnchor;
    private static GameManagerBase instance;

    public static GameManagerBase Instance
    {
        get
        {
            if (instance == null)
            {
                Instance = FindObjectOfType<GameManagerBase>();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    protected virtual void Start()
    {
        try
        {
            eventTextPrefab = Resources.Load<GameObject>("generics/prefabs/eventtext");
        }
        catch (System.NullReferenceException)
        {
            Debug.LogWarning("Event Text Prefab not found.");
            throw;
        }

        eventTextAnchor = GameObject.FindGameObjectsWithTag("EventTextAnchor") as GameObject[];
        if (eventTextAnchor == null)
        {
            Debug.LogWarning("Event Text Anchor missing. Instantiating");
            eventTextAnchor[0] = GameObject.Instantiate(Resources.Load<GameObject>("generics/prefabs/eventtextanchor")) as GameObject;
        }

    }

    protected virtual void Update()
    {
    }

    public GameObject CreateEventMessage(string text, Color color, float time = 1.0f, float displace = 2.0f, int size = 500, int anchorNumber = 0)
    {
        GameObject o;
        try
        {
            o = GameObject.Instantiate(eventTextPrefab, eventTextAnchor[anchorNumber].transform.position, Quaternion.identity) as GameObject;
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("Event Text Anchor missing");
            throw;
        }
        o.transform.position = eventTextAnchor[anchorNumber].transform.position;
        o.GetComponent<SpringJoint>().connectedBody = eventTextAnchor[anchorNumber].GetComponent<Rigidbody>();
        o.transform.Translate(Vector3.up * displace);
        EventText e = o.GetComponent<EventText>();
        e.SendText(text, color, time, size);
        return o;
    }

    public GameObject CreateEventMessage(string text, Color color, int anchorNumber)
    {
        return CreateEventMessage(text, color, 1.0f, 2.0f, 500, anchorNumber);
    }

    public GameObject SpawnGameObject(GameObject objectToSpawn)
    {
        GameObject o = GameObject.Instantiate(objectToSpawn) as GameObject;
        return o;
    }

    public GameObject SpawnGameObject(GameObject objectToSpawn, Vector3 pos)
    {
        GameObject o = GameObject.Instantiate(objectToSpawn, pos, Quaternion.identity) as GameObject;
        return o;
    }

    /* Todo: Basic gameplay control commands
     * Restart, Debug on/off
     * 
     *
     */
}