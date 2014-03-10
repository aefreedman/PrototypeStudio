using UnityEngine;

public class P6Scoreboard : MonoBehaviour
{

    private TextMesh text;

    private void Start()
    {
        text = GetComponent<TextMesh>();
    }

    private void FixedUpdate()
    {
        int p = P6GameManager.Instance.points;
        p *= 55;
        
        text.text = p.ToString("0000000000");
    }
}