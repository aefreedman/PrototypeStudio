using UnityEngine;
using System.Collections;

public class P6BeatGen : MonoBehaviour
{

    public P6Beat beatPrefab;
    public GameObject targetPad;
    public GameObject beatContainer;

    void Start()
    {
        ObjectPool.CreatePool(beatPrefab);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector3.one / 10);
    }

    public P6Beat GenerateBeat()
    {
        P6Beat o = ObjectPool.Spawn(beatPrefab, transform.position, beatPrefab.transform.rotation);
        o.target = targetPad.transform.position;
        o.transform.parent = beatContainer.transform;
        return o;
    }
}
