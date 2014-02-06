using UnityEngine;
using System.Collections.Generic;
using MMConstants;

public class MMGameManager : MonoBehaviour
{
    public List<MoneyRecepticle> moneyRecepticles;
    public float dollars;
    public TextMesh textMesh;
    public float maxDollars;
    public float dollarsPerRecepticle;
    public GameObject moneyParticle;
    public GameObject moneyParticleSpawn;
    public float startingCash;
    public float recepticleCost;
    public GameObject recepticlePrefab;

    public static MMGameManager instance;

    public static MMGameManager Instance()
    {
        return GameObject.FindObjectOfType<MMGameManager>();
    }

    private void Start()
    {
        moneyRecepticles = new List<MoneyRecepticle>();
        MoneyRecepticle[] r = FindObjectsOfType<MoneyRecepticle>();
        moneyRecepticles.AddRange(r);
        instance = MMGameManager.Instance();
        dollars += startingCash;
        Physics.gravity = new Vector3(0, -5.0f, 0);
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
        maxDollars = recepticles * dollarsPerRecepticle + startingCash;
        
        textMesh.text = "$" + dollars.ToString("0,000.00");
    }

    public void MakeDollars(float amount)
    {
        if (dollars + amount <= maxDollars)
        {
            dollars += Mathf.Abs(amount);
            GameObject g = GameObject.Instantiate(moneyParticle, moneyParticleSpawn.transform.position, Quaternion.identity) as GameObject;
            MoneyParticle p = g.GetComponent<MoneyParticle>();
            p.SetText("+$" + amount.ToString("0.00"), Color.green);
        }
        else
        {
            float diff = maxDollars - dollars;
            float extraMoney = amount - diff;
            int neededRecepticles = Mathf.FloorToInt(extraMoney / dollarsPerRecepticle);
            dollars = maxDollars;
            NeedMoreRecepticles(neededRecepticles);
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
            default:
                break;
        }
    }

    private void SpawnRecepticle()
    {
        GameObject o = GameObject.Instantiate(recepticlePrefab) as GameObject;
        MoneyRecepticle r = o.GetComponent<MoneyRecepticle>();
        moneyRecepticles.Add(r);
    }
}