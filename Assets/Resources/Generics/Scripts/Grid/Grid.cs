using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GridSpot[,] gridSpots;
    public GameObject gridSpotPrefab;
    public int size;
    public List<GameObject> gridItems;

    private void Start()
    {
        gridSpots = new GridSpot[size, size];
        gridItems = new List<GameObject>();
        CreateGrid();
    }

    private void Update()
    {
        //Control();

        gridItems.Clear();
        foreach (GridSpot c in gridSpots)
        {
            if (c.contents != null)
            {
                gridItems.Add(c.contents);
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < size; i++) // row
        {
            for (int j = 0; j < size; j++) //column
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireCube(new Vector3(transform.position.x + i, transform.position.y + j, transform.position.z), Vector3.one);
            }
        }
    }

    private void CreateGrid()
    {
        for (int i = 0; i < size; i++) // row
        {
            for (int j = 0; j < size; j++) //column
            {
                GameObject g = (GameObject)Instantiate(gridSpotPrefab, new Vector3(transform.position.x + i, transform.position.y + j, transform.position.z), Quaternion.identity);
                g.transform.parent = transform;
                gridSpots[i, j] = g.GetComponent<GridSpot>();
                gridSpots[i, j].grid = this;
            }
        }
    }

    private void Activate()
    {
    }

    private void Control()
    {
        int yDelta = 0;
        int xDelta = 0;
        if (Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                yDelta = 1;
            }
            else
            {
                yDelta = -1;
            }
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                xDelta = 1;
            }
            else
            {
                xDelta = -1;
            }
        }

        //if (xDelta != 0 || yDelta != 0)
        //{
        //    if (!PathBlocked(xDelta, yDelta))
        //    {
                transform.position = new Vector3(transform.position.x + xDelta, transform.position.y + yDelta, 0);
        //    }
        //}

    }

    //private bool PathBlocked(int xDelta, int yDelta)
    //{
    //    bool blocked = true;
    //    int numberofBlocks = 0;
    //    foreach (GameObject i in gridItems)
    //    {
    //        GridItem item = i.GetComponent<GridItem>();
    //        if (item.blocksMovement) // if an item on the activator grid blocks movement
    //        {
    //            GridSpot itemSpot = item.gridPosition.grid.GetGridSpotAtWorldCoord(i.transform.position); // get the position of the activator
    //            GridSpot spotToCheck = GridManager.Instance().objectGrid.GetGridSpotAtWorldCoord(new Vector3(item.transform.position.x + xDelta, item.transform.position.y + yDelta, 0)); // get the position we're trying to move into
    //            if (spotToCheck != null) // if the position we're trying to move to exists
    //            {
    //                //Debug.Log("Checking " + spotToCheck.transform.position);
    //                if (spotToCheck.contents != null) // if the position has something in it
    //                {
    //                    if (spotToCheck.contents.GetComponent<GridItem>().blocksMovement)
    //                    {
    //                        Debug.Log("PathBlock!");
    //                        numberofBlocks++;
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                numberofBlocks = 1;
    //            }
    //        }
    //    }
    //    if (numberofBlocks == 0)
    //    {
    //        blocked = false;
    //    }
    //    return blocked;
    //}

    public bool SpotOccupied(int x, int y)
    {
        if (gridSpots[x, y].contents != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public GridSpot GetGridSpotAtWorldCoord(Vector3 pos)
    {
        foreach (GridSpot g in gridSpots)
        {
            if (g.transform.position.x == pos.x && g.transform.position.y == pos.y)
            {
                return g;
            }
        }
        return null;
    }
}