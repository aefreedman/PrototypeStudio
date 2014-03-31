using UnityEngine;

public abstract class GameManagerBase : MonoBehaviour
{
    public enum State { PreGame, InGame, GameOver, PostGame };
    public State gameState;
    public bool startDebug;
    public bool canReset;
    private GameObject eventTextPrefab;
    public GameObject[] eventTextAnchor;
    public bool manualSetupTextAnchors;
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

        if (!manualSetupTextAnchors)
        {
            eventTextAnchor = GameObject.FindGameObjectsWithTag("EventTextAnchor") as GameObject[];
            if (eventTextAnchor == null)
            {
                Debug.LogWarning("Event Text Anchor missing. Instantiating");
                eventTextAnchor[0] = GameObject.Instantiate(Resources.Load<GameObject>("generics/prefabs/eventtextanchor")) as GameObject;
            }
        }
    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {
        if (canReset)
        {
            if (Input.GetKeyDown(KeyCode.R) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    /// <summary>
    /// Deprecated
    /// </summary>
    /// <param name="text"></param>
    /// <param name="color"></param>
    /// <param name="time"></param>
    /// <param name="displace"></param>
    /// <param name="size"></param>
    /// <param name="anchorNumber"></param>
    /// <param name="collideWithOtherText"></param>
    /// <returns></returns>
    public GameObject CreateEventMessage(string text, Color color, float time = 1.0f, float displace = 2.0f, int size = 500, int anchorNumber = 0, bool collideWithOtherText = true)
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
        if (!collideWithOtherText)
        {
            o.collider.isTrigger = true;
        }
        o.GetComponent<SpringJoint>().connectedBody = eventTextAnchor[anchorNumber].GetComponent<Rigidbody>();
        o.transform.Translate(Vector3.up * displace);
        o.transform.rotation = eventTextAnchor[anchorNumber].transform.rotation;
        EventText e = o.GetComponent<EventText>();
        e.SendText(text, color, time, size);
        return o;
    }
    

    public GameObject CreateEventMessage(string text, Color color, Vector3 displace, float time = 1.0f, int size = 500, int anchorNumber = 0, bool collideWithOtherText = true)
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
        if (!collideWithOtherText)
        {
            o.collider.isTrigger = true;
        }
        o.GetComponent<SpringJoint>().connectedBody = eventTextAnchor[anchorNumber].GetComponent<Rigidbody>();
        o.transform.Translate(displace);
        o.transform.rotation = eventTextAnchor[anchorNumber].transform.rotation;
        EventText e = o.GetComponent<EventText>();
        e.SendText(text, color, time, size);
        return o;
    }

    public GameObject CreateEventMessage(string text, Color color, int anchorNumber, bool collideWithOtherText)
    {
        return CreateEventMessage(text, color, new Vector3(0, 2.0f, 0), 1.0f, 500, anchorNumber, collideWithOtherText);
    }

    public GameObject CreateEventMessage(string text, Color color, bool collideWithOtherText)
    {
        return CreateEventMessage(text, color, new Vector3(0, 2.0f, 0), 1.0f, 500, 0, collideWithOtherText);
    }

    public GameObject CreateEventMessage(string text, Color color, int anchorNumber)
    {
        return CreateEventMessage(text, color, Vector3.zero, 1.0f, 500, anchorNumber);
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

    public State SwitchGameState(State newState)
    {
        gameState = newState;
        return gameState;
    }

    /* Todo: Basic gameplay control commands
     * Restart, Debug on/off
     * 
     *
     */
}