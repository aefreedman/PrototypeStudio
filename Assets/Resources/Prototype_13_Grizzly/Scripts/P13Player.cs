using UnityEngine;

public class P13Player : MonoBehaviour
{

    public GameObject selectCube;
    private GameObject lastObject;
    
    public void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, 20.0f))
        {
            if (hit.collider.gameObject.tag.StartsWith("P13Item"))
            {
                P13Item i = hit.collider.gameObject.GetComponent<P13Item>();
                if (i.canConsume && !i.consumed)
                {
                    lastObject = hit.collider.gameObject;
                }
            }
        }

        if (lastObject != null)
        {
            if (selectCube.activeInHierarchy)
            {
                selectCube.transform.parent = lastObject.transform;
                selectCube.transform.position = lastObject.transform.position;
                selectCube.transform.localScale = Vector3.one * 1.2f;
                selectCube.transform.localRotation = lastObject.transform.rotation;
            }
            else
            {
                selectCube.SetActive(true);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                selectCube.transform.parent = this.gameObject.transform;
                selectCube.SetActive(false);
                lastObject.GetComponent<P13Item>().ConsumeItem();
                lastObject = null;
            }
        }
        else if (selectCube.activeInHierarchy)
        {
            selectCube.SetActive(false);
        }

    }
}