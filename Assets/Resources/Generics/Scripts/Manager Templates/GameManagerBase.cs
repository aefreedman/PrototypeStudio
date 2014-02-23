using UnityEngine;

public abstract class GameManagerBase : MonoBehaviour
{

    public enum State { PreGame, InGame, GameOver, PostGame };
    public State gameState;
    public bool startDebug;
    public GameObject eventTextPrefab;
    public GameObject eventTextAnchor;
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
    }

    protected virtual void Update()
    {
        switch (gameState)
        {
            case State.PreGame:
                break;
            case State.InGame:
                break;
            case State.GameOver:
                break;
            case State.PostGame:
                break;
            default:
                break;
        }

    }

    public GameObject CreateEventMessage(string text, Color color, float time = 1.0f, float displace = 2.0f, int size = 500)
    {
        GameObject o = GameObject.Instantiate(eventTextPrefab, eventTextAnchor.transform.position, Quaternion.identity) as GameObject;
        o.GetComponent<SpringJoint>().connectedBody = eventTextAnchor.GetComponent<Rigidbody>();
        o.transform.Translate(Vector3.up * displace);
        EventText e = o.GetComponent<EventText>();
        e.SendText(text, color, time, size);
        return o;
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