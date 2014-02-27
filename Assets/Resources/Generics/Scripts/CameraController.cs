using UnityEngine;

public class CameraController : MonoBehaviour
{

    private float scale;
    
    private void Start()
    {
        scale = 1.0f;
    }

    private void Update()
    {
        if (Input.GetButton("CameraHorizontal"))
        {
        }
        Camera.main.transform.Translate(new Vector3(Input.GetAxis("CameraHorizontal") * scale, Input.GetAxis("CameraVertical") * scale));
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            float x = 3.0f/4.0f;
            Camera.main.orthographicSize *= x;
            scale *= x;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            float x = 4.0f / 3.0f;
            Camera.main.orthographicSize *= x;
            scale *= x;
        }
    }
}