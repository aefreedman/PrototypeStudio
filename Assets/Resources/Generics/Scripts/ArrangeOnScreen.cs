using UnityEngine;
using System.Collections;

public class ArrangeOnScreen : MonoBehaviour
{
    Camera camera;
    [Range(0.0f, 1.0f)]
    public float x;
    [Range(0.0f, 1.0f)]
    public float y;

    void Start()
    {
        camera = FindObjectOfType<Camera>();

        transform.position = camera.ViewportToWorldPoint(new Vector3(x, y, 30));
    }

    void Update()
    {

    }
}
