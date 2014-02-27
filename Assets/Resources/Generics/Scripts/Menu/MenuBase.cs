using UnityEngine;

/// <summary>
/// A base class for 2D menus that exist in the game world.
/// </summary>
public abstract class MenuBase : MonoBehaviour
{
    private bool attachedToCamera;
    protected Vector3 openPos;
    protected Vector3 closePos;
    protected Vector3 targetPos;
    public GameObject menuItemContainer;
    public Vector2 itemTargetDisplacementAdd;
    public Vector2 openDisplacement;
    public float openSpeed;
    private MenuItemBase[] menuItems;
    private static MenuBase instance;

    public static MenuBase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MenuBase>();
            }
            return instance;
        }
    }

    protected virtual void Start()
    {
        if (gameObject.transform.parent.CompareTag("MainCamera"))
        {
            closePos = transform.localPosition;
            targetPos = closePos;
            attachedToCamera = true;
        }
        else
        {
            attachedToCamera = false;
            closePos = transform.position;
            openPos = new Vector3(closePos.x + openDisplacement.x, closePos.y + openDisplacement.y, closePos.z);
            targetPos = closePos;
        }
        menuItems = menuItemContainer.GetComponentsInChildren<MenuItemBase>();
        //SetMenuChoiceStartingConditions();

    }

    protected virtual void OnMouseOver()
    {
        if (targetPos == closePos)
        {
            if (Input.GetMouseButtonDown(0))
            {
                targetPos = openPos;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                targetPos = closePos;
            }
        }
    }

    protected virtual void Update()
    {
        if (attachedToCamera)
        {
            openPos = new Vector3(transform.localPosition.x + openDisplacement.x, transform.localPosition.y + openDisplacement.y, closePos.z);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, openSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, openSpeed * Time.deltaTime);
        }


        foreach (var i in menuItems)
        {
            Vector3 itemTargetPos = targetPos;
            if (targetPos == openPos) // If the menu is open
            {
                //itemTargetPos = new Vector3((targetPos.x * itemTargetDisplacementScalar.x) + i.OriginalPos.x, (targetPos.y * itemTargetDisplacementScalar.y) + i.OriginalPos.y, i.transform.position.z);
                itemTargetPos = new Vector3(i.OriginalPos.x + itemTargetDisplacementAdd.x, i.OriginalPos.y + itemTargetDisplacementAdd.y, targetPos.z);
            }
            else // if not open
            {
                //itemTargetPos = new Vector3((targetPos.x * -itemTargetDisplacementScalar.x) - i.OriginalPos.x, (targetPos.y * -itemTargetDisplacementScalar.y) - i.OriginalPos.y, i.transform.position.z);
                itemTargetPos = i.OriginalPos;
            }
            i.transform.position = Vector3.Lerp(i.transform.position, itemTargetPos, openSpeed * 1.15f * Time.deltaTime);
        }
    }

    public void DisableAllMenuChoices()
    {
        foreach (var i in menuItems)
        {
            i.Disable = true;
            i.Text.color = i.disableColor;
        }
    }

    public void EnableAllMenuChoices()
    {
        foreach (var i in menuItems)
        {
            i.Disable = false;
            i.Text.color = i.defaultColor;
        }
    }

    public void SetToStartConditions()
    {
        foreach (var i in menuItems)
        {
            i.Disable = !i.startActive;
            i.Text.color = i.defaultColor;
        }
    }

    //protected abstract void SetMenuChoiceStartingConditions();
}