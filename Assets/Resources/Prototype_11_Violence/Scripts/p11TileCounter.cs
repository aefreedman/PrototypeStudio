using UnityEngine;

public class p11TileCounter : MonoBehaviour
{
    public GameObject connectedTiles;
    private p11FloorTile[] tile;
    public int count;
    private int tileCount;
    public TextMesh[] text;
    public int tolerance;
    public int target;

    private void Start()
    {
        tile = connectedTiles.GetComponentsInChildren<p11FloorTile>();
        text = GetComponentsInChildren<TextMesh>();
    }

    private void Update()
    {
        count = 0;
        for (int i = 0; i < tile.Length; i++)
        {
            float h, s, v;
            Utilities.ColorToHSV(tile[i].renderer.material.color, out h, out s, out v);
            float _h, _s, _v;
            Utilities.ColorToHSV(tile[i].materials[1].color, out _h, out _s, out _v);
            if (Mathf.Abs(h - _h) > tolerance)
            {
                count++;
            }
            else if (Mathf.Abs(s - _s) > tolerance)
            {
                count++;
            }

        }
        for (int j = 0; j < text.Length; j++)
        {
            text[j].text = count.ToString();
        }
    }
}