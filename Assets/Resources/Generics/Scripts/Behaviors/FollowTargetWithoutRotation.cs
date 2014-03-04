using UnityEngine;

public class FollowTargetWithoutRotation : MonoBehaviour
{

    public Transform target;
    public float distance;
    public float speed;
    
    private void Update()
    {
        FollowTargetWitouthRotation(target, distance, speed);
    }

    private void FollowTargetWitouthRotation(Transform target, float distanceToStop, float speed)
    {
        var direction = Vector3.zero;
        if (Vector3.Distance(transform.position, target.position) > distanceToStop)
        {
            direction = target.position - transform.position;
            rigidbody.AddRelativeForce(direction.normalized * speed, ForceMode.Force);
        }
    }
}