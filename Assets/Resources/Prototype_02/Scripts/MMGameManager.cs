using UnityEngine;
using System.Collections.Generic;
using MMConstants;

public class MMGameManager : MonoBehaviour
{
    public enum State { playing, win, over, debug };
    public State gameState;

    public bool debugMode;
    public GameObject mainCamera;
    private List<MoneyRecepticle> moneyRecepticles;
    public float dollars;
    public TextMesh textMesh;
    public float maxDollars;
    public float RecepticlesHoldThisMuch;
    public GameObject moneyParticle;
    public GameObject moneyParticleSpawn;
    public float startingCash;
    public float recepticleCost;
    public GameObject recepticlePrefab;
    public GameObject moneyMakerPrefab;
    private MoneyMaker moneyMaker;
    public float moneyMakerCost;
    public float moneyMakerUpkeep;
    public float recepticleUpkeep;
    private float lastTax;
    public float taxPeriod;
    public float shuttleUpgradeCost;
    public float shuttleUpgradeCostMultiplier;
    public float shuttleSpeedAdd;
    public GameObject eventTextAnchor;
    public GameObject eventTextPrefab;
    public AudioClip scream;
    private float totalDollars;
    public float minimumFramesPerSecond;

    private static MMGameManager instance;

    public static MMGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Instance = FindObjectOfType<MMGameManager>();
            }
            return instance;
        }
        set
        {
            instance = value;
        }

    }

    private void Start()
    {
        lastTax = Time.time;
        moneyRecepticles = new List<MoneyRecepticle>();
        MoneyRecepticle[] r = FindObjectsOfType<MoneyRecepticle>();
        moneyRecepticles.AddRange(r);
        dollars += startingCash;
        Physics.gravity = new Vector3(0, -5.0f, 0);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        totalDollars = 0;
        SetDebugParameters();

        CreateEventMessage("Make a million bucks", Color.yellow, 10.0f, 3.0f);
        CreateEventMessage("Click stuff", Color.white, 10.0f, 2.0f);
    }

    private void SetDebugParameters()
    {
        if (debugMode)
        {
            maxDollars = 999999999999999.0f;
        }
    }

    private void Update()
    {
        if (debugMode)
        {
            if (dollars < startingCash)
            {
                dollars = startingCash;
            }
        }

        if (1.0f / Time.deltaTime < minimumFramesPerSecond)
        {
            ClearMoneyObjects();
        }

        switch (gameState)
        {
            case State.playing:
                int recepticles = 0;
                foreach (var m in moneyRecepticles)
                {
                    if (m != null)
                    {
                        recepticles++;
                    }
                }
                if (!debugMode)
                {
                    maxDollars = recepticles * RecepticlesHoldThisMuch;
                }

                if (moneyMaker != null || recepticles > 0)
                {
                    if (Time.time > lastTax + taxPeriod)
                    {
                        if (dollars > 0)
                        {
                            Taxes((recepticleUpkeep * recepticles * 1.1f) + moneyMakerUpkeep + (dollars * 0.05f));
                        }
                        else
                        {
                            Taxes((recepticleUpkeep * recepticles * 1.1f) + moneyMakerUpkeep);
                        }

                        lastTax = Time.time;
                    }
                }
                else
                {
                    lastTax = Time.time;
                }


                if (dollars >= 1000000.0f)
                {
                    gameState = State.win;
                }
                break;
            case State.win:
                CreateEventMessage("GREAT SUCCESS", Color.cyan, 120.0f, 4.0f);
                CreateEventMessage("You made $" + totalDollars.ToString("0,000.00"), Color.yellow, 120.0f, 3.0f);
                CreateEventMessage("in " + Time.time.ToString("0.00") + "s", Color.white, 120.0f, 2.0f);
                gameState = State.over;
                break;
            case State.over:
                break;
            default:
                break;
        }

        if (Mathf.Abs(dollars) <= 9.99f)
        {
            textMesh.text = "$" + dollars.ToString("0.00");
        }
        else if (Mathf.Abs(dollars) <= 99.99f)
        {
            textMesh.text = "$" + dollars.ToString("00.00");
        }
        else if (Mathf.Abs(dollars) <= 999.99f)
        {
            textMesh.text = "$" + dollars.ToString("000.00");
        }
        else if (Mathf.Abs(dollars) > 999.99f)
        {
            textMesh.text = "$" + dollars.ToString("0,000.00");
        }
        if (dollars < 0)
        {
            textMesh.color = Color.red;
        }
        else
        {
            textMesh.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.R) && (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)))
        {
            Application.LoadLevel(0);
        }

    }

    public bool MakeDollars(float amount)
    {
        if (dollars + amount <= maxDollars)
        {
            dollars += Mathf.Abs(amount);
            GameObject g = GameObject.Instantiate(moneyParticle, moneyParticleSpawn.transform.position, Quaternion.identity) as GameObject;
            MoneyParticle p = g.GetComponent<MoneyParticle>();
            p.SetText("+$" + amount.ToString("0.00"), Color.green);
            totalDollars += amount;
            return true;
        }
        else
        {
            float diff = maxDollars - dollars;
            float extraMoney = amount - diff;
            int neededRecepticles = Mathf.CeilToInt(extraMoney / RecepticlesHoldThisMuch);
            NeedMoreRecepticles(neededRecepticles);

            if (dollars < maxDollars)
            {
                dollars = maxDollars;
            }
            return false;
        }


    }

    public bool SpendDollars(float amount)
    {
        if (amount <= dollars)
        {
            dollars -= Mathf.Abs(amount);
            GameObject g = GameObject.Instantiate(moneyParticle, moneyParticleSpawn.transform.position, Quaternion.identity) as GameObject;
            MoneyParticle p = g.GetComponent<MoneyParticle>();
            p.SetText("-$" + amount.ToString("0.00"), Color.red);
            return true;
        }
        else
        {
            NeedMoreMoney(amount - dollars);
            return false;
        }
    }

    public bool CheckDollars(float amount)
    {
        if (amount < dollars)
        {
            return true;
        }
        else return false;
    }

    public void NeedMoreMoney(float amount)
    {
        GameObject g = GameObject.Instantiate(moneyParticle, moneyParticleSpawn.transform.position, Quaternion.identity) as GameObject;
        MoneyParticle p = g.GetComponent<MoneyParticle>();
        p.SetText("Need more money! " + "($" + amount.ToString("0.00") + ")", Color.red);
    }

    public void NeedMoreRecepticles(int amount)
    {
        GameObject g = GameObject.Instantiate(moneyParticle, moneyParticleSpawn.transform.position, Quaternion.identity) as GameObject;
        MoneyParticle p = g.GetComponent<MoneyParticle>();
        p.SetText("Need more bags! (" + amount.ToString() + ")", Color.red);
    }

    public void SpawnItem(Item item)
    {
        switch (item)
        {
            case Item.Recepticle:

                if (SpendDollars(recepticleCost))
                {
                    SpawnRecepticle();
                }
                break;
            case Item.ShuttleSpeed:
                if (SpendDollars(shuttleUpgradeCost))
                {
                    IncreaseShuttleSpeed();
                }
                break;
            case Item.MoneyMaker:
                if (moneyMaker == null)
                {
                    if (SpendDollars(moneyMakerCost))
                    {
                        SpawnMoneyMaker();
                    }
                }
                break;
            case Item.Premium:
                //eventText.SendText("Premium Version Only!", new Color(1f, 0.5f, 0));
                CreateEventMessage("Premium Version Only!", new Color(1f, 0.5f, 0));
                break;
            case Item.GiveUp:
                Application.LoadLevel(0);
                break;
            default:
                break;
        }
    }

    private void SpawnRecepticle()
    {
        GameObject o = GameObject.Instantiate(recepticlePrefab, new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0), Quaternion.identity) as GameObject;
        recepticleCost *= 1.0f + (0.001f * moneyRecepticles.Count); // TODO extract this multiplier into a variable
        MoneyRecepticle r = o.GetComponent<MoneyRecepticle>();
        moneyRecepticles.Add(r);
        CreateEventMessage("Hold dat !@&#", new Color(0.75f, 1.0f, 0.75f), 2.0f);
    }

    private void SpawnMoneyMaker()
    {
        GameObject o = GameObject.Instantiate(moneyMakerPrefab) as GameObject;
        MoneyMaker m = o.GetComponent<MoneyMaker>();
        moneyMaker = m;
        //eventText.SendText("Make that money!", Color.yellow, 2.0f);
        CreateEventMessage("Make that money!", Color.yellow, 2.0f);
        CreateEventMessage("Click on it", Color.gray, 2.0f, 1.0f);
    }

    private void IncreaseShuttleSpeed()
    {
        moneyMaker.speed += shuttleSpeedAdd;
        shuttleUpgradeCost *= shuttleUpgradeCostMultiplier;
        CreateEventMessage("Shake it to make it", Color.white, 2.0f);
    }

    private void Taxes(float amount)
    {
        dollars -= Mathf.Abs(amount);
        GameObject g = GameObject.Instantiate(moneyParticle, moneyParticleSpawn.transform.position, Quaternion.identity) as GameObject;
        MoneyParticle p = g.GetComponent<MoneyParticle>();
        p.SetText("-$" + amount.ToString("0.00") + " (Taxes)", Color.red);
        CreateEventMessage("TAXES", Color.red, 1.0f);
        audio.PlayOneShot(scream);
        mainCamera.GetComponent<ScreenShake>().StartShake(0.1f, 0.5f);
    }

    public GameObject CreateEventMessage(string text, Color color, float time = 1.0f, float displace = 2.0f)
    {
        GameObject o = GameObject.Instantiate(eventTextPrefab) as GameObject;
        o.GetComponent<SpringJoint>().connectedBody = eventTextAnchor.GetComponent<Rigidbody>();
        o.transform.Translate(Vector3.up * displace);
        EventText e = o.GetComponent<EventText>();
        e.SendText(text, color, time);
        return o;
    }

    private void ClearMoneyObjects()
    {
        GameObject[] money = GameObject.FindGameObjectsWithTag("FlagForDelete");
        foreach (var item in money)
        {
            Destroy(item);
        }
    }
}