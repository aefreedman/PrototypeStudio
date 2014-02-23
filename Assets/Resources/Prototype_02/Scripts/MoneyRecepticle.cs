using UnityEngine;

public class MoneyRecepticle : MonoBehaviour
{

    GameObject moneyMaker;
    public float attractSpeed;
    
    private void Start()
    {
        moneyMaker = GameObject.FindObjectOfType<MoneyMaker>().gameObject;
    }
    
    void FixedUpdate()
    {
        if (moneyMaker != null)
        {
            FollowTargetWitouthRotation(moneyMaker.transform, 0.0f, attractSpeed);
        }
        else
        {
            moneyMaker = GameObject.FindObjectOfType<MoneyMaker>().gameObject;
        }
    }

    void FollowTargetWitouthRotation(Transform target, float distanceToStop, float speed)
    {
        var direction = Vector3.zero;
        if (Vector3.Distance(transform.position, target.position) > distanceToStop)
        {
            direction = target.position - transform.position;
            rigidbody.AddRelativeForce(direction.normalized * speed, ForceMode.Force);
        }
    }

    private void Update()
    {
    }
}