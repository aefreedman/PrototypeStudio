using UnityEngine;

public class KeyboardControl : MonoBehaviour
{

    public bool physicsMove;
    public bool leftRightIsRotation;
    public Vector3 moveSpeed;

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        if (physicsMove)
        {
            if (Input.GetButton("Horizontal"))
            {
                if (leftRightIsRotation)
                {
                    rigidbody.AddTorque(new Vector3(0, 0, Input.GetAxis("Horizontal") * -moveSpeed.x * Time.deltaTime));
                }
                else
                {
                    rigidbody.AddForce(new Vector3(Input.GetAxis("Horizontal") * moveSpeed.x * Time.deltaTime, 0, 0));
                }
            }
            if (Input.GetButton("Vertical"))
            {
                if (leftRightIsRotation)
                {
                    rigidbody.AddRelativeForce(new Vector3(0, Input.GetAxis("Vertical") * moveSpeed.y * Time.deltaTime, 0));
                }
                else
                {
                    rigidbody.AddForce(new Vector3(0, Input.GetAxis("Vertical") * moveSpeed.y * Time.deltaTime, 0));
                }
            }
        }
        else
        {
            if (Input.GetButton("Horizontal"))
            {
                transform.Translate(new Vector3(Input.GetAxis("Horizontal") * moveSpeed.x * Time.deltaTime, 0, 0));
            }
            if (Input.GetButton("Vertical"))
            {
                transform.Translate(new Vector3(0, Input.GetAxis("Vertical") * moveSpeed.y * Time.deltaTime, 0));
            }
        }
    }
}