using UnityEngine;

public class EventText : MonoBehaviour
{
    private TextMesh textMesh;

    public float displayTime;
    private float activeTime;
    private float fade;
    private float fadeTarget;
    private int fadeSpeed;
    private Color targetColor;
    private SpringJoint spring;
    public float lifetime;

    private void Start()
    {
        textMesh = GetComponent<TextMesh>();
    }

    private void Update()
    {
        fade = Mathf.Lerp(fade, fadeTarget, fadeSpeed * Time.deltaTime);
        activeTime += Time.deltaTime;
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, fade);
        if (activeTime > displayTime)
        {
            FadeOut();
        }

        if (activeTime > lifetime)
        {
            Destroy(gameObject);
        }
    }

    public void SendText(string text, Color color, float _displayTime = 1, int _size = 500)
    {
        if (textMesh == null)
        {
            textMesh = GetComponent<TextMesh>();
        }

        textMesh.fontSize = _size;
        textMesh.text = text;
        textMesh.color = new Color(color.r, color.g, color.b, fade);
        displayTime = _displayTime;
        lifetime = displayTime * 2;
        FadeIn();
    }

    public void FadeIn()
    {
        fadeTarget = 1.0f;
        activeTime = 0;
        fadeSpeed = 10;
    }

    public void FadeOut()
    {
        fadeTarget = 0;
        fadeSpeed = 5;
    }
}