using UnityEngine;

public class FanGameController : MonoBehaviour
{

    CharacterController controller;
    
    private void Start()
    {
        Screen.lockCursor = true;
        Screen.showCursor = false;
        
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Mouse X") != 0)
        {
            controller.SimpleMove(transform.forward * 100.0f * Time.deltaTime);
        }
    }
}