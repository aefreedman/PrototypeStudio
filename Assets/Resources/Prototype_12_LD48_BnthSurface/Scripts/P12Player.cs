using UnityEngine;

public class P12Player : MonoBehaviour
{
    private GameObject root;
    public float grabDistance;
    public float originalMoveSpeed;

    public enum Polarity { up, down };

    public Polarity polarity;

    private void Start()
    {
        root = GameObject.Find("_root");
        originalMoveSpeed = GetComponent<FirstPersonDrifter>().walkSpeed;
    }

    private void Update()
    {
        //RaycastHit rayHit;
        //Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rayHit);
        //if (rayHit.collider.gameObject.CompareTag("P12TriggerObj") && rayHit.distance < grabDistance)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        rayHit.collider.gameObject.transform.parent = gameObject.transform;
        //    }
        //    else if (Input.GetMouseButtonUp(0))
        //    {
        //        rayHit.collider.gameObject.transform.parent = root.transform;
        //    }
        //}
    }

    public void ResetMoveSpeed()
    {
        GetComponent<FirstPersonDrifter>().walkSpeed = originalMoveSpeed;
    }

    public void SwitchPolarity()
    {
        switch (polarity)
        {
            case Polarity.up:
                polarity = Polarity.down;
                break;

            case Polarity.down:
                polarity = Polarity.up;

                break;

            default:
                break;
        }
    }
}