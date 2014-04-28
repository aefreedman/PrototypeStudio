using UnityEngine;

public class P12ViewBox : MonoBehaviour
{
    public GameObject[] connectedObjects;
    public GameObject player;
    public Font font;
    public bool triggerFade;
    public bool resetsLevel;

    private void Start()
    {
        for (int i = 0; i < connectedObjects.Length; i++)
        {
            connectedObjects[i].SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    void OnTriggerStay(Collider other)
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            p11GameManager.Instance.inBox = true;            
            for (int i = 0; i < connectedObjects.Length; i++)
            {
                connectedObjects[i].SetActive(true);
            }
            player.GetComponent<FirstPersonDrifter>().walkSpeed = 1.0f;
            //GameObject t = p11GameManager.Instance.CreateEventMessage("There is a way out", Color.white, Vector3.zero, 10.0f);
            //t.GetComponent<SpringJoint>().damper = 1.0f;
            //t.GetComponent<TextMesh>().font = font;
            //t.renderer.material = font.material;
        }
    }

    void OnTriggerExit(Collider other)
    {
        p11GameManager.Instance.inBox = false;

        if (other.gameObject.CompareTag("Player"))
        {
            if (resetsLevel)
            {
                p11GameManager.Instance.ResetTiles();
            }   
            for (int i = 0; i < connectedObjects.Length; i++)
            {
                connectedObjects[i].SetActive(false);
            }
            if (triggerFade)
            {
                CameraFadeOnStart fade = GameObject.Find("Main Camera").GetComponent<CameraFadeOnStart>();
                if (fade != null)
                {
                    fade.Fade();
                }
            }

            player.GetComponent<P12Player>().ResetMoveSpeed();
            player.GetComponent<P12Player>().SwitchPolarity();
        }
    }

}