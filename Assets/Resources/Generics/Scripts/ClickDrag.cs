using UnityEngine;
using System.Collections;

/// <summary>
/// Adding this script to an object allows you to drag the object around. 2D only.
/// </summary>
public class ClickDrag : MonoBehaviour
{

    Camera camera;

    void Start()
    {
        camera = FindObjectOfType<Camera>();
    }

    void OnMouseDrag()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }

    void Update()
    {

    }
}
