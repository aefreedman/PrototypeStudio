using UnityEngine;

public class p11FloorTile : MonoBehaviour
{
    public Material[] materials;
    private bool triggerMaterialChange;
    private float colorLerpSpeed = 1.500f;
    public bool colorWillReset;
    public bool off;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            triggerMaterialChange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            triggerMaterialChange = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

        }
    }

    private void Update()
    {
        if (!p11GameManager.Instance.inBox)
        {
            if (triggerMaterialChange)
            {
                renderer.material.color = Color.Lerp(renderer.material.color, materials[0].color, colorLerpSpeed * Time.deltaTime);
                float h, s, v;
                Utilities.ColorToHSV(renderer.material.color, out h, out s, out v);
                float _h, _s, _v;
                Utilities.ColorToHSV(materials[1].color, out _h, out _s, out _v);
                if (Mathf.Abs(h - _h) > 20)
                {
                    colorWillReset = false;
                }
                else if (Mathf.Abs(s - _s) > 20)
                {
                    colorWillReset = false;
                }
            }
            else if (colorWillReset)
            {
                renderer.material.color = Color.Lerp(renderer.material.color, materials[1].color, colorLerpSpeed * 3.0f * Time.deltaTime);
            }   
        }
        if (renderer.material.color.a < 10.0f)
        {
            off = true;
        }
        else
        {
            off = false;
        }

    }
}