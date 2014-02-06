using UnityEngine;
using MMConstants;

public class MenuItem : MonoBehaviour
{
    public Item itemToSpawn;
    private TextMesh textMesh;
    
    private void Start()
    {
        textMesh = GetComponent<TextMesh>();
    }
    
    void OnMouseOver()
    {
        textMesh.color = Color.yellow;
        if (Input.GetMouseButtonDown(0))
        {
            MMGameManager.instance.SpawnItem(itemToSpawn);
        }
    }

    void OnMouseExit()
    {
        textMesh.color = Color.white;
    }
    

    private void Update()
    {
    }
}