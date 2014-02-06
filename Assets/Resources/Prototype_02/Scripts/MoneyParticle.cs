using UnityEngine;

public class MoneyParticle : MonoBehaviour
{
    public float lifetime;
    private float start;
    public TextMesh mesh;

    private void Start()
    {
        rigidbody.AddForce(new Vector3(Random.Range(-150, 150), 200, 0));
        start = Time.time;
    }

    private void Update()
    {
        if (Time.time > start + lifetime)
        {
            Destroy(gameObject);
        }
    }

    public void SetText(string text, Color color)
    {
        mesh.text = text;
        mesh.color = color;
    }
}