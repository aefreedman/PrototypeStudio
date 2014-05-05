using System.Collections.Generic;
using UnityEngine;

public class P13ItemGenerator : MonoBehaviour
{
    public P13Item item;
    public int numberToMaintain;
    public float spawnPeriod;
    private float lastSpawn;
    private List<P13Item> itemList;
    public float range;
    public bool prewarm;
    public bool rotate;

    private void Start()
    {
        itemList = new List<P13Item>();
        if (prewarm)
        {
            for (int i = 0; i < numberToMaintain; i++)
            {
                GenerateItem();
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, Vector3.one * range * 2);
    }

    private P13Item GenerateItem()
    {
        Vector3 pos = new Vector3(transform.position.x + Random.Range(-range, range), 100.0f, transform.position.z + Random.Range(-range, range));
        RaycastHit hit;
        if (Physics.Raycast(pos, Vector3.down, out hit, 100.0f))
        {
            if (hit.collider.gameObject.tag.StartsWith("Environment"))
            {
                pos = new Vector3(pos.x, hit.point.y + 0.1f, pos.z);
            }
        }
        Quaternion rotation = Quaternion.identity;
        if (rotate)
        {
            rotation = Quaternion.Euler((Vector3.right * 90) + hit.normal);
        }
        P13Item o = ObjectPool.Spawn(item, pos, rotation) as P13Item;
        o.host = this;
        itemList.Add(o);
        lastSpawn = Time.time;
        o.gameObject.transform.parent = GameObject.Find("Items").transform;
        return o;
    }

    private void Update()
    {
        if (itemList.Count < numberToMaintain)
        {
            if (Time.time > lastSpawn + spawnPeriod)
            {
                GenerateItem();
            }
        }
    }

    public void RemoveItem(P13Item item)
    {
        itemList.Remove(item);
    }
}