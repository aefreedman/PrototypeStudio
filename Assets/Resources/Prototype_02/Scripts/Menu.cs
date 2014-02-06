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

    private void Start()
    {
        closePos = transform.position;
        openPos = new Vector3(closePos.x + 0.75f, closePos.y, closePos.z);
        targetPos = closePos;
        menuItems = menuItemHolder.GetComponentsInChildren<MenuItem>();
    }

    void OnMouseOver()
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

    void OnMouseExit()
    {

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
}