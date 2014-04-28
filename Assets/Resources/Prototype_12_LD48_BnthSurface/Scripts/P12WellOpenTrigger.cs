using UnityEngine;
using System.Collections;

public class P12WellOpenTrigger : MonoBehaviour
{

    public GameObject doorObj;
    private Vector3 doorOriginalPos;
    private bool resetPos;
    public float lerpSpeed;

    void Start()
    {
        doorOriginalPos = doorObj.transform.position;
    }

    void Update()
    {
        if (resetPos)
        {
            doorObj.transform.position = Vector3.Lerp(doorObj.transform.position, doorOriginalPos, lerpSpeed * Time.deltaTime);

        }
    }

    void OnTriggerStay(Collider other)
    {
        doorObj.transform.position = Vector3.Lerp(doorObj.transform.position, doorOriginalPos + Vector3.left * 10, lerpSpeed * Time.deltaTime);
        resetPos = false;
    }

    void OnTriggerExit(Collider other)
    {
        resetPos = true;
    }



}
