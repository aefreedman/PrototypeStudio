using UnityEngine;

public class Menu : MonoBehaviour
{
    private Vector3 openPos;
    private Vector3 closePos;
    public Vector3 targetPos;
    public GameObject menuItemHolder;
    public float itemTargetXPos;
    public float openSpeed;
    private MenuItem[] menuItems;
    private static Menu instance;

    public static Menu Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Menu>();
            }
            return instance;
        }
    }

    private void Start()
    {
        closePos = transform.position;
        openPos = new Vector3(closePos.x + 0.75f, closePos.y, closePos.z);
        targetPos = closePos;
        menuItems = menuItemHolder.GetComponentsInChildren<MenuItem>();
        SetMenuChoiceStartingConditions();
    }

    private void OnMouseOver()
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

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, openSpeed * Time.deltaTime);
        foreach (var i in menuItems)
        {
            Vector3 itemTargetPos = targetPos;
            if (targetPos == openPos)
            {
                itemTargetPos = new Vector3(targetPos.x + itemTargetXPos, i.transform.position.y, i.transform.position.z);
            }
            else
            {
                itemTargetPos = new Vector3(targetPos.x - itemTargetXPos, i.transform.position.y, i.transform.position.z);
            }
            i.transform.position = Vector3.Lerp(i.transform.position, itemTargetPos, openSpeed * 1.15f * Time.deltaTime);
        }
    }

    public void DisableAllMenuChoices()
    {
        foreach (var i in menuItems)
        {
            i.Disable = true;
        }
    }

    public void EnableAllMenuChoices()
    {
        foreach (var i in menuItems)
        {
            i.Disable = false;
        }
    }

    public void SetMenuChoiceStartingConditions()
    {
        foreach (var i in menuItems)
        {
            switch (i.itemToSpawn)
            {
                case MMConstants.Item.Recepticle:
                    i.Disable = true;
                    break;

                case MMConstants.Item.MoneyMaker:
                    i.Disable = false;
                    break;

                case MMConstants.Item.ShuttleSpeed:
                    i.Disable = true;
                    break;

                case MMConstants.Item.Premium:
                    i.Disable = true;
                    break;

                case MMConstants.Item.GiveUp:
                    i.Disable = false;
                    break;

                case MMConstants.Item.Quit:
                    i.Disable = false;
                    break;

                default:
                    break;
            }
        }
    }
}