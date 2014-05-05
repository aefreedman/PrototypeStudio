using UnityEngine;
using System.Collections;


public class P13Item : MonoBehaviour
{
    public bool canConsume;
    public P13GameManager.Resources property;
    public int value;
    protected bool fadeOut;
    Color originalColor;
    public P13Item prefab;
    public bool consumed;
    public P13ItemGenerator host;
    
    protected virtual void Start()
    {
        if (renderer != null && renderer.material.HasProperty("_Color"))
        {
            originalColor = renderer.material.color;
        }
        ObjectPool.CreatePool(prefab);
        consumed = false;
    }

    protected void FixedUpdate()
    {
        if (fadeOut && renderer != null && renderer.material.HasProperty("_Color"))
        {
            renderer.material.color = Color.Lerp(renderer.material.color, Color.clear, 10.0f * Time.deltaTime);
        }
    }

    public virtual void ConsumeItem()
    {
        consumed = true;
        StartCoroutine(Reset(1.0f));
        P13GameManager.Instance.AddResource(property, value);
        Debug.Log("Got " + value.ToString() + " " + System.Enum.GetName(typeof(P13GameManager.Resources), property));
    }

    protected IEnumerator Reset(float fadeTime)
    {
        fadeOut = true;
        yield return new WaitForSeconds(fadeTime);
        host.RemoveItem(this);
        ObjectPool.Recycle(this);
        if (renderer != null && renderer.material.HasProperty("_Color"))
        {
            renderer.material.color = originalColor;
        }
        fadeOut = false;
    }

}