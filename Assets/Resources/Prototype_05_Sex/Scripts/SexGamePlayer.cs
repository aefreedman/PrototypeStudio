using UnityEngine;

public class SexGamePlayer : MonoBehaviour
{
    public enum PlayerNumber { One, Two };
    public PlayerNumber playerNumber;
    public SexBallGameManager.Team team;
    public Vector3 startPos;
    public Quaternion startRotation;
    public GameObject[] dangerObj;

    public GamepadInfo gamepad;
    public Vector3 moveSpeed;
    public Vector3 joystickDeadzone;
    private SexBallGameManager gm;

    private void Start()
    {
        startPos = transform.position;
        startRotation = transform.rotation;
        gm = SexBallGameManager.Instance as SexBallGameManager;
    }

    private void FixedUpdate()
    {
        switch (playerNumber)
        {
            case PlayerNumber.One:
                if (Mathf.Abs(gamepad.leftStick.x) > joystickDeadzone.x)
                {
                    rigidbody.AddTorque(new Vector3(0, 0, gamepad.leftStick.x * -moveSpeed.x * Time.deltaTime));
                }
                if (Mathf.Abs(gamepad.leftStick.y) > joystickDeadzone.y)
                {
                    rigidbody.AddRelativeForce(new Vector3(0, gamepad.leftStick.y * moveSpeed.y * Time.deltaTime, 0));
                }
                break;

            case PlayerNumber.Two:
                if (Mathf.Abs(gamepad.rightStick.x) > joystickDeadzone.x)
                {
                    rigidbody.AddTorque(new Vector3(0, 0, gamepad.rightStick.x * -moveSpeed.x * Time.deltaTime));
                }
                if (Mathf.Abs(gamepad.rightStick.y) > joystickDeadzone.y)
                {
                    rigidbody.AddRelativeForce(new Vector3(0, gamepad.rightStick.y * moveSpeed.y * Time.deltaTime, 0));
                }

                break;

            default:
                break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SexGameBall"))
        {
            audio.PlayOneShot(gm.boing[Random.Range(0, gm.boing.Length)]);
        }

    }

    public void SetGamepad()
    {
        gamepad = GamepadInfoHandler.Instance.AttachControllerToPlayer(gameObject);
    }

    public void Reset()
    {
        transform.position = startPos;
        rigidbody.velocity = Vector3.zero;
        transform.rotation = startRotation;
    }
}