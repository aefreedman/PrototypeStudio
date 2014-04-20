using UnityEngine;

public class P11AudioManager : AudioManagerBase
{

    public AudioClip[] glitchClip;
    public AudioClip[] walkClip;
    public GameObject player;
    public float walkClipPeriod;
    private float lastWalkClipPlay;

    private void Start()
    {
        lastWalkClipPlay = Time.time;
    }

    public void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            if (Time.time > lastWalkClipPlay + walkClipPeriod)
            {
                player.audio.PlayOneShot(walkClip[Random.Range(0, walkClip.Length)]);
                lastWalkClipPlay = Time.time;
            }

        }
    }

}