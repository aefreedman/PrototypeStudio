using UnityEngine;
using System.Collections;

public class ArrangeOnScreen : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float x;
    [Range(0.0f, 1.0f)]
    public float y;

    void Start()
    {
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 30));
    }

    void Update()
    {

    }
}
