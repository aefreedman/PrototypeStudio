using UnityEngine;
using System.Collections.Generic;
using MMConstants;

public class MMGameManager : MonoBehaviour
{
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
    private float lastUpkeep;
    public float upkeepSeconds;
    public float shuttleUpgradeCost;
    public float shuttleUpgradeCostMultiplier;
    public float shuttleSpeedAdd;

    public static MMGameManager instance;

    public static MMGameManager Instance()
    {
        return GameObject.FindObjectOfType<MMGameManager>();
    }

    private void Start()
    {
        lastUpkeep = Time.time;
        moneyRecepticles = new List<MoneyRecepticle>();
        MoneyRecepticle[] r = FindObjectsOfType<MoneyRecepticle>();
        moneyRecepticles.AddRange(r);
        instance = MMGameManager.Instance();
        dollars += startingCash;
        Physics.gravity = new Vector3(0, -5.0f, 0);

        //int recepticlesToSpawn = Mathf.CeilToInt(dollars / RecepticlesHoldThisMuch);
        //for (int i = 0; i < recepticlesToSpawn; i++)
        //{
        //    SpawnRecepticle();
        //}
    }

    private void Update()
    {
        int recepticles = 0;
        foreach (var m in moneyRecepticles)
        {
            if (m != null)
            {
                recepticles++;
            }
        }
        maxDollars = recepticles * RecepticlesHoldThisMuch;

        if (moneyMaker != null || recepticles > 0)
        {
            if (Time.time > lastUpkeep + upkeepSeconds)
            {
                Upkeep((recepticleUpkeep * recepticles * 1.1f) + moneyMakerUpkeep);
                lastUpkeep = Time.time;
            }
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
        p.SetText("Need more recepticles! (" + amount.ToString() + ")", Color.red);
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
            default:
                break;
        }
    }

    private void SpawnRecepticle()
    {
        GameObject o = GameObject.Instantiate(recepticlePrefab) as GameObject;
        recepticleCost *= 1.0f + (0.01f * moneyRecepticles.Count);
        MoneyRecepticle r = o.GetComponent<MoneyRecepticle>();
        moneyRecepticles.Add(r);
    }

    private void SpawnMoneyMaker()
    {
        GameObject o = GameObject.Instantiate(moneyMakerPrefab) as GameObject;
        MoneyMaker m = o.GetComponent<MoneyMaker>();
        moneyMaker = m;
    }

    private void IncreaseShuttleSpeed()
    {
        moneyMaker.speed += shuttleSpeedAdd;
        shuttleUpgradeCost *= shuttleUpgradeCostMultiplier;
    }

    private void Upkeep(float amount)
    {
        dollars -= Mathf.Abs(amount);
        GameObject g = GameObject.Instantiate(moneyParticle, moneyParticleSpawn.transform.position, Quaternion.identity) as GameObject;
        MoneyParticle p = g.GetComponent<MoneyParticle>();
        p.SetText("-$" + amount.ToString("0.00") + " (Upkeep)", Color.red);
    }
}