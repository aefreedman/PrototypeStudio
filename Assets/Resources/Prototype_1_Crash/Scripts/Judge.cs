using UnityEngine;

public class Judge : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    //public float fadeOutTime;
    //public float fadeInTime;
    public float displayTime;
    private float activeTime;
    private float fade;
    private float fadeTarget;
    public Sprite[] sprites;
    private int fadeSpeed;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.color = new Color(0, 0, 0, 1.0f);
        fade = 0.0f;
        Deactivate();
    }

    private void Update()
    {
        fade = Mathf.Lerp(fade, fadeTarget, fadeSpeed * Time.deltaTime);
        activeTime += Time.deltaTime;
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, fade);
        if (activeTime > displayTime)
        {
            Deactivate();
        }
    }

    public void Activate()
    {
        fadeTarget = 1.0f;
        activeTime = 0;
        fadeSpeed = 10;
    }

    public void Deactivate()
    {
        fadeTarget = 0;
        fadeSpeed = 5;
    }
}