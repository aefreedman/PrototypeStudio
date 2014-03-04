using UnityEngine;

public class AutoRotate : MonoBehaviour
{

    public Vector3 rotation;
    
    private void Start()
    {
        rotation = new Vector3(rotation.x * Time.deltaTime, rotation.y * Time.deltaTime, rotation.z * Time.deltaTime);
    }

    private void Update()
    {
        transform.Rotate(rotation);
    }
}