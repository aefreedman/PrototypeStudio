using UnityEngine;

public class p11EyeBlink : MonoBehaviour
{
    public float blinkPeriod;
    private float lastBlink;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.time > lastBlink + blinkPeriod)
        {
            animator.SetTrigger("blink");
            lastBlink = Time.time;
        }
    }
}