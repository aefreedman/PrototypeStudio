using UnityEngine;

public class KeyboardControl : MonoBehaviour
{

    public bool physicsMove;
    public bool useCharacterController;
    public bool leftRightIsRotation;
    public bool mouseControlsCamera;
    public bool TopDownMovement;
    public Vector3 moveSpeed;
    private CharacterController characterController;

    private void Start()
    {
        if (GetComponent<CharacterController>())
        {
            characterController = GetComponent<CharacterController>();
        }
        if (mouseControlsCamera)
        {
            Screen.showCursor = false;
        }
    }

    private void FixedUpdate()
    {
        if (useCharacterController)
        {
            if (TopDownMovement)
            {
                #region 2D / Top-down controls schemes

                if (Input.GetButton("Horizontal"))
                {
                    if (leftRightIsRotation)
                    {
                        transform.Rotate(Vector3.forward, Input.GetAxis("Horizontal") * moveSpeed.z * Time.deltaTime);
                    }
                    else
                    {
                        characterController.Move(new Vector3(
                            Input.GetAxis("Horizontal") * moveSpeed.x * Time.deltaTime,
                            0,
                            0
                            ));
                    }
                }
                if (Input.GetButton("Vertical"))
                {
                    if (leftRightIsRotation)
                    {
                        characterController.Move(Input.GetAxis("Vertical") * transform.up * moveSpeed.y * Time.deltaTime);
                    }
                    else
                    {
                        characterController.Move(new Vector3(
                            0,
                            Input.GetAxis("Vertical") * moveSpeed.y * Time.deltaTime,
                            0
                            ));
                    }
                }
                #endregion
            }
            else
            {
                #region 3D Perspective controls w/ character controller

                if (Input.GetButton("Horizontal"))
                {
                    if (leftRightIsRotation)
                    {
                        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * moveSpeed.x * Time.deltaTime);
                    }
                    else
                    {
                        characterController.Move(new Vector3(
                            Input.GetAxis("Horizontal") * moveSpeed.x * Time.deltaTime,
                            0,
                            0
                            ));
                    }
                }
                if (Input.GetButton("Vertical"))
                {

                    characterController.Move(new Vector3(
                        0,
                        0,
                        Input.GetAxis("Vertical") * moveSpeed.z * Time.deltaTime
                        ));
                }

                if (mouseControlsCamera)
                {
                    if (Input.GetAxis("MouseHorizontal") > 0)
                    {
                        transform.Rotate(Vector3.up, 1.0f, Space.World);
                    }
                    else if (Input.GetAxis("MouseHorizontal") < 0)
                    {
                        transform.Rotate(Vector3.up, -1.0f, Space.World);
                    }
                    if (Input.GetAxis("MouseVertical") > 0 && transform.rotation.eulerAngles.x < 5.0f)
                    {
                        //transform.Rotate(Vector3.right, 1.0f, Space.World);
                    }
                    else if (Input.GetAxis("MouseVertical") < 0 && transform.rotation.eulerAngles.x > -5.0f)
                    {
                        //transform.Rotate(Vector3.right, -1.0f, Space.World);
                    }
                }
                #endregion
            }
        }
        else
        {
            #region Non-character controller control schemes
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
            #endregion
        }
    }
}