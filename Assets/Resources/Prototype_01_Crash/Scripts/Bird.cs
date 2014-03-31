using UnityEngine;

public class Bird : MonoBehaviour
{
    public enum Direction { left, right };

    public float moveForce;
    public float flightForce;
    public Direction direction;
    private Animator animator;
    public float minVel;
    public int crashes;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //flightForce = -Physics2D.gravity.y;
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        switch (direction)
        {
            case Direction.left:
                rigidbody2D.AddForce(new Vector2(0, flightForce));
                if (rigidbody2D.velocity.x > -minVel)
                {
                    rigidbody2D.AddForce(new Vector2(-moveForce, 0));
                }
                animator.SetInteger("Direction", 1);
                break;

            case Direction.right:
                rigidbody2D.AddForce(new Vector2(0, flightForce));
                if (rigidbody2D.velocity.x < minVel)
                {
                    rigidbody2D.AddForce(new Vector2(moveForce, 0));
                }
                animator.SetInteger("Direction", 0);
                break;

            default:
                break;
        }
    }

    private void OnMouseUpAsButton()
    {
        flightForce = 0;
    }

    public void SetDirection(Direction dir)
    {
        direction = dir;
        switch (direction)
        {
            case Direction.left:
                break;

            case Direction.right:
                break;

            default:
                break;
        }
    }

    public void SetForce(float val)
    {
        moveForce = val;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.gameObject.name == "Ground")
        {
            DestroyObject(gameObject);
        }
        if (coll.collider.gameObject.name == "Edge")
        {
            DestroyObject(gameObject);
            CrashGameManager.Score(crashes);
            Debug.Log("Bird hit " + crashes + " fireworks");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<FireworkExplosion>())
        {
            crashes++;
            //Debug.Log("Bird hit explosion!");
        }
    }


}