using UnityEngine;

public class EventText : MonoBehaviour
{

    TextMesh textMesh;

    public float displayTime;
    private float activeTime;
    private float fade;
    private float fadeTarget;
    private int fadeSpeed;
    private Color targetColor;
    private SpringJoint spring;
    
    private void Start()
    {
        textMesh = GetComponent<TextMesh>();
        //fade = 0.0f;
        //FadeOut();
        //spring = GetComponent<SpringJoint>();
        //spring.connectedBody = GameObject.FindGameObjectWithTag("EventTextAnchor").GetComponent<Rigidbody>();
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

        if (activeTime > 10.0f)
        {
            Destroy(gameObject);
        }
    }

    public void SendText(string text, Color color, float _displayTime = 1)
    {
        if (textMesh == null)
        {
            textMesh = GetComponent<TextMesh>();
        }

            
        textMesh.text = text;
        textMesh.color = new Color(color.r, color.g, color.b, fade);
        displayTime = _displayTime;
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