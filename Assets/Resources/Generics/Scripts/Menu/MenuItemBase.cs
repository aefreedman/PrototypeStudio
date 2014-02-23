using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
/// <summary>
/// Basic template for objects that go into a Menu as interactable menu items
/// </summary>
public abstract class MenuItemBase : MonoBehaviour
{
    protected TextMesh textMesh;
    public TextMesh Text
    {
        get
        {
            return textMesh;
        }
    }
    public bool disable;
    public bool Disable
    {
        set
        {
            disable = value;
        }
        get
        {
            return disable;
        }
    }
    public Color defaultColor;
    public Color mouseOverColor;
    public Color disableColor;
    protected Vector3 originalPos;
    public bool startActive;

    public Vector3 OriginalPos
    {
        get
        {
            return originalPos;
        }
    }

    protected void Start()
    {
        textMesh = GetComponent<TextMesh>();
        originalPos = transform.position;
        if (!startActive)
        {
            disable = true;
        }
    }

    protected void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!disable)
            {
                Activate();
            }
            else
            {
                DisabledEvent();
            }
        }
    }

    protected void OnMouseExit()
    {
        textMesh.color = defaultColor;
    }
    
    protected void OnMouseEnter()
    {
        textMesh.color = mouseOverColor;
    }
    

    protected void Update()
    {
        if (disable)
        {
            textMesh.color = disableColor;
        }
        else
        {
            //textMesh.color = defaultColor;
        }
    }

    protected abstract void Activate();

    protected abstract void DisabledEvent();
}