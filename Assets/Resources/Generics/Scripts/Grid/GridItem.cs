using UnityEngine;

public class GridItem : MonoBehaviour
{
    private Vector2 startPos;
    public GridSpot gridPosition;
    public Camera camera;
    public GridManager gridManager;
    public bool blocksMovement;

    protected virtual void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();
        startPos = new Vector2(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
    }
    
    void OnDrawGizmos()
    {
        transform.position = new Vector2(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
    }

    protected virtual void Update()
    {
        if (gridPosition == null)
        {
            SetGridPosition(startPos);
        }
    }

    public virtual void Activate()
    {
    }

    public virtual bool SetGridPosition(Vector2 pos)
    {
        return false;
    }
}