using UnityEngine;

public class P6Combo : MonoBehaviour
{
    private TextMesh text;

    private void Start()
    {
        text = GetComponent<TextMesh>();
    }

    private void FixedUpdate()
    {
        text.text = P6GameManager.Instance.combo.ToString() + "x";
    }
}