using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AudioManagerBase : MonoBehaviour
{
    public AudioClip[] audioClip;

    private static AudioManagerBase instance;

    public static AudioManagerBase Instance
    {
        get
        {
            if (instance == null)
            {
                Instance = FindObjectOfType<AudioManagerBase>();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    
    private void Start()
    {
    }

    private void Update()
    {
    }

    public void PlayClipOneShot(int clipNumber)
    {
        audio.PlayOneShot(audioClip[clipNumber]);
    }
}